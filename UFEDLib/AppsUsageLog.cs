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
        public string Action { get; set; }
        public string ArtifactFamily { get; set; }
        public DateTime EndTime { get; set; }
        public string Identifier { get; set; }
        public string ServiceIdentifier { get; set; }
        public DateTime StartTime { get; set; }
        public string Source { get; set; }
        public string SubModule { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region multiField
        public List<string> AdditionalInfo { get; set; }
        #endregion

        #region Parsers
        public static AppsUsageLog ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<AppsUsageLog>(element, debugAttributes);
        }

        public static List<AppsUsageLog> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<AppsUsageLog>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, AppsUsageLog result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Action":
                        result.Action = field.Value.Trim();
                        break;

                    case "ArtifactFamily":
                        result.ArtifactFamily = field.Value.Trim();
                        break;

                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Identifier":
                        result.Identifier = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "StartTime":
                        if (field.Value.Trim() != "")
                            result.StartTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "SubModule":
                        result.SubModule = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("AppsUsageLog Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, AppsUsageLog result, bool debugAttributes = false)
        {
            IUfedModelParser<AppsUsageLog>.CheckModelFields<AppsUsageLog>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, AppsUsageLog result, bool debugAttributes = false)
        {
            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "AdditionalInfo":
                        result.AdditionalInfo = multiField.Elements().Select(x => x.Value).ToList();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("AppsUsageLog Parser: Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, AppsUsageLog result, bool debugAttributes = false)
        {
            IUfedModelParser<AppsUsageLog>.CheckMultiModelFields<AppsUsageLog>(multiModelFieldElements, debugAttributes);
        }

        #endregion
    }
}
