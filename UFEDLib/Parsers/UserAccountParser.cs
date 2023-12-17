using System;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using UFEDLib.Models;


namespace UFEDLib.Parsers
{
    public class UserAccountParser
    {
        public static UserAccount Parse(XElement userAccountNode)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            UserAccount result = new UserAccount();

            var fieldElements = userAccountNode.Elements(xNamespace + "field");
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
                }
            }

            foreach (var multiField in multiModelFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "Photos":
                        result.Photos = ContactPhotoParser.ParseContactPhotos(multiField);
                        break;

                    case "Entries":
                        result.Entries = ContactEntryParser.ParseContactEntries(multiField);
                        break;

                    case "Addresses":
                        result.Addresses = StreetAddressParser.ParseStreetAddresses(multiField);
                        break;

                    case "Organizations":
                        result.Organizations = OrganizationsParser.ParseOrganizations(multiField);
                        break;
                }
            }


            return result;

        }
    }
}
