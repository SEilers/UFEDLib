﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class AppsUsageLog : ModelBase, IUfedModelParser<AppsUsageLog>
    {
        public static string GetXmlModelType()
        {
            return "AppsUsageLog";
        }

        #region fields
        public string UserMapping { get; set; }
        public string Source { get; set; }
        public string ServiceIdentifier { get; set; }
        public string SubModule { get; set; }
        public string Action { get; set; }
        public string Identifier { get; set; }
        public string ArtifactFamily { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        #endregion

        #region Parsers
        public static AppsUsageLog ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            AppsUsageLog result = new AppsUsageLog();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "SubModule":
                        result.SubModule = field.Value.Trim();
                        break;

                    case "Action":
                        result.Action = field.Value.Trim();
                        break;

                    case "Identifier":
                        result.Identifier = field.Value.Trim();
                        break;

                    case "ArtifactFamily":
                        result.ArtifactFamily = field.Value.Trim();
                        break;

                    case "StartTime":
                        if (field.Value.Trim() != "")
                            result.StartTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("AppsUsageLog Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("AppsUsageLog Parser: Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("AppsUsageLog Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }

        public static List<AppsUsageLog> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<AppsUsageLog> result = new List<AppsUsageLog>();

            IEnumerable<XElement> auElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "AppsUsageLog");

            foreach (XElement auElement in auElements)
            {
                AppsUsageLog aul = ParseModel(auElement, debugAttributes);
                result.Add(aul);
            }

            return result;
        }

        #endregion
    }
}
