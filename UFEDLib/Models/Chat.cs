using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Chat : ModelBase, IUfedModelParser<Chat>
    {
        public static string GetXmlModelType()
        {
            return "Chat";
        }

        #region fields
        public string Account { get; set; }
        public string ChatType { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public DateTime LastActivity { get; set; }
        public string Name { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public DateTime StartTime { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        public List<InstantMessage> Messages { get; set; } = new List<InstantMessage>();
        public List<Party> Participants { get; set; } = new List<Party>();
        public List<ContactPhoto> Photos { get; set; } = new List<ContactPhoto>();
        #endregion

        #region Parsers

        public static Chat ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Chat>(element, debugAttributes);
        }
        public static List<Chat> ParseMultiModel(XElement chatsElement, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Chat>(chatsElement, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Chat result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "ChatType":
                        result.ChatType = field.Value.Trim();
                        break;

                    case "Description":
                        result.Description = field.Value.Trim();
                        break;

                    case "Id":
                        result.Id = field.Value.Trim();
                        break;

                    case "LastActivity":
                        if (field.Value.Trim() != "")
                            result.LastActivity = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
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

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Chat Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Chat result, bool debugAttributes = false)
        {
            IUfedModelParser<Chat>.CheckModelFields<Chat>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Chat result, bool debugAttributes = false)
        {
            IUfedModelParser<Chat>.CheckMultiFields<Chat>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Chat result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "ActivityLog":
                        //TODO: Parse ActivityLog
                        //result.ActivityLog = ActivityLogParser.ParseActivityLogs(multiModelField, debugAttributes);
                        break;

                    case "Messages":
                        result.Messages = InstantMessage.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Participants":
                        result.Participants = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Photos":
                        result.Photos = ContactPhoto.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Chat Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}