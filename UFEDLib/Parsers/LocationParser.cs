using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class LocationParser
    {
        public static List<Location> ParseLocations(XElement locationsElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Location> result = new List<Location>();

            IEnumerable<XElement> locationElements = locationsElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Location");

            foreach (var locationElement in locationElements)
            {
                result.Add(Parse(locationElement));
            }

            return result;
        }

        public static Location Parse(XElement locationElelment)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Location result = new Location();

            var fieldElements = locationElelment.Elements(xNamespace + "field");
            var modelFieldElements = locationElelment.Elements(xNamespace + "modelField");
         

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "Description":
                        result.Description = field.Value.Trim();
                        break;

                    case "Type":
                        result.Type = field.Value.Trim();
                        break;

                    case "Precision":
                        result.Precision = field.Value.Trim();
                        break;

                    case "Map":
                        result.Map = field.Value.Trim();
                        break;

                    case "Category":
                        result.Category = field.Value.Trim();
                        break;

                    case "Confidence":
                        result.Confidence = field.Value.Trim();
                        break;

                    case "Origin":
                        result.Origin = field.Value.Trim();
                        break;

                    case "PositionAddress":
                        result.PositionAddress = field.Value.Trim();
                        break;
                }
            }

            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    case "Position":
                        result.Position = CoordinateParser.Parse(modelField);
                        break;

                    case "Address":
                        result.Address = StreetAddressParser.Parse(modelField);
                        break;
                }
            }

            return result;
        }
    }
}
