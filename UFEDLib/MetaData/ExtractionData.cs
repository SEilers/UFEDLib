using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UFEDLib
{
    public class ExtractionData
    {
        public static List<(string name, string value)> Parse(String fileName)
        {
            List<(string name, string value)> ExtractionData = null;

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
                        ExtractionData = ParseExtractionData(reportStream);
                        return ExtractionData;
                    }
                }
            }
            else if (fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    ExtractionData = ParseExtractionData(fs);
                    return ExtractionData;
                }
            }
            else
            {
                Console.WriteLine("Unsupported file type: " + fileName);
            }

            // return empty list if file type is unsupported
            return new List<(string name, string value)>();
        }


        public static List<(string name, string value)> ParseExtractionData(Stream stream)
        {
            List<(string name, string value)> result = new List<(string name, string value)>();
            bool fieldsRead = false;

            using (StreamReader sr = new StreamReader(stream))
            {
                using (XmlReader reader = XmlReader.Create(sr, new XmlReaderSettings { CheckCharacters = false }))
                {
                    while (reader.Read())
                    {
                        if (reader.Depth == 1 && reader.Name == "metadata" && reader.GetAttribute("section") == "Extraction Data")
                        {
                            XmlReader attReader = reader.ReadSubtree();
                            XElement attNode = XElement.Load(attReader);

                            IEnumerable<XElement> atributes = attNode.Descendants();

                            foreach (XElement att in atributes)
                            {
                                string name = (string)att.Attribute("name");
                                string value = att.Value;

                                result.Add((name, value));
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

            return result;
        }
    }
}
