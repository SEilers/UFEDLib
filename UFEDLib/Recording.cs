using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class Recording : ModelBase, IUfedModelParser<Recording>
    {
        public static string GetXmlModelType()
        {
            return "Recording";
        }

        #region fields
        public TimeSpan Duration { get; set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region Parsers
        public static Recording ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Recording result = new Recording();

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
                Logger.LogError("Recording: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static List<Recording> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Recording> result = new List<Recording>();

            IEnumerable<XElement> recordingElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Recording");

            foreach (XElement recordingElement in recordingElements)
            {
                Recording re = ParseModel(recordingElement, debugAttributes);
                result.Add(re);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Recording result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Duration":
                        if (field.Value.Trim() != "")
                            result.Duration = TimeSpan.Parse(field.Value.Trim());
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Title":
                        result.Title = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Type":
                        result.Type = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Recording Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Recording result, bool debugAttributes = false)
        {
            IUfedModelParser<Recording>.CheckModelFields<Recording>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Recording result, bool debugAttributes = false)
        {
            IUfedModelParser<Recording>.CheckMultiFields<Recording>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Recording result, bool debugAttributes = false)
        {
            IUfedModelParser<Recording>.CheckMultiModelFields<Recording>(multiModelFieldElements, debugAttributes);
        }

        #endregion
    }
}
