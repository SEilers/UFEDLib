using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UFEDLib
{
    public class DeviceInfo
    {
        public static string Parse(String fileName)
        {
            var projectInfos = UFEDLib.Report.ParseProjectInfos(fileName);

            string version = projectInfos.reportVersion;
            string projectId = projectInfos.projectId;
            string dbFileName = projectId + ".db";
            string dbJsonFileName = projectId + ".json";
            string restoreDbFileName = projectId + ".sql";

            if (String.IsNullOrEmpty(version))
            {
                Console.WriteLine("Could not determine report version.");
                return "";
            }

            if (string.IsNullOrEmpty(projectId))
            {
                Console.WriteLine("Could not determine project ID.");
                return "";
            }

            var v = new Version(version);

            if (v < new Version("8.5"))
            {
                List<(string id, string name, string value)> DeviceInfo = null;

                if (fileName.EndsWith(".ufdr", StringComparison.OrdinalIgnoreCase))
                {
                    using (ZipArchive zip = ZipFile.OpenRead(fileName))
                    {
                        var report = zip.GetEntry("report.xml");

                        if (report == null)
                        {
                            Console.WriteLine("report.xml not found in the ufdr file");
                        }

                        using (Stream reportStream = report.Open())
                        {
                            return ParseDeviceInfoXML(reportStream);
                        }
                    }
                }
                else if (fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        return ParseDeviceInfoXML(fs);
                    }
                }
                else
                {
                    Console.WriteLine("Unsupported file type: " + fileName);
                }
            }
            else
            {
                bool pg_restore_installed = CanExecute("pg_restore");
                if (!pg_restore_installed)
                {
                    Console.WriteLine("pg_restore is not installed or not found in PATH. Please install PostgreSQL client tools to extract device infos from reports with version >= 8.5.");
                    return "";
                }

                using (ZipArchive archive = ZipFile.Open(fileName, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry entryDatabase = archive.GetEntry("DbData/database.db");
                    ZipArchiveEntry caseDbJson = archive.GetEntry("DbData/database.json");

                    if (entryDatabase != null)
                    {
                        entryDatabase.ExtractToFile(dbFileName, true);
                    }
                    else
                    {
                        Console.WriteLine("Database file not found in the report.");
                        return "";
                    }

                    if (caseDbJson != null)
                    {
                        caseDbJson.ExtractToFile(dbJsonFileName, true);
                    }
                    else
                    {
                        Console.WriteLine("Database Json file not found in the report.");
                        return "";
                    }

                    // check if pg_restore exists on the system
                    var pgRestorePath = "pg_restore"; // assume it's in PATH

                    var pgRestoreProcess = new Process();
                    pgRestoreProcess.StartInfo.FileName = pgRestorePath;
                    pgRestoreProcess.StartInfo.Arguments = $"-f {restoreDbFileName} {dbFileName}";
                    pgRestoreProcess.StartInfo.RedirectStandardOutput = true;
                    pgRestoreProcess.StartInfo.RedirectStandardError = true;
                    pgRestoreProcess.StartInfo.UseShellExecute = false;
                    pgRestoreProcess.Start();

                    string output = pgRestoreProcess.StandardOutput.ReadToEnd();
                    string error = pgRestoreProcess.StandardError.ReadToEnd();
                    pgRestoreProcess.WaitForExit();

                    if (pgRestoreProcess.ExitCode != 0)
                    {
                        Console.WriteLine("pg_restore failed:");
                        Console.WriteLine(error);
                        return "";
                    }
                    else
                    {
                        Console.WriteLine($"Database extracted successfully to '{restoreDbFileName}' database.");
                    }

                    // parse device info from the SQL file
                    string schemaName = GetSchemaName(dbJsonFileName);

                    var deviceInfoEntries = GetDeviceInfoEntries(schemaName, restoreDbFileName);

                    var jsonReady = deviceInfoEntries
                   .Select(x => new Dictionary<string, string> { [x.Item1] = x.Item2 })
                   .ToList();

                    var result = JsonSerializer.Serialize(jsonReady, new JsonSerializerOptions { WriteIndented = true });

                    // cleanup
                    if (File.Exists(dbFileName)) File.Delete(dbFileName);

                    if (File.Exists(dbJsonFileName)) File.Delete(dbJsonFileName);

                    if (File.Exists(restoreDbFileName)) File.Delete(restoreDbFileName);

                    return result;
                }
            }

            return "";
        }




        public static string ParseDeviceInfoXML(Stream stream)
        {
            var DeviceInfo = new List<(string id, string name, string value)>();
            bool fieldsRead = false;

            using (stream)
            using (StreamReader sr = new StreamReader(stream))
            using (XmlReader reader = XmlReader.Create(sr, new XmlReaderSettings { CheckCharacters = false }))
            {
                while (reader.Read())
                {
                    if (reader.Depth == 1 && reader.Name == "metadata" && reader.GetAttribute("section") == "Device Info")
                    {
                        XmlReader attReader = reader.ReadSubtree();
                        XElement attNode = XElement.Load(attReader);

                        IEnumerable<XElement> attributes = attNode.Descendants();

                        foreach (XElement att in attributes)
                        {
                            string id = (string)att.Attribute("id");
                            string name = (string)att.Attribute("name");
                            string value = att.Value;

                            DeviceInfo.Add((id, name, value));
                        }
                        attReader.Close();

                        fieldsRead = true;
                    }

                    if (fieldsRead)
                    {
                        break;
                    }
                }
            }

            var jsonReady = DeviceInfo
           .Select(x => new Dictionary<string, string> { [x.name] = x.value })
           .ToList();

            var result = JsonSerializer.Serialize(jsonReady, new JsonSerializerOptions { WriteIndented = true });

            return result;
        }

        static bool CanExecute(string programName)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = programName,
                        Arguments = "--version",  // oder ein anderer sicherer Parameter
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                process.WaitForExit(3000); // 3 Sekunden Timeout

                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }

        public static string GetSchemaName(String dbJsonFilePath)
        {
            string json = File.ReadAllText(dbJsonFilePath);

            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                JsonElement root = doc.RootElement;
                string deviceId = root.GetProperty("DeviceId").GetString();
                return "device_" + deviceId;
            }
        }

        public static List<Tuple<string, string>> GetDeviceInfoEntries(string schemaName, string dbSqlFilePath)
        {
            string deviceInfoEntriesDataStart = "COPY " + "\"" + schemaName + "\"" + "." + "\"DeviceInfoEntries\"";

            List<string> deviceInfoEntriesData = new List<string>();

            if (File.Exists(dbSqlFilePath))
            {
                //find the positision of the line that starts with "COPY " + "\"" + schemaName + "\"" + "." + "\"DeviceInfoEntries\""
                using (StreamReader sr = new StreamReader(dbSqlFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith(deviceInfoEntriesDataStart))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line.StartsWith("\\.")) // end of COPY data
                                {
                                    break;
                                }
                                deviceInfoEntriesData.Add(line);
                            }
                            // found the line, now we can read the data
                            break;
                        }
                    }
                }
            }

            List<Tuple<string, string>> deviceInfoEntries = new List<Tuple<string, string>>();

            foreach (var entry in deviceInfoEntriesData)
            {
                // split the line by tab character
                var parts = entry.Split('\t');
                if (parts.Length >= 4)
                {
                    // add the first two parts as a tuple to the list
                    deviceInfoEntries.Add(new Tuple<string, string>(parts[2], parts[3]));
                }
            }

            return deviceInfoEntries;
        }
    }
}
