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
        public static SearchedItem ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            SearchedItem result = new SearchedItem();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var modelFieldElements = element.Elements(xNamespace + "modelField");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            ParseFields(fieldElements, result, debugAttributes);
            ParseModelFields(modelFieldElements, result, debugAttributes);
            ParseMultiFields(multiFieldElements, result, debugAttributes);
            ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, SearchedItem result, bool debugAttributes = false)
        {
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

                    case "PositionAddress":
                        result.PositionAddress = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("SearchedItem Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, SearchedItem result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    case "Position":
                        result.Position = Coordinate.ParseModel(modelField);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("SearchedItem Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, SearchedItem result, bool debugAttributes = false)
        {
            IUfedModelParser<SearchedItem>.CheckMultiFields<SearchedItem>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, SearchedItem result, bool debugAttributes = false)
        {
            IUfedModelParser<SearchedItem>.CheckMultiModelFields<SearchedItem>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
