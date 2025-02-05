using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class DeviceInfoEntry : ModelBase, IUfedModelParser<DeviceInfoEntry>
    {
        public static string GetXmlModelType()
        {
            return "DeviceInfoEntry";
        }

        #region fields
        public string UserMapping { get; set; }

        public string Source { get; set; }

        public string EntryName { get; set; }

        public string EntryValue { get; set; }

        public string EntryCategory { get; set; }
        #endregion

        #region Parsers
        public static DeviceInfoEntry ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            DeviceInfoEntry result = new DeviceInfoEntry();
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

                    case "EntryName":
                        result.EntryName = field.Value.Trim();
                        break;

                    case "EntryValue":
                        result.EntryValue = field.Value.Trim();
                        break;

                    case "EntryCategory":
                        result.EntryCategory = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("DeviceInfoEntry Parser: Unknown field: " + field.Attribute("name").Value);
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
                            Logger.LogAttribute("DeviceInfoEntry Parser: Unknown multiField: " + multiField.Attribute("name").Value);
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
                            Logger.LogAttribute("DeviceInfoEntry Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }

        public static List<DeviceInfoEntry> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<DeviceInfoEntry> result = new List<DeviceInfoEntry>();

            IEnumerable<XElement> dieElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "DeviceInfoEntry");

            foreach (XElement dieElement in dieElements)
            {
                DeviceInfoEntry die = ParseModel(dieElement, debugAttributes);
                result.Add(die);
            }

            return result;
        }
        #endregion
    }
}
