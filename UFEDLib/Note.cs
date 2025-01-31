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
        public string UserMapping { get; set; }
        public string Source { get; set; }
        public string Body { get; set; }
        public DateTime Creation { get; set; }
        public string Folder { get; set; }
        public DateTime Modification { get; set; }
        public string PositionAddress { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        #endregion

        #region models
        public Coordinate Position { get; set; }

        public StreetAddress Address { get; set; }
        #endregion

        #region multiModels
        public List<Attachment> Attachments { get; set; }


        #endregion

        #region Parsers
        

        public static Note ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Note result = new Note();

            var fieldElements = element.Elements(xNamespace + "field");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Body":
                        result.Body = field.Value.Trim();
                        break;

                    case "Folder":
                        result.Folder = field.Value.Trim();
                        break;

                    case "PositionAddress":
                        result.PositionAddress = field.Value.Trim();
                        break;
      
                    case "Summary":
                        result.Summary = field.Value.Trim();
                        break;

                    case "Title":
                        result.Title = field.Value.Trim();
                        break;

                    case "Creation":
                        if (field.Value.Trim() != "")
                            result.Creation = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Modification":
                        if (field.Value.Trim() != "")
                            result.Modification = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("Note Parser: Unknown field: " + field.Attribute("name").Value);
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
                            Console.WriteLine("Note Parser:Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    
                    case "Attachments":
                        result.Attachments = Attachment.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("Note Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }

        public static List<Note> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Note> result = new List<Note>();

            IEnumerable<XElement> noteElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Note");

            foreach (var noteElement in noteElements)
            {
                try
                {
                    result.Add(ParseModel(noteElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing Note: " + ex.Message);
                }
            }

            return result;
        }
        #endregion
    }
}
