
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class OrganizationsParser
    {
        public static List<Organization> ParseOrganizations(XElement oraganizationElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Organization> result = new List<Organization>();

            IEnumerable<XElement> organizations = oraganizationElement.Descendants(xNamespace + "model").Where(x => x.Attribute("type").Value == "Organization");

            foreach (XElement organization in organizations)
            {
                Organization o = Parse(organization);
                result.Add(o);
            }

            return result;
        }

        public static Organization Parse(XElement organizationNode)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Organization result = new Organization();

            var fieldElements = organizationNode.Elements(xNamespace + "field");

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
                        break;
                }
            }

            return result;
        }
    }
}
