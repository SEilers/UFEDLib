using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class ContactEntryParser
    {
        public static List<ContactEntry> ParseContactEntries(XElement ContactEntriesElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<ContactEntry> result = new List<ContactEntry>();


            // ContactEntry is UserID, PhoneNumber, EMailAddress, WebAddress
            //IEnumerable<XElement> contactEntries = ContactEntriesElement.Descendants(xNamespace + "model").Where(x => x.Attribute("type").Value == "ContactEntry");

            IEnumerable<XElement> contactEntries = ContactEntriesElement.Elements(xNamespace + "model")
                .Where(x => x.Attribute("type").Value == "UserID" || 
                    x.Attribute("type").Value == "PhoneNumber" || 
                    x.Attribute("type").Value == "EMailAddress" || 
                    x.Attribute("type").Value == "WebAddress");

            foreach (XElement contactEntry in contactEntries)
            {
                ContactEntry c = ContactEntryParser.Parse(contactEntry);
                result.Add(c);
            }

            return result;
        }

        public static ContactEntry Parse(XElement contactNode)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            ContactEntry result = new ContactEntry();

            var fieldElements = contactNode.Elements(xNamespace + "field");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Value":
                        result.Value = field.Value.Trim();
                        break;

                    case "Category":
                        result.Category = field.Value.Trim();
                        break;

                    case "Domain":
                        result.Domain = field.Value.Trim();
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
    }
}
