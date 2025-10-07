using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
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
            return DefaultModelParser<DeviceConnectivity>(element, debugAttributes);
        }

        public static List<DeviceConnectivity> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<DeviceConnectivity>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, DeviceConnectivity result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "ConnectivityMethod":
                        result.ConnectivityMethod = field.Value.Trim();
                        break;

                    case "ConnectivityNature":
                        result.ConnectivityNature = field.Value.Trim();
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

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "StartTime":
                        if (field.Value.Trim() != "")
                            result.StartTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
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
