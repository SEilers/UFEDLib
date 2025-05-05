using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class ChatActivity : ModelBase, IUfedModelParser<ChatActivity>
    {
        public static string GetXmlModelType()
        {
            return "ChatActivity";
        }

        #region fields
        public string Action { get; set; }
        public string Source { get; set; }
        public string SystemMessageBody { get; set; }
        public string SystemMessageId { get; set; }
        public DateTime SystemMessageTimeStamp { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        public Party Participant { get; set; }
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
                switch (field.Attribute("name").Value)
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
                            Logger.LogAttribute("ChatActivity Parser: Unknown field: " + field.Attribute("name").Value);
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
                XElement modelElement = modelField.Element(ns + "model");

                switch (modelField.Attribute("name").Value)
                {
                    case "Participant":
                        result.Participant = Party.ParseModel(modelElement, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ChatActivity Parser: Unknown modelField: " + modelField.Attribute("name").Value);
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