using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class ContactPhoto : ModelBase, IUfedModelParser<ContactPhoto>
    {
        public static string GetXmlModelType()
        {
            return "ContactPhoto";
        }

        #region fields
        /// <summary>
        /// Filename (if exists).
        /// </summary>
        public string Name { get; set; }
        public string Url { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region Parsers
        public static List<ContactPhoto> ParseMultiModel(XElement contactPhotosElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<ContactPhoto> result = new List<ContactPhoto>();

            IEnumerable<XElement> contactPhotos = contactPhotosElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "ContactPhoto");

            foreach (XElement contactPhoto in contactPhotos)
            {
                ContactPhoto c = ParseModel(contactPhoto, debugAttributes);
                result.Add(c);
            }

            return result;
        }

        public static ContactPhoto ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            ContactPhoto result = new ContactPhoto();
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
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ContactPhoto Parser: Unknown field: " + field.Attribute("name").Value);
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
                            Logger.LogAttribute("Contact Photo Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "Url":
                        // TODO: Check for an example
                        result.Url = multiField.Value.Trim();
                        break;
                    case "Data":
                        break;
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Contact Photo Parser: Unknown multiField: " + multiField.Attribute("name").Value);
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
                            Logger.LogAttribute("Contact Photo Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
        #endregion

    }
}
