using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class DeviceEvent : ModelBase, IUfedModelParser<DeviceEvent>
    {
        public static string GetXmlModelType()
        {
            return "DeviceEvent";
        }

        #region fields
        public DateTime EndTime { get; set; }
        public string EventType { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public DateTime StartTime { get; set; }
        public string UserMapping { get; set; }
        public string Value { get; set; }
        #endregion

        #region multiModels
        public Dictionary<string, string> AdditionalInfo { get; set; } = new Dictionary<string, string>();
        #endregion


        #region Parsers
        public static DeviceEvent ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            DeviceEvent result = new DeviceEvent();
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
                Logger.LogError("DeviceEvent: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static List<DeviceEvent> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<DeviceEvent> result = new List<DeviceEvent>();

            IEnumerable<XElement> deElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "DeviceEvent");

            foreach (XElement deElement in deElements)
            {
                DeviceEvent aul = ParseModel(deElement, debugAttributes);
                result.Add(aul);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, DeviceEvent result, bool debugAttributes = false)
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

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Value":
                        result.Value = field.Value.Trim();
                        break;

                    case "EventType":
                        result.EventType = field.Value.Trim();
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
                            Logger.LogAttribute("DeviceEvent Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, DeviceEvent result, bool debugAttributes = false)
        {
            IUfedModelParser<DeviceEvent>.CheckModelFields<DeviceEvent>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, DeviceEvent result, bool debugAttributes = false)
        {
            IUfedModelParser<DeviceEvent>.CheckMultiFields<DeviceEvent>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, DeviceEvent result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "Additional_Info":
                        var kvModelsAdditionalInfo = KeyValueModel.ParseMultiModel(multiModelField, debugAttributes);
                        foreach (var kvModel in kvModelsAdditionalInfo)
                        {
                            if (!string.IsNullOrEmpty(kvModel.Key) && !string.IsNullOrEmpty(kvModel.Value))
                            {
                                result.AdditionalInfo[kvModel.Key] = kvModel.Value;
                            }
                        }
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("DeviceEvent Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        #endregion
    }
}
