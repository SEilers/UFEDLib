using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class ContactPhotoParser
    {
        public static List<ContactPhoto> ParseContactPhotos(XElement contactPhotosElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<ContactPhoto> result = new List<ContactPhoto>();

            IEnumerable<XElement> contactPhotos = contactPhotosElement.Descendants(xNamespace + "model").Where(x => x.Attribute("type").Value == "ContactPhoto");

            foreach (XElement contactPhoto in contactPhotos)
            {
                ContactPhoto c = ContactPhotoParser.Parse(contactPhoto);
                result.Add(c);
            }

            return result;
        }

        public static ContactPhoto Parse(XElement contactNode)
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
                        break;
                }
            }

            return result;
        }
    }
}
