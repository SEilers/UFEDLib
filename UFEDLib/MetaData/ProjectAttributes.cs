using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UFEDLib
{
    [Serializable]
    public class ProjectAttributes
    {
        // Image File Attributes
        public string FileName { get; set; }

        public double FileSizeInBytes { get; set; }

        // UFED Project Attributes

        public string ProjectName { get; set; }

        public string ProjectId { get; set; }

        public string ReportVersion { get; set; }

        public int NodeCount { get; set; }

        public int ModelCount { get; set; }

        public List<(string name, string value)> CaseInformation { get; set; } = new List<(string name, string value)>();

        public List<(string name, string value)> AdditionalFields { get; set; } = new List<(string name, string value)>();

        public List<(string name, string value)> ExtractionData { get; set; } = new List<(string name, string value)>();

        public List<(string name, string value)> DeviceInfo { get; set; } = new List<(string name, string value)>();

        public static ProjectAttributes Parse(string filename, IProgress<int> progress = null)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found: " + filename);
                return null;
            }

            FileInfo fileInfo = new FileInfo(filename);
            long reportSize = fileInfo.Length;

            try
            {
                if(Path.GetExtension(filename).ToLower() == ".ufdr")
                {
                    using (ZipArchive zip = ZipFile.OpenRead(filename))
                    {
                        var report = zip.GetEntry("report.xml");

                        if (report == null)
                        {
                            Console.WriteLine("report.xml not found in the ufdr file");
                            return null;
                        }
                       
                        using (Stream reportStream = report.Open())
                        {
                            return ParseProjectAttributes(reportStream, reportSize, progress);
                        }
                    }
                }
                else if (Path.GetExtension(filename).ToLower() == ".xml")
                {
                    using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        return ParseProjectAttributes(fs, reportSize, progress);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }

            return null;
        }

        private static ProjectAttributes ParseProjectAttributes( Stream stream, long reportSize, IProgress<int> progress = null)
        {
            long currentPosition = 0;
            int lastPercent = 0;


            using (StreamReader sr = new StreamReader(stream))
            {
                XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
                {
                    CheckCharacters = false
                };

                using (XmlReader reader = XmlReader.Create(sr, xmlReaderSettings))
                {
                    ProjectAttributes ufedProjectAttributes = new ProjectAttributes();

                    var nsmgr = new XmlNamespaceManager(new NameTable());
                    nsmgr.AddNamespace("a", "http://pa.cellebrite.com/report/2.0");
                    XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

                    bool readProjectAttributes = false;
                    bool readAddítionalFields = false;
                    bool readExtractionData = false;
                    bool readDeviceInfo = false;
                    bool readCaseInformation = false;

                    while (reader.Read())
                    {
                        try
                        {
                            if (reader.Name == "project" && reader.NodeType != XmlNodeType.EndElement)
                            {
                                ufedProjectAttributes.ProjectId = reader.GetAttribute("id");
                                ufedProjectAttributes.ProjectName = reader.GetAttribute("name");
                                ufedProjectAttributes.NodeCount = int.Parse(reader.GetAttribute("NodeCount"));
                                ufedProjectAttributes.ModelCount = int.Parse(reader.GetAttribute("ModelCount"));
                                ufedProjectAttributes.ReportVersion = reader.GetAttribute("reportVersion");
                                readProjectAttributes = true;
                            }

                            if (reader.Depth == 1 && reader.Name == "metadata" && reader.GetAttribute("section") == "Additional Fields")
                            {
                                XmlReader attReader = reader.ReadSubtree();
                                XElement attNode = XElement.Load(attReader);

                                IEnumerable<XElement> atributes = attNode.Descendants();

                                foreach (XElement att in atributes)
                                {
                                    string name = (string)att.Attribute("name");
                                    string value = att.Value;

                                    ufedProjectAttributes.AdditionalFields.Add((name, value));
                                }
                                readAddítionalFields = true;

                            }
                            else if (reader.Depth == 1 && reader.Name == "metadata" && reader.GetAttribute("section") == "Extraction Data")
                            {
                                XmlReader attReader = reader.ReadSubtree();
                                XElement attNode = XElement.Load(attReader);

                                IEnumerable<XElement> atributes = attNode.Descendants();

                                foreach (XElement att in atributes)
                                {
                                    string name = (string)att.Attribute("name");
                                    string value = att.Value;

                                    ufedProjectAttributes.ExtractionData.Add((name, value));
                                }

                                readExtractionData = true;
                            }
                            else if (reader.Depth == 1 && reader.Name == "metadata" && reader.GetAttribute("section") == "Device Info")
                            {
                                XmlReader attReader = reader.ReadSubtree();
                                XElement attNode = XElement.Load(attReader);

                                IEnumerable<XElement> atributes = attNode.Descendants();

                                foreach (XElement att in atributes)
                                {
                                    string name = (string)att.Attribute("name");
                                    string value = att.Value;

                                    ufedProjectAttributes.DeviceInfo.Add((name, value));
                                }

                                readDeviceInfo = true;
                            }
                            else if (reader.Depth == 1 && reader.Name == "caseInformation")
                            {
                                XmlReader attReader = reader.ReadSubtree();
                                XElement attNode = XElement.Load(attReader);

                                IEnumerable<XElement> atributes = attNode.Descendants();

                                foreach (XElement att in atributes)
                                {
                                    string name = (string)att.Attribute("name");
                                    string value = att.Value;

                                    ufedProjectAttributes.CaseInformation.Add((name, value));
                                }

                                readCaseInformation = true;
                            }

                            if (progress != null)
                            {
                                if (reportSize > 0)
                                {
                                    currentPosition = sr.BaseStream.Position;
                                    int percent = (int)((double)currentPosition / reportSize * 100);
                                    if (percent > lastPercent)
                                    {
                                        lastPercent = percent;
                                        progress.Report(percent);
                                    }
                                }
                            }

                            if (readProjectAttributes && readAddítionalFields && readExtractionData && readDeviceInfo && readCaseInformation)
                            {
                                break;
                            }

                            // Stop parsing if we reach the decodedData section - no more metadate from here on
                            if (reader.Depth == 1 && reader.Name == "decodedData")
                            {
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error parsing project attributes: " + ex.Message);
                        }
                    }

                    if (progress != null)
                    {
                        progress.Report(100);
                    }
                    return ufedProjectAttributes;
                }
            }
        }
    }
}
