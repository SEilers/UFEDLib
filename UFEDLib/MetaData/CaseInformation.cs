using System;
using System.Collections.Generic;
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
    public class CaseInformation
    {
        public static List<(string name, string value)> Parse(String fileName)
        {
            if (fileName.EndsWith(".ufdr", StringComparison.OrdinalIgnoreCase))
            {
                using (ZipArchive zip = ZipFile.OpenRead(fileName))
                {
                    var report = zip.GetEntry("report.xml");

                    if (report == null)
                    {
                        throw new FileNotFoundException("report.xml not found in the ufdr file");
                    }

                    using (Stream reportStream = report.Open())
                    {
                        return ParseCaseInformation(reportStream);
                    }
                }
            }
            else if (fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    return ParseCaseInformation(fs);
                }
            }
            else
            {
                throw new NotSupportedException("Unsupported file type: " + fileName);
            }
        }

        public static string ParseToJson(String fileName)
        {
            var nameValueList = Parse(fileName);
            var result = JsonSerializer.Serialize(nameValueList.ToDictionary(x => x.name, x => x.value), new JsonSerializerOptions { WriteIndented = true });
            return result;
        }


        public static List<(string name, string value)> ParseCaseInformation(Stream stream)
        {
            List<(string name, string value)> nameValueList = new List<(string name, string value)>();

            bool fieldsRead = false;

            using (StreamReader sr = new StreamReader(stream))
            {
                using (XmlReader reader = XmlReader.Create(sr, new XmlReaderSettings { CheckCharacters = false }))
                {
                    while (reader.Read())
                    {
                        if (reader.Depth == 1 && reader.Name == "caseInformation")
                        {
                            XmlReader attReader = reader.ReadSubtree();
                            XElement attNode = XElement.Load(attReader);

                            IEnumerable<XElement> atributes = attNode.Descendants();

                            foreach (XElement att in atributes)
                            {
                                string? name = att.Attribute("name")?.Value;
                                string value = att.Value;

                                nameValueList.Add((name ?? string.Empty, value));
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
            }

            return nameValueList;
        }
    }
}
