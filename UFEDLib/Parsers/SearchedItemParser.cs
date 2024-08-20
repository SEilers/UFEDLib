using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    public class SearchedItemParser
    {
        public static SearchedItem Parse(XElement searchedItemElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            SearchedItem result = new SearchedItem();

            var fieldElements = searchedItemElement.Elements(xNamespace + "field");
            var multiFieldElements = searchedItemElement.Elements(xNamespace + "multiField");
            var multiModelFieldElements = searchedItemElement.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Value":
                        result.Value = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "SearchResult":
                        result.SearchResults = field.Value.Trim();
                        break;

                    case "Position":
                        result.Position = CoordinateParser.Parse(field);
                        break;

                    case "PositionAddress":
                        result.PositionAddress = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("SearchedItemParser.Parse: Unhandled field: " + field.Attribute("name").Value);
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
                            Console.WriteLine("SearchedItemParser.Parse: Unhandled multiField: " + multiField.Attribute("name").Value);
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
                            Console.WriteLine("SearchedItemParser.Parse: Unhandled multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
    }
}
