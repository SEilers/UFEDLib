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
        public string Source { get; set; }
        public string UserMapping { get; set; }
        public String Element { get; set; }
        public String Event { get; set; }
        public DateTime TimeStamp { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion

        #region Parsers
        public static PoweringEvent ParseModel(XElement element, bool debugAttributes = false)
        {
            PoweringEvent result = new PoweringEvent();
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            try
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
                                Console.WriteLine("PoweringEvent Parser: Unknown field: " + field.Attribute("name").Value);
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
                                Console.WriteLine("PoweringEvent Parser: Unknown multiField: " + multiField.Attribute("name").Value);
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
                                Console.WriteLine("PoweringEvent Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
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
        #endregion
    }
}
