using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    public class ContactPhotoParser
    {
        public static List<ContactPhoto> ParseContactPhotos(XElement contactPhotosElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<ContactPhoto> result = new List<ContactPhoto>();

            IEnumerable<XElement> contactPhotos = contactPhotosElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "ContactPhoto");

            foreach (XElement contactPhoto in contactPhotos)
            {
                ContactPhoto c = ContactPhotoParser.Parse(contactPhoto, debugAttributes);
                result.Add(c);
            }

            return result;
        }

        public static ContactPhoto Parse(XElement contactNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            ContactPhoto result = new ContactPhoto();

            var fieldElements = contactNode.Elements(xNamespace + "field");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Name":
                        result.Name = field.Value.Trim();
                        break;
                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("ContactPhotoParser: Unhandled field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiField in contactNode.Elements(xNamespace + "multiField"))
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
                            Console.WriteLine("ContactPhotoParser: Unhandled multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiModelField in contactNode.Elements(xNamespace + "multiModelField"))
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("ContactPhotoParser: Unhandled multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
    }
}
