using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class VoiceMail : ModelBase, IUfedModelParser<VoiceMail>
    {
        public static string GetXmlModelType()
        {
            return "VoiceMail";
        }

        #region fields
        public TimeSpan Duration { get; set; }
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }
        #endregion

        #region models
        public Party From { get; set; }
        #endregion


        #region Parsers
        public static VoiceMail ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            VoiceMail result = new VoiceMail();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var modelFieldElements = element.Elements(xNamespace + "modelField");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Duration":
                        if (field.Value.Trim() != "")
                            result.Duration = TimeSpan.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("VoiceMail Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("VoiceMail Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "From":
                        result.From = Party.ParseModel(multiField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("VoiceMail Parser: Unknown multiField: " + multiField.Attribute("name").Value);
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
                            Logger.LogAttribute("VoiceMail Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }

        public static List<VoiceMail> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<VoiceMail> result = new List<VoiceMail>();

            IEnumerable<XElement> vmElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "VoiceMail");

            foreach (XElement vmElement in vmElements)
            {
                VoiceMail vm = ParseModel(vmElement, debugAttributes);
                result.Add(vm);
            }

            return result;
        }
        #endregion
    }
}
