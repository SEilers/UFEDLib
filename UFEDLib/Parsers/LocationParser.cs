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
        public static List<Location> ParseLocations(XElement locationsElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Location> result = new List<Location>();

            IEnumerable<XElement> locationElements = locationsElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Location");

            foreach (var locationElement in locationElements)
            {
                result.Add(Parse(locationElement, debugAttributes));
            }

            return result;
        }

        public static Location Parse(XElement locationElelment, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Location result = new Location();

            var fieldElements = locationElelment.Elements(xNamespace + "field");
            var modelFieldElements = locationElelment.Elements(xNamespace + "modelField");
         

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

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

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("LocationParser.Parse: Unhandled field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    case "Position":
                        var coordinateModel = modelField.Element(xNamespace + "model");
                        result.Position = CoordinateParser.Parse(coordinateModel);
                        break;

                    case "Address":
                        var addressModel = modelField.Element(xNamespace + "model");
                        result.Address = StreetAddressParser.Parse(addressModel);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("LocationParser.Parse: Unhandled modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiModelField in locationElelment.Elements(xNamespace + "multiModelField"))
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("LocationParser.Parse: Unhandled multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
    }
}
