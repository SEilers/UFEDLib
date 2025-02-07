using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class WebBookmark : ModelBase, IUfedModelParser<WebBookmark>
    {
        public static string GetXmlModelType()
        {
            return "WebBookmark";
        }

        #region fields
        public string ServiceIdentifier { get; set; }
        public DateTime LastVisited { get; set; }
        public string Path { get; set; }
        public string PositionAddress { get; set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string UserMapping { get; set; }
        public int VisitCount { get; set; }
        #endregion

        #region models
        public Coordinate Position { get; set; }

        #endregion

        #region Parsers
        public static WebBookmark ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            WebBookmark result = new WebBookmark();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Path":
                        result.Path = field.Value.Trim();
                        break;

                    case "PositionAddress":
                        result.PositionAddress = field.Value.Trim();
                        break;

                    case "Title":
                        result.Title = field.Value.Trim();
                        break;

                    case "Url":
                        result.Url = field.Value.Trim();
                        break;

                    case "VisitCount":
                        if (field.Value.Trim() != "")
                        {
                            result.VisitCount = int.Parse(field.Value.Trim());
                        }
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "LastVisited":
                        if (field.Value.Trim() != "")
                            result.LastVisited = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("WebBookmark Parser: Unknown field: " + field.Attribute("name").Value);
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
                            Logger.LogAttribute("WebBookmark Parser: Unknown multiField: " + multiField.Attribute("name").Value);
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
                            Logger.LogAttribute("WebBookmark Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }

        public static List<WebBookmark> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<WebBookmark> result = new List<WebBookmark>();

            IEnumerable<XElement> webBookmarks = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "WebBookmark");

            foreach (XElement webBookmark in webBookmarks)
            {
                WebBookmark wb = ParseModel(webBookmark, debugAttributes);
                result.Add(wb);
            }

            return result;
        }

        #endregion
    }
}
