
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    public class OrganizationsParser
    {
        public static List<Organization> ParseOrganizations(XElement oraganizationElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Organization> result = new List<Organization>();

            IEnumerable<XElement> organizations = oraganizationElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Organization");

            foreach (XElement organization in organizations)
            {
                Organization o = Parse(organization, debugAttributes);
                result.Add(o);
            }

            return result;
        }

        public static Organization Parse(XElement organizationNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Organization result = new Organization();

            var fieldElements = organizationNode.Elements(xNamespace + "field");
            var multiFieldElements = organizationNode.Elements(xNamespace + "multiField");
            var multiModelFieldElements = organizationNode.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "Position":
                        result.Position = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("OrganizationsParser.Parse: Unhandled field: " + field.Attribute("name").Value);
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
                            Console.WriteLine("OrganizationsParser.Parse: Unhandled field: " + multiField.Attribute("name").Value);
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
                            Console.WriteLine("OrganizationsParser.Parse: Unhandled field: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
    }
}
