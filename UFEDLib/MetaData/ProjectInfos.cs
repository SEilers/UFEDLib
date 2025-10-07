using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UFEDLib
{
    public class ProjectInfos
    {
        public string projectId = "";
        public string name = "";
        public string reportVersion = "";
        public string licenseId = "";
        public string containsGarbage = "";
        public string extractionType = "";
        public string ProjectNodeCount = "";
        public string ProjectModelCount = "";
        public string MediaResults = "";
        public string xmlns = "";

        public static ProjectInfos Parse(string fileName)
        {
            ProjectInfos projectInfos = null;

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
                        projectInfos = ParseProjectInfos(reportStream);
                        return projectInfos;
                    }
                }
            }
            else if (fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    projectInfos = ParseProjectInfos(fs);
                    return projectInfos;
                }
            }
            else
            {
                Console.WriteLine("Unsupported file type: " + fileName);
            }

            return projectInfos;
        }

        public static ProjectInfos ParseProjectInfos(Stream stream)
        {
            ProjectInfos result = new ProjectInfos();

            try
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    using (XmlReader reader = XmlReader.Create(sr, new XmlReaderSettings { CheckCharacters = false }))
                    {
                        bool extracted = false;

                        while (reader.Read())
                        {
                            if (reader.Depth == 0 && reader.Name == "project" && reader.IsStartElement())
                            {
                                result.projectId = reader.GetAttribute("id");
                                result.name = reader.GetAttribute("name");
                                result.reportVersion = reader.GetAttribute("reportVersion");
                                result.licenseId = reader.GetAttribute("licenseID");
                                result.containsGarbage = reader.GetAttribute("containsGarbage");
                                result.extractionType = reader.GetAttribute("extractionType");
                                result.ProjectNodeCount = reader.GetAttribute("NodeCount");
                                result.ProjectModelCount = reader.GetAttribute("ModelCount");
                                result.MediaResults = reader.GetAttribute("MediaResults");
                                result.xmlns = reader.GetAttribute("xmlns");

                                extracted = true;
                            }

                            if (extracted) { break; }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing project info: " + ex.Message);
                return null;
            }

            return result;
        }
    }
}
