using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class DeviceConnectivity : ModelBase, IUfedModelParser<DeviceConnectivity>
    {
        public static string GetXmlModelType()
        {
            return "DeviceConnectivity";
        }

        #region fields
        public string ConnectivityMethod { get; set; }
        public string ConnectivityNature { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public DateTime StartTime { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region multiModels
        public Dictionary<string, string> DeviceIdentifiers { get; set; } = new Dictionary<string, string>();
        #endregion

        #region Parsers
        public static DeviceConnectivity ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            DeviceConnectivity result = new DeviceConnectivity();

            try
            {
                result.ParseAttributes(element);

                var fieldElements = element.Elements(xNamespace + "field");
                var modelFieldElements = element.Elements(xNamespace + "modelField");
                var multiFieldElements = element.Elements(xNamespace + "multiField");
                var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

                ParseFields(fieldElements, result, debugAttributes);
                ParseModelFields(modelFieldElements, result, debugAttributes);
                ParseMultiFields(multiFieldElements, result, debugAttributes);
                ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);
            }
            catch (Exception ex)
            {
                Logger.LogError("DeviceConnectivity: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static List<DeviceConnectivity> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<DeviceConnectivity> result = new List<DeviceConnectivity>();

            IEnumerable<XElement> dcElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "DeviceConnectivity");

            foreach (XElement dcElement in dcElements)
            {
                DeviceConnectivity dc = ParseModel(dcElement, debugAttributes);
                result.Add(dc);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, DeviceConnectivity result, bool debugAttributes = false)
        {
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

                    case "DeviceName":
                        result.DeviceName = field.Value.Trim();
                        break;

                    case "DeviceType":
                        result.DeviceType = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "ConnectivityMethod":
                        result.ConnectivityMethod = field.Value.Trim();
                        break;

                    case "ConnectivityNature":
                        result.ConnectivityNature = field.Value.Trim();
                        break;

                    case "StartTime":
                        if (field.Value.Trim() != "")
                            result.StartTime = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("DeviceConnectivity Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, DeviceConnectivity result, bool debugAttributes = false)
        {
            IUfedModelParser<DeviceConnectivity>.CheckModelFields<DeviceConnectivity>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, DeviceConnectivity result, bool debugAttributes = false)
        {
            IUfedModelParser<DeviceConnectivity>.CheckMultiFields<DeviceConnectivity>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, DeviceConnectivity result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
               
                    case "DeviceIdentifiers":
                        var kvModelsDeviceIdentifiers = KeyValueModel.ParseMultiModel(multiModelField, debugAttributes);
                        foreach (var diModel in kvModelsDeviceIdentifiers)
                        {
                            if (!string.IsNullOrEmpty(diModel.Key) && !string.IsNullOrEmpty(diModel.Value))
                            {
                                result.DeviceIdentifiers[diModel.Key] = diModel.Value;
                            }
                        }
                        break;


                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("DeviceConnectivity Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
