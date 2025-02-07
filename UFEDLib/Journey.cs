using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Journey : ModelBase, IUfedModelParser<Journey>
    {
        public static string GetXmlModelType()
        {
            return "Journey";
        }


        #region fields
        public string Account { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public DateTime StartTime { get; set; }
        public string UserMapping { get; set; }

        public string Type { get; set; }
        #endregion

        #region models
        public Location FromPoint { get; set; }

        public Location ToPoint { get; set; }
        #endregion

        #region multiModels
        /// <summary>
        /// Journey locations
        /// </summary>
        public List<Location> WayPoints { get; set; } = new List<Location>();
        #endregion

        #region Parsers
        public static List<Journey> ParseMultiModel(XElement journeysElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Journey> result = new List<Journey>();

            IEnumerable<XElement> journeyElements = journeysElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Journey");

            foreach (var journeyElement in journeyElements)
            {
                try
                {
                    result.Add(ParseModel(journeyElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing chat: " + ex.Message);
                }
            }

            return result;
        }


        public static Journey ParseModel(XElement journeyElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Journey result = new Journey();
            result.ParseAttributes(journeyElement);

            var fieldElements = journeyElement.Elements(xNamespace + "field");
            var modelFieldElements = journeyElement.Elements(xNamespace + "modelField");
            var multiModelFieldElements = journeyElement.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "StartTime":
                        if (field.Value.Trim() != "")
                            result.StartTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Type":
                        result.Type = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Journey Parser: Unhandled field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }



            foreach (var modelFieldElement in modelFieldElements)
            {
                switch (modelFieldElement.Attribute("name").Value)
                {
                    case "FromPoint":
                        result.FromPoint = Location.ParseModel(modelFieldElement, debugAttributes);
                        break;

                    case "ToPoint":
                        result.ToPoint = Location.ParseModel(modelFieldElement, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Journey Parser: Unhandled modelField: " + modelFieldElement.Attribute("name").Value);
                        }
                        break;
                }
            }


            foreach (var multiModelFieldElement in multiModelFieldElements)
            {
                switch (multiModelFieldElement.Attribute("name").Value)
                {
                    case "WayPoints":
                        result.WayPoints = Location.ParseMultiModel(multiModelFieldElement, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Journey Parser: Unhandled multiModelField: " + multiModelFieldElement.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
        #endregion
    }
}
