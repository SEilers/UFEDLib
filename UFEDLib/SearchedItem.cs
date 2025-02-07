using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class SearchedItem : ModelBase, IUfedModelParser<SearchedItem>
    {
        public static string GetXmlModelType()
        {
            return "SearchedItem";
        }

        #region fields
        public string UserMapping { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Account { get; set; }
        public string Origin { get; set; }
        public string PositionAddress { get; set; }
        public string SearchResults { get; set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Value { get; set; }
        #endregion

        #region models
        public Coordinate Position { get; set; }
        #endregion

        #region multiModels
        #endregion

        #region Parsers
        public static List<SearchedItem> ParseMultiModel(XElement searchedItemsElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            List<SearchedItem> result = new List<SearchedItem>();
           
            IEnumerable<XElement> searchedItemElements = searchedItemsElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "SearchedItem");

            foreach (var searchedItemElement in searchedItemElements)
            {
                try
                {
                    result.Add(ParseModel(searchedItemElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing searched item: " + ex.Message);
                }
            }

            return result;
        }
        public static SearchedItem ParseModel(XElement searchedItemElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            SearchedItem result = new SearchedItem();
            result.ParseAttributes(searchedItemElement);

            var fieldElements = searchedItemElement.Elements(xNamespace + "field");
            var multiFieldElements = searchedItemElement.Elements(xNamespace + "multiField");
            var multiModelFieldElements = searchedItemElement.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Origin":
                        result.Origin = field.Value.Trim();
                        break;

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
                        result.Position = Coordinate.ParseModel(field);
                        break;

                    case "PositionAddress":
                        result.PositionAddress = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("SearchedItem Parser: Unhandled field: " + field.Attribute("name").Value);
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
                            Logger.LogAttribute("SearchedItem Parser: Unhandled multiField: " + multiField.Attribute("name").Value);
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
                            Logger.LogAttribute("SearchedItem Parser: Unhandled multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
        #endregion
    }
}
