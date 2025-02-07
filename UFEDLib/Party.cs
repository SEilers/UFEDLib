using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Party : ModelBase, IUfedModelParser<Party>
    {
        public static string GetXmlModelType()
        {
            return "Party";
        }

        #region fields

        public DateTime DateDellivered { get; set; }
        public DateTime DatePlayed { get; set; }
        public DateTime DateRead { get; set; }
        public string Id { get; set; }
        public string Identifier { get; set; }
        public string IPAddress { get; set; }
        public string IsGroupAdmin { get; set; }
        public bool IsPhoneOwner { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string Distance { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion

        public override string ToString()
        {
            return Name + " " + Identifier + " " + Role;
        }

        #region Parsers
        public static List<Party> ParseMultiModel(XElement partiesElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Party> result = new List<Party>();

            IEnumerable<XElement> parties = partiesElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Party");

            foreach (XElement party in parties)
            {
                Party p = ParseModel(party, debugAttributes);
                result.Add(p);
            }

            return result;
        }

        public static Party ParseModel(XElement xElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Party result = new Party();
            result.ParseAttributes(xElement);

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

                    case "IsGroupAdmin":
                        result.IsGroupAdmin = field.Value.Trim();
                        break;

                    case "DatePlayed":
                        if (field.Value.Trim() != "")
                            result.DatePlayed = DateTime.Parse(field.Value.Trim());
                        break;

                    case "IPAddress":
                        result.IPAddress = field.Value.Trim();
                        break;

                    case "Status":
                        result.Status = field.Value.Trim();
                        break;

                    case "Distance":
                        result.Distance = field.Value.Trim();
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
                            Logger.LogAttribute("Party Parser: Unknown field: " + field.Attribute("name").Value);
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
                            Logger.LogAttribute("Party Parser: Unknown multiField: " + multiField.Attribute("name").Value);
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
                            Logger.LogAttribute("Party Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }


            return result;
        }
        #endregion
    }
}

