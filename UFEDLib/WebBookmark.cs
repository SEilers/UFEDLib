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
        public DateTime LastVisited { get; set; }
        public string Path { get; set; }
        public string PositionAddress { get; set; }
        public string ServiceIdentifier { get; set; }
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
            return DefaultModelParser<WebBookmark>(element, debugAttributes);
        }

        public static List<WebBookmark> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<WebBookmark>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, WebBookmark result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {

                    case "LastVisited":
                        if (field.Value.Trim() != "")
                            result.LastVisited = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Path":
                        result.Path = field.Value.Trim();
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

                    case "Title":
                        result.Title = field.Value.Trim();
                        break;

                    case "Url":
                        result.Url = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "VisitCount":
                        if (field.Value.Trim() != "")
                        {
                            if (int.TryParse(field.Value.Trim(), out int visitCount))
                                result.VisitCount = visitCount;
                        }
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("WebBookmark Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, WebBookmark result, bool debugAttributes = false)
        {
            IUfedModelParser<WebBookmark>.CheckModelFields<WebBookmark>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, WebBookmark result, bool debugAttributes = false)
        {
            IUfedModelParser<WebBookmark>.CheckMultiFields<WebBookmark>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, WebBookmark result, bool debugAttributes = false)
        {
            IUfedModelParser<WebBookmark>.CheckMultiModelFields<WebBookmark>(multiModelFieldElements, debugAttributes);
        }

        #endregion
    }
}
