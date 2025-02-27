﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace UFEDLib
{
    [Serializable]
    public class ProjectAttributes
    {
        // Image File Attributes
        public String FileName { get; set; }

        public double FileSizeInBytes { get; set; }

        // UFED Project Attributes

        public String ProjectName { get; set; }

        public string ProjectId { get; set; }

        public String ReportVersion { get; set; }

        public int NodeCount { get; set; }

        public int ModelCount { get; set; }

        public String SourceExtractionsDeviceName { get; set; }

        public String SourceExtractionsFullName { get; set; }

        public List<Tuple<string, string>> CaseInformation { get; set; } = new List<Tuple<string, string>>();

        public List<Tuple<string, string>> AdditionalFields { get; set; } = new List<Tuple<string, string>>();

        public List<Tuple<string, string>> ExtractionData { get; set; } = new List<Tuple<string, string>>();

        public List<Tuple<string, string>> DeviceInfo { get; set; } = new List<Tuple<string, string>>();

        public static ProjectAttributes Parse(string filename, IProgress<int> progress = null)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found: " + filename);
                return null;
            }

            FileInfo fileInfo = new FileInfo(filename);
            long reportSize = fileInfo.Length;
            long currentPosition = 0;
            int lastPercent = 0;

            try
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
                        using (StreamReader sr = new StreamReader(reportStream))
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

                                                ufedProjectAttributes.AdditionalFields.Add(Tuple.Create(name, value));
                                            }
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

                                                ufedProjectAttributes.ExtractionData.Add(Tuple.Create(name, value));
                                            }
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

                                                ufedProjectAttributes.DeviceInfo.Add(Tuple.Create(name, value));
                                            }
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

                                                ufedProjectAttributes.CaseInformation.Add(Tuple.Create(name, value));
                                            }
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
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }

            return null;
        }
    }
}
