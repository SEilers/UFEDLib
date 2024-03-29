﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    public class ProjectAttributeParser
    {
        // parse the report.xml file from the ufdr image to read the basic project data
        public static ProjectAttributes Parse(XmlReader reader)
        {
            ProjectAttributes ufedProjectAttributes = new ProjectAttributes();

            //FileInfo fileInfo = new FileInfo(reportFilePath);
            //FileSizeInBytes = fileInfo.Length;

            var nsmgr = new XmlNamespaceManager(new NameTable());
            nsmgr.AddNamespace("a", "http://pa.cellebrite.com/report/2.0");
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            List<Chat> chats = new List<Chat>();

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
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return ufedProjectAttributes;
        }
    }
}
