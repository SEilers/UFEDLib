using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Note : ModelBase, IUfedModelParser<Note>
    {
        public static string GetXmlModelType()
        {
            return "Note";
        }

        #region fields
        public string Account { get; set; }
        public string Body { get; set; }
        public DateTime Creation { get; set; }
        public string Folder { get; set; }
        public DateTime Modification { get; set; }
        public string PositionAddress { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        public StreetAddress Address { get; set; }
        public Coordinate Position { get; set; }
        #endregion

        #region multiModels
        public List<Attachment> Attachments { get; set; }
        public List<Party> Participants { get; set; } = new List<Party>();
        #endregion

        #region Parsers


        public static Note ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Note>(element, debugAttributes);
        }

        public static List<Note> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Note>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Note result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Body":
                        result.Body = field.Value.Trim();
                        break;

                    case "Creation":
                        if (field.Value.Trim() != "")
                            result.Creation = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Folder":
                        result.Folder = field.Value.Trim();
                        break;

                    case "Modification":
                        if (field.Value.Trim() != "")
                            result.Modification = DateTime.Parse(field.Value.Trim());
                        break;

                    case "PositionAddress":
                        result.PositionAddress = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Summary":
                        result.Summary = field.Value.Trim();
                        break;

                    case "Title":
                        result.Title = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Note Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Note result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    case "Address":
                        result.Address = StreetAddress.ParseModel(modelField, debugAttributes);
                        break;

                    case "Position":
                        result.Position = Coordinate.ParseModel(modelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Note Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Note result, bool debugAttributes = false)
        {
            IUfedModelParser<Note>.CheckMultiFields<Note>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Note result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "Attachments":
                        result.Attachments = Attachment.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Participants":
                        result.Participants = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Note Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
