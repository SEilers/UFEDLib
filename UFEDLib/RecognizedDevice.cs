using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class RecognizedDevice : ModelBase, IUfedModelParser<RecognizedDevice>
    {
        public static string GetXmlModelType()
        {
            return "RecognizedDevice";
        }

        #region fields
        public string Account { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Source { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region multiModels
        public Dictionary<string, string> AdditionalInfo { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> DeviceIdentifiers { get; set; } = new Dictionary<string, string>();
        #endregion


        #region Parsers
        public static List<RecognizedDevice> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<RecognizedDevice> result = new List<RecognizedDevice>();

            IEnumerable<XElement> recognizedDeviceElements = element.Elements(xNamespace + "model").Where(element => element.Attribute("type").Value == "RecognizedDevice");

            foreach (XElement recognizedDeviceElement in recognizedDeviceElements)
            {
                RecognizedDevice rd = ParseModel(recognizedDeviceElement, debugAttributes);
                result.Add(rd);
            }

            return result;
        }

        public static RecognizedDevice ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            RecognizedDevice result = new RecognizedDevice();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var modelFieldElements = element.Elements(xNamespace + "modelField");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            ParseFields(fieldElements, result, debugAttributes);
            ParseModelFields(modelFieldElements, result, debugAttributes);
            ParseMultiFields(multiFieldElements, result, debugAttributes);
            ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);

            return result;
        }


        public static void ParseFields(IEnumerable<XElement> fieldElements, RecognizedDevice result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "SerialNumber":
                        result.SerialNumber = field.Value.Trim();
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
                            Logger.LogAttribute("RecognizedDevice Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, RecognizedDevice result, bool debugAttributes = false)
        {
            IUfedModelParser<RecognizedDevice>.CheckModelFields<RecognizedDevice>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, RecognizedDevice result, bool debugAttributes = false)
        {
            IUfedModelParser<RecognizedDevice>.CheckMultiFields<RecognizedDevice>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, RecognizedDevice result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "AdditionalInfo":
                        var kvModelsAdditionalInfo = KeyValueModel.ParseMultiModel(multiModelField, debugAttributes);
                        foreach (var kvModel in kvModelsAdditionalInfo)
                        {
                            if (!string.IsNullOrEmpty(kvModel.Key) && !string.IsNullOrEmpty(kvModel.Value))
                            {
                                result.AdditionalInfo[kvModel.Key] = kvModel.Value;
                            }
                        }
                        break;

                    case "DeviceIdentifiers":
                        var kvModelsDeviceIdentifiers = KeyValueModel.ParseMultiModel(multiModelField, debugAttributes);
                        foreach (var kvModel in kvModelsDeviceIdentifiers)
                        {
                            if (!string.IsNullOrEmpty(kvModel.Key) && !string.IsNullOrEmpty(kvModel.Value))
                            {
                                result.DeviceIdentifiers[kvModel.Key] = kvModel.Value;
                            }
                        }
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("RecognizedDevice Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
