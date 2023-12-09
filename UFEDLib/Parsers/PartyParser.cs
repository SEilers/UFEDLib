using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib.Parsers
{
    internal class PartyParser
    {
        public static List<Party> ParseParties(XElement partiesElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Party> result = new List<Party>();

            IEnumerable<XElement> parties = partiesElement.Descendants(xNamespace + "model").Where(x => x.Attribute("type").Value == "Party");

            foreach (XElement party in parties)
            {
                Party p = PartyParser.Parse(party);
                result.Add(p);
            }

            return result;
        }

        public static Party Parse(XElement xElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Party result = new Party();

            var fieldElements = xElement.Elements(xNamespace + "field");

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
                        result.DateDellivered = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DateRead":
                        result.DateRead = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DatePlayed":
                        result.DatePlayed = DateTime.Parse(field.Value.Trim());
                        break;

                    case "IPAddress":
                        result.IPAddress = field.Value.Trim();
                        break;

                    case "IsPhoneOwner":
                        result.IsPhoneOwner = bool.Parse(field.Value.Trim());
                        break;

                    default:
                        break;
                }
            }

            return result;
        }
    }
}
