using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class PartyParser
    {
        public static List<Party> ParseParties(XElement partiesElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Party> result = new List<Party>();

            IEnumerable<XElement> parties = partiesElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Party");

            foreach (XElement party in parties)
            {
                Party p = PartyParser.Parse(party, debugAttributes);
                result.Add(p);
            }

            return result;
        }

        public static Party Parse(XElement xElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Party result = new Party();

            var fieldElements = xElement.Elements(xNamespace + "field");
            var multiFieldElements = xElement.Elements(xNamespace + "multiField");
            var multiModelFieldElements = xElement.Elements(xNamespace + "multiModelField");
            
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

                    case "Identifier":
                        result.Identifier = field.Value.Trim();
                        break;

                    case "Role":
                        result.Role = field.Value.Trim();
                        break;

                    case "DateDellivered":
                        if (field.Value.Trim() != "")
                            result.DateDellivered = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DateRead":
                        if (field.Value.Trim() != "")
                            result.DateRead = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DatePlayed":
                        if (field.Value.Trim() != "")
                            result.DatePlayed = DateTime.Parse(field.Value.Trim());
                        break;

                    case "IPAddress":
                        result.IPAddress = field.Value.Trim();
                        break;

                    case "IsPhoneOwner":
                        if (!string.IsNullOrEmpty(field.Value.Trim()))
                        {
                            result.IsPhoneOwner = bool.Parse(field.Value.Trim());
                        }
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("PartyParser: Unknown attribute: " + field.Attribute("name").Value);
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
                            Console.WriteLine("PartyParser: Unknown multiAttribute: " + multiField.Attribute("name").Value);
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
                            Console.WriteLine("PartyParser: Unknown multiModelAttribute: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }


            return result;
        }
    }
}
