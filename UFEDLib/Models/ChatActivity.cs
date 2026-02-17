using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class ChatActivity : ModelBase, IUfedModelParser<ChatActivity>
    {
        public static string GetXmlModelType()
        {
            return "ChatActivity";
        }

        #region fields
        public string Action { get; set; } = "";
        public string SystemMessageBody { get; set; } = "";
        public string SystemMessageId { get; set; } = "";
        public DateTime SystemMessageTimeStamp { get; set; } = DateTime.MinValue;
        #endregion

        #region models
        public Party? Participant { get; set; } = null;
        #endregion

        #region parsers
        public static ChatActivity ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<ChatActivity>(element, debugAttributes);
        }

        public static List<ChatActivity> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<ChatActivity>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, ChatActivity result, bool debugAttributes = false)
        {


            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "Action":
                        result.Action = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "SystemMessageBody":
                        result.SystemMessageBody = field.Value.Trim();
                        break;

                    case "SystemMessageId":
                        result.SystemMessageId = field.Value.Trim();
                        break;

                    case "SystemMessageTimeStamp":
                        if (field.Value.Trim() != "")
                            result.SystemMessageTimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ChatActivity Parser: Unknown field: " + fieldName);
                        }
                        break;
                }
            }

        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, ChatActivity result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                XNamespace ns = modelField.Name.Namespace;
                XElement? modelElement = modelField.Element(ns + "model");
                if (modelElement == null) continue;

                var modelFieldName = ModelBase.GetAttributeValueOrLog(modelField, "name", debugAttributes);
                if (modelFieldName == null) continue;

                switch (modelFieldName)
                {
                    case "Participant":
                        result.Participant = Party.ParseModel(modelElement, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ChatActivity Parser: Unknown modelField: " + modelFieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, ChatActivity result, bool debugAttributes = false)
        {
            IUfedModelParser<ChatActivity>.CheckMultiFields<ChatActivity>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, ChatActivity result, bool debugAttributes = false)
        {
            IUfedModelParser<ChatActivity>.CheckMultiModelFields<ChatActivity>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}