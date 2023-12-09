using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics.Contracts;
using System.Xml.XPath;
using UFEDLib.Models;
using UFEDLib.Parsers;

namespace UFEDLib
{
    public class ContactParser
    {
        public static Contact Parse(XElement contactNode)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Contact result = new Contact();

            var fieldElements = contactNode.Elements(xNamespace + "field");
            var multiModelFieldElements = contactNode.Elements(xNamespace + "multiModelField");

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

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Group":
                        result.Group = field.Value.Trim();
                        break;

                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "TimeContacted":
                        result.TimeContacted = DateTime.Parse(field.Value.Trim());
                        break;

                    case "TimeCreated":
                        result.TimeCreated = DateTime.Parse(field.Value.Trim());
                        break;

                    case "TimeModified":
                        result.TimeModified = DateTime.Parse(field.Value.Trim());
                        break;
                }
            }

            foreach( var multiField in multiModelFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "Photos":
                        result.Photos =  ContactPhotoParser.ParseContactPhotos(multiField);
                        break;

                    case "Entries":
                        result.Entries =  ContactEntryParser.ParseContactEntries(multiField);
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
