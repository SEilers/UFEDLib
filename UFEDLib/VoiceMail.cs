using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Voicemail : ModelBase, IUfedModelParser<Voicemail>
    {
        public static string GetXmlModelType()
        {
            return "Voicemail";
        }

        #region fields
        public TimeSpan Duration { get; set; }
        public DateTime LastModified { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; }
        public string UserMapping { get; set; }
        public string WasPlayed { get; set; }
        #endregion

        #region models
        public Party From { get; set; }
        #endregion


        #region Parsers
        public static Voicemail ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Voicemail result = new Voicemail();
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

        public static List<Voicemail> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Voicemail> result = new List<Voicemail>();

            IEnumerable<XElement> vmElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "VoiceMail");

            foreach (XElement vmElement in vmElements)
            {
                Voicemail vm = ParseModel(vmElement, debugAttributes);
                result.Add(vm);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Voicemail result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Duration":
                        if (field.Value.Trim() != "")
                            result.Duration = TimeSpan.Parse(field.Value.Trim());
                        break;

                    case "LastModified":
                        if (field.Value.Trim() != "")
                            result.LastModified = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Timestamp":
                        if (field.Value.Trim() != "")
                            result.Timestamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Type":
                        result.Type = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "WasPlayed":
                        result.WasPlayed = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Voicemail Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Voicemail result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    case "From":
                        result.From = Party.ParseModel(modelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Voicemail Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Voicemail result, bool debugAttributes = false)
        {
            IUfedModelParser<Voicemail>.CheckMultiFields<Voicemail>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Voicemail result, bool debugAttributes = false)
        {
            IUfedModelParser<Voicemail>.CheckMultiModelFields<Voicemail>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
