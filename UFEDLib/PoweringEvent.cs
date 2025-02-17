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
        public string Description { get; set; }
        public String Element { get; set; }
        public String Event { get; set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion

        #region Parsers
        public static PoweringEvent ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            PoweringEvent result = new PoweringEvent();

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
                Logger.LogError("PoweringEvent: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static List<PoweringEvent> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<PoweringEvent> result = new List<PoweringEvent>();

            IEnumerable<XElement> poweringEventElements = element.Elements(xNamespace + "model").Where(element => element.Attribute("type").Value == "PoweringEvent");

            foreach (XElement poweringEventElement in poweringEventElements)
            {
                PoweringEvent im = ParseModel(poweringEventElement, debugAttributes);
                result.Add(im);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, PoweringEvent result, bool debugAttributes = false)
        {
            foreach (XElement field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Description":
                        result.Description = field.Value.Trim();
                        break;

                    case "Element":
                        result.Element = field.Value.Trim();
                        break;

                    case "Event":
                        result.Event = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("PoweringEvent Parser: Unknown field: " + field.Attribute("name").Value);
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
