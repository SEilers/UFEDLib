using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class VisitedPage : ModelBase, IUfedModelParser<VisitedPage>
    {
        public static string GetXmlModelType()
        {
            return "VisitedPage";
        }

        #region fields
        public DateTime LastVisited { get; set; }

        public string Source { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string UserMapping { get; set; }

        public int VisitCount { get; set; }
        #endregion

        #region Parsers
        public static VisitedPage ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            VisitedPage result = new VisitedPage();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var modelFieldElements = element.Elements(xNamespace + "modelField");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            try
            {
                foreach (XElement field in fieldElements)
                {
                    switch (field.Attribute("name").Value)
                    {
                        case "Source":
                            result.Source = field.Value.Trim();
                            break;

                        case "UserMapping":
                            result.UserMapping = field.Value.Trim();
                            break;

                        case "Url":
                            result.Url = field.Value.Trim();
                            break;

                        case "Title":
                            result.Title = field.Value.Trim();
                            break;

                        case "LastVisited":
                            if (field.Value.Trim() != "")
                                result.LastVisited = DateTime.Parse(field.Value.Trim());
                            break;

                        case "VisitCount":
                            if(field.Value.Trim() != "")
                                result.VisitCount = int.Parse(field.Value.Trim());
                            break;

                        default:
                            if (debugAttributes)
                            {
                                Logger.LogAttribute("VisitedPage Parser: Unknown field: " + field.Attribute("name").Value);
                            }
                            break;
                    }
                }

                foreach (var modelField in modelFieldElements)
                {
                    switch (modelField.Attribute("name").Value)
                    {
                        default:
                            if (debugAttributes)
                            {
                                Logger.LogAttribute("VisitedPage Parser: Unknown modelField: " + modelField.Attribute("name").Value);
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
                                Logger.LogAttribute("VisitedPage Parser: Unknown multiField: " + multiField.Attribute("name").Value);
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
                                Logger.LogAttribute("VisitedPage Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }

            return result;
        }

        public static List<VisitedPage> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<VisitedPage> result = new List<VisitedPage>();

            IEnumerable<XElement> vPElements = element.Elements(xNamespace + "model").Where(element => element.Attribute("type").Value == "VisitedPage");

            foreach (XElement vPElement in vPElements)
            {
                VisitedPage im = ParseModel(vPElement, debugAttributes);
                result.Add(im);
            }

            return result;
        }
        #endregion
    }
}
