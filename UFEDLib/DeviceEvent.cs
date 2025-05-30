﻿using System;
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
            return DefaultModelParser<DeviceEvent>(element, debugAttributes);
        }

        public static List<DeviceEvent> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<DeviceEvent>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, DeviceEvent result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "EventType":
                        result.EventType = field.Value.Trim();
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

                    case "Value":
                        result.Value = field.Value.Trim();
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
