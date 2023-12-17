using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class JourneyParser
    {
        public static Journey Parse(XElement journeyElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Journey result = new Journey();

            var fieldElements = journeyElement.Elements(xNamespace + "field");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
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


                }
            }

            var modelFieldElements = journeyElement.Elements(xNamespace + "modelField");

            foreach (var modelFieldElement in modelFieldElements)
            {
                switch (modelFieldElement.Attribute("name").Value)
                {
                    case "FromPoint":
                        result.FromPoint = LocationParser.Parse(modelFieldElement);
                        break;

                    case "ToPoint":
                        result.ToPoint = LocationParser.Parse(modelFieldElement);
                        break;
                }
            }

            var multiModelFieldElements = journeyElement.Elements(xNamespace + "multiModelField");

            foreach (var multiModelFieldElement in multiModelFieldElements)
            {
                switch (multiModelFieldElement.Attribute("name").Value)
                {
                    case "WayPoints":
                        result.WayPoints = LocationParser.ParseLocations(multiModelFieldElement);
                        break;
                }
            }

            return result;
        }
    }
}
