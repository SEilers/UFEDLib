using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class PoweringEvent : ModelBase, IUfedModelParser<PoweringEvent>
    {
        public static string GetXmlModelType()
        {
            return "PoweringEvent";
        }

        #region fields
        public string Description { get; set; } = "";
        public string Element { get; set; } = "";
        public string Event { get; set; } = "";
        public string Source { get; set; } = "";
        public DateTime TimeStamp { get; set; } = DateTime.MinValue;
        public string UserMapping { get; set; } = "";
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion

        #region Parsers
        public static PoweringEvent ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<PoweringEvent>(element, debugAttributes);
        }

        public static List<PoweringEvent> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<PoweringEvent>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, PoweringEvent result, bool debugAttributes = false)
        {
            foreach (XElement field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "Description":
                        result.Description = field.Value.Trim();
                        break;

                    case "Element":
                        result.Element = field.Value.Trim();
                        break;

                    case "Event":
                        result.Event = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("PoweringEvent Parser: Unknown field: " + fieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, PoweringEvent result, bool debugAttributes = false)
        {
            IUfedModelParser<PoweringEvent>.CheckModelFields<PoweringEvent>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, PoweringEvent result, bool debugAttributes = false)
        {
            IUfedModelParser<PoweringEvent>.CheckMultiFields<PoweringEvent>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, PoweringEvent result, bool debugAttributes = false)
        {
            IUfedModelParser<PoweringEvent>.CheckMultiModelFields<PoweringEvent>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
