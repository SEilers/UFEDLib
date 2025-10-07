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
        public string Account { get; set; }
        public string Origin { get; set; }
        public string OSUserId { get; set; }
        public string PositionAddress { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserMapping { get; set; }
        public string Value { get; set; }
        #endregion

        #region models
        public Coordinate Position { get; set; }
        #endregion

        #region multiModels
        #endregion

        #region multiFields
        public List<string> SearchResults { get; set; }
        #endregion

        #region Parsers

        public static SearchedItem ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<SearchedItem>(element, debugAttributes);
        }

        public static List<SearchedItem> ParseMultiModel(XElement searchedItemsElement, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<SearchedItem>(searchedItemsElement, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, SearchedItem result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Origin":
                        result.Origin = field.Value.Trim();
                        break;

                    case "OSUserId":
                        result.OSUserId = field.Value.Trim();
                        break;

                    case "PositionAddress":
                        result.PositionAddress = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Value":
                        result.Value = field.Value.Trim();
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
                XNamespace ns = modelField.Name.Namespace;
                XElement modelElement = modelField.Element(ns + "model");

                switch (modelField.Attribute("name").Value)
                {
                    case "Position":
                        result.Position = Coordinate.ParseModel(modelElement);
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
            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "SearchResults":
                        result.SearchResults = multiField.Elements().Select(x => x.Value.Trim()).ToList();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("SearchedItem Parser: Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, SearchedItem result, bool debugAttributes = false)
        {
            IUfedModelParser<SearchedItem>.CheckMultiModelFields<SearchedItem>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
