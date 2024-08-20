using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    public class StreetAddressParser
    {
        public static List<StreetAddress> ParseStreetAddresses(XElement streetAddresssElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<StreetAddress> result = new List<StreetAddress>();

            IEnumerable<XElement> streetAddresses = streetAddresssElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "StreetAddress");

            foreach (XElement streetAddress in streetAddresses)
            {
                StreetAddress s = Parse(streetAddress, debugAttributes);
                result.Add(s);
            }

            return result;
        }

        public static StreetAddress Parse(XElement streetAddressNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            StreetAddress result = new StreetAddress();

            var fieldElements = streetAddressNode.Elements(xNamespace + "field");
            var multiFieldElements = streetAddressNode.Elements(xNamespace + "multiField");
            var multiModelFieldElements = streetAddressNode.Elements(xNamespace + "multiModelField");


            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Street1":
                        result.Street1 = field.Value.Trim();
                        break;
                    case "Street2":
                        result.Street2 = field.Value.Trim();
                        break;
                    case "HouseNumber":
                        result.HouseNumber = field.Value.Trim();
                        break;

                    case "City":
                        result.City = field.Value.Trim();
                        break;

                    case "State":
                        result.State = field.Value.Trim();
                        break;

                    case "Country":
                        result.Country = field.Value.Trim();
                        break;

                    case "PostalCode":
                        result.PostalCode = field.Value.Trim();
                        break;

                    case "POBox":
                        result.POBox = field.Value.Trim();
                        break;

                    case "Neighborhood":
                        result.Neighborhood = field.Value.Trim();
                        break;

                    case "Category":
                        result.Category = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("StreetAddressParser.Parse: Unhandled field: " + field.Attribute("name").Value);
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
                            Console.WriteLine("StreetAddressParser.Parse: Unhandled multiField: " + multiField.Attribute("name").Value);
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
                            Console.WriteLine("StreetAddressParser.Parse: Unhandled multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
    }
}
