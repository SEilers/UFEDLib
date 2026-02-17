using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Recording : ModelBase, IUfedModelParser<Recording>
    {
        public static string GetXmlModelType()
        {
            return "Recording";
        }

        #region fields
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;
        public DateTime TimeStamp { get; set; } = DateTime.MinValue;
        public string Title { get; set; } = "";
        public string Type { get; set; } = "";
        #endregion

        #region Parsers
        public static Recording ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Recording>(element, debugAttributes);
        }

        public static List<Recording> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Recording>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Recording result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
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
                            Logger.LogAttribute("Recording Parser: Unknown field: " + fieldName);
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
