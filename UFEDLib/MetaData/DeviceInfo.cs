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
    public class DeviceInfo
    {
        public static List<(string id, string name, string value)> Parse(String fileName)
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
                        DeviceInfo = ParseDeviceInfo(reportStream);
                        return DeviceInfo;
                    }
                }
            }
            else if (fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    DeviceInfo = ParseDeviceInfo(fs);
                    return DeviceInfo;
                }
            }
            else
            {
                Console.WriteLine("Unsupported file type: " + fileName);
            }

            // return empty list if file type is unsupported
            return new List<(string id, string name, string value)>();
        }

        public static List<(string id, string name, string value)> ParseDeviceInfo(Stream stream)
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

            return DeviceInfo;
        }
    }
}
