using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public DateTime LastLaunch { get; set; }

        public TimeSpan LastUsageDuration { get; set; }

        public int LaunchCount { get; set; }

        public string Name { get; set; }

        #endregion

        #region Parsers
        public static ApplicationUsage ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            ApplicationUsage result = new ApplicationUsage();

            var fieldElements = element.Elements(xNamespace + "field");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "ActivationCount":
                        if (field.Value.Trim() != "")
                        {
                            result.ActivationCount = int.Parse(field.Value.Trim());
                        }
                        break;

                    case "Date":
                        if (field.Value.Trim() != "")
                            result.Date = DateTime.Parse(field.Value.Trim());
                        break;

                    case "LastLaunch":
                        if (field.Value.Trim() != "")
                            result.LastLaunch = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "ActiveTime":
                        if (field.Value.Trim() != "")
                            result.ActiveTime = TimeSpan.Parse(field.Value.Trim());
                        break;

                    case "LastUsageDuration":
                        if (field.Value.Trim() != "")
                            result.LastUsageDuration = TimeSpan.Parse(field.Value.Trim());
                        break;

                    case "BackgroundTime":
                        if (field.Value.Trim() != "")
                            result.BackgroundTime = TimeSpan.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("ApplicationUsage Parser: Unknown field: " + field.Attribute("name").Value);
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
                            Console.WriteLine("ApplicationUsage Parser:Unknown multiField: " + multiField.Attribute("name").Value);
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
                            Console.WriteLine("ApplicationUsage Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }

        public static List<ApplicationUsage> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<ApplicationUsage> result = new List<ApplicationUsage>();

            IEnumerable<XElement> applicationUsageElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "ApplicationUsage");

            foreach (var applicationUsageElement in applicationUsageElements)
            {
                try
                {
                    result.Add(ParseModel(applicationUsageElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing ApplicationUsage: " + ex.Message);
                }
            }

            return result;
        }

        #endregion
    }
}
