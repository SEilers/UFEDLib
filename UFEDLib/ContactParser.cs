using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics.Contracts;
using System.Xml.XPath;
using UFEDLib.Parsers;

namespace UFEDLib
{
    public class ContactParser
    {
        public static List<Contact> ParseContacts(XElement contactsElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Contact> result = new List<Contact>();

            IEnumerable<XElement> contacts = contactsElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Contact");

            foreach (XElement contact in contacts)
            {
                Contact c = Parse(contact, debugAttributes);
                result.Add(c);
            }

            return result;
        }


        public static Contact Parse(XElement contactNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Contact result = new Contact();
            result.ParseAttributes(contactNode);

            var fieldElements = contactNode.Elements(xNamespace + "field");
            var multiFieldElements = contactNode.Elements(xNamespace + "multiField");
            var multiModelFieldElements = contactNode.Elements(xNamespace + "multiModelField");

            List<string> debugAttributesList = new List<string>();

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "AdditionalInfo":
                        result.AdditionalInfo = field.Value.Trim();
                        break;

                    case "Id":
                        result.Id = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Group":
                        result.Group = field.Value.Trim();
                        break;

                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "TimeContacted":
                        if (field.Value.Trim() != "")
                            result.TimeContacted = DateTime.Parse(field.Value.Trim());
                        break;

                    case "TimeCreated":
                        if (field.Value.Trim() != "")
                            result.TimeCreated = DateTime.Parse(field.Value.Trim());
                        break;

                    case "TimeModified":
                        if (field.Value.Trim() != "")
                            result.TimeModified = DateTime.Parse(field.Value.Trim());
                        break;

                    case "TimesContacted":
                        result.TimesContacted = int.Parse(field.Value.Trim());
                        break;

                    case "Type":
                        result.Type = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            string debugAttrubuteText = "ContactParser: Unknown field: " + field.Attribute("name").Value;
                            if (!debugAttributesList.Contains(debugAttrubuteText))
                            {
                                debugAttributesList.Add(debugAttrubuteText);
                            }
                        }
                        break;
                }
            }

            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "Notes":
                        result.Notes = multiField.Elements(xNamespace + "field").Select(x => x.Value.Trim()).ToList();
                        break;

                    case "InteractionStatuses":
                        result.InteractionStatuses = multiField.Elements(xNamespace + "field").Select(x => x.Value.Trim()).ToList();
                        break;

                    case "UserTags":
                        result.UserTags = multiField.Elements(xNamespace + "field").Select(x => x.Value.Trim()).ToList();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            string debugAttrubuteText = "ContactParser: Unknown multiField: " + multiField.Attribute("name").Value;
                            if (!debugAttributesList.Contains(debugAttrubuteText))
                            {
                                debugAttributesList.Add(debugAttrubuteText);
                            }
                        }
                        break;
                }
            }

            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "AdditionalInfo":
                        break;

                    case "Photos":
                        result.Photos = ContactPhotoParser.ParseContactPhotos(multiModelField, debugAttributes);
                        break;

                    case "Entries":
                        result.Entries = ContactEntryParser.ParseContactEntries(multiModelField, debugAttributes);
                        break;

                    case "Addresses":
                        result.Addresses = StreetAddressParser.ParseStreetAddresses(multiModelField, debugAttributes);
                        break;

                    case "Organizations":
                        result.Organizations = OrganizationsParser.ParseOrganizations(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            string debugAttrubuteText = "ContactParser: Unknown multiModelField: " + multiModelField.Attribute("name").Value;
                            if (!debugAttributesList.Contains(debugAttrubuteText))
                            {
                                debugAttributesList.Add(debugAttrubuteText);
                            }
                        }
                        break;
                }
            }

            if (debugAttributes)
            {
                foreach (string debugAttributeText in debugAttributesList)
                {
                    Console.WriteLine(debugAttributeText);
                }
            }

            return result;
        }
    }
}
