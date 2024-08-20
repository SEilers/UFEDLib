using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    public class ContactEntryParser
    {
        public static List<ContactEntry> ParseContactEntries(XElement ContactEntriesElement, bool debugAttributes = false)
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
                ContactEntry c = ContactEntryParser.Parse(contactEntry, debugAttributes);
                result.Add(c);
            }

            return result;
        }

        public static ContactEntry Parse(XElement contactNode, bool debugAttributes = false)
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
                        if (debugAttributes)
                        {
                            Console.WriteLine("ContactEntryParser: Unknown attribute: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiField in contactNode.Elements(xNamespace + "multiField"))
            {
                switch (multiField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("ContactEntryParser: Unknown multiField: " + multiField.Attribute("name").Value);
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
                            Console.WriteLine("ContactEntryParser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
    }
}
