using System;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using UFEDLib.Parsers;

namespace UFEDLib
{
    public class UserAccountParser
    {
        public static UserAccount Parse(XElement userAccountNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            UserAccount result = new UserAccount();

            var fieldElements = userAccountNode.Elements(xNamespace + "field");
            var multiFieldElements = userAccountNode.Elements(xNamespace + "multiField");
            var multiModelFieldElements = userAccountNode.Elements(xNamespace + "multiModelField");


            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Id":
                        result.Id = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "Username":
                        result.Username = field.Value.Trim();
                        break;

                    case "Password":
                        result.Password = field.Value.Trim();
                        break;

                    case "ServiceType":
                        result.ServiceType = field.Value.Trim();
                        break;

                    case "ServerAddress":
                        result.ServerAddress = field.Value.Trim();
                        break;

                    case "TimeCreated":
                        if (field.Value.Trim() != "")
                            result.TimeCreated = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("UserAccountParser.Parse: Unhandled field: " + field.Attribute("name").Value);
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
                            Console.WriteLine("UserAccountParser.Parse: Unhandled multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
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
                            Console.WriteLine("UserAccountParser.Parse: Unhandled multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }


            return result;

        }
    }
}
