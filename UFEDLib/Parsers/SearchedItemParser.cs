using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class SearchedItemParser
    {
        public static SearchedItem Parse(XElement searchedItemElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            SearchedItem result = new SearchedItem();

            var fieldElements = searchedItemElement.Elements(xNamespace + "field");

            foreach (var fieldElement in fieldElements)
            {
                switch (fieldElement.Attribute("name").Value)
                {
                    case "TimeStamp":
                        result.TimeStamp = DateTime.Parse(fieldElement.Value.Trim());
                        break;

                    case "Value":
                        result.Value = fieldElement.Value.Trim();
                        break;

                    case "Source":
                        result.Source = fieldElement.Value.Trim();
                        break;

                    case "SearchResult":
                        result.SearchResults = fieldElement.Value.Trim();
                        break;

                    case "Position":
                        result.Position = CoordinateParser.Parse(fieldElement);
                        break;

                    case "PositionAddress":
                        result.PositionAddress = fieldElement.Value.Trim();
                        break;
                }
            }
            return result;
        }
    }
}
