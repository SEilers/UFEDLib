﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class ApplicationUsage : ModelBase, IUfedModelParser<ApplicationUsage>
    {
        public static string GetXmlModelType()
        {
            return "ApplicationUsage";
        }

        #region fields
        public int ActivationCount { get; set; }
        public TimeSpan ActiveTime { get; set; }
        public TimeSpan BackgroundTime { get; set; }
        public DateTime Date { get; set; }
        public DateTime EndTime { get; set; }
        public string Identifier { get; set; }
        public DateTime LastLaunch { get; set; }
        public TimeSpan LastUsageDuration { get; set; }
        public int LaunchCount { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public string Source { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region Parsers
        public static ApplicationUsage ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<ApplicationUsage>(element, debugAttributes);
        }

        public static List<ApplicationUsage> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<ApplicationUsage>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, ApplicationUsage result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "ActivationCount":
                        if (field.Value.Trim() != "")
                        {
                            if( int.TryParse(field.Value.Trim(), out int activationCount))
                                result.ActivationCount = activationCount;
                        }
                        break;

                    case "ActiveTime":
                        if (field.Value.Trim() != "")
                            result.ActiveTime = TimeSpan.Parse(field.Value.Trim());
                        break;

                    case "BackgroundTime":
                        if (field.Value.Trim() != "")
                            result.BackgroundTime = TimeSpan.Parse(field.Value.Trim());
                        break;

                    case "Date":
                        if (field.Value.Trim() != "")
                            result.Date = DateTime.Parse(field.Value.Trim());
                        break;

                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Identifier":
                        result.Identifier = field.Value.Trim();
                        break;

                    case "LastLaunch":
                        if (field.Value.Trim() != "")
                            result.LastLaunch = DateTime.Parse(field.Value.Trim());
                        break;

                    case "LastUsageDuration":
                        if (field.Value.Trim() != "")
                            result.LastUsageDuration = TimeSpan.Parse(field.Value.Trim());
                        break;

                    case "LaunchCount":
                        if (field.Value.Trim() != "")
                        {
                            if( int.TryParse(field.Value.Trim(), out int launchCount) )
                                result.LaunchCount = launchCount;
                        }
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "StartTime":
                        if (field.Value.Trim() != "")
                            result.StartTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ApplicationUsage Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, ApplicationUsage result, bool debugAttributes = false)
        {
            IUfedModelParser<ApplicationUsage>.CheckModelFields<ApplicationUsage>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, ApplicationUsage result, bool debugAttributes = false)
        {
            IUfedModelParser<ApplicationUsage>.CheckMultiFields<ApplicationUsage>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, ApplicationUsage result, bool debugAttributes = false)
        {
            IUfedModelParser<ApplicationUsage>.CheckMultiModelFields<ApplicationUsage>(multiModelFieldElements, debugAttributes);
        }

        #endregion
    }
}
