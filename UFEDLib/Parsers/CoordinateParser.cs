using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using UFEDLib.Models;
using System.Globalization;

namespace UFEDLib.Parsers
{
    public class CoordinateParser
    {
        public static Coordinate Parse(XElement corrdinateElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Coordinate result = new Coordinate();

            var fieldElements = corrdinateElement.Elements(xNamespace + "field");

            foreach (var fieldElement in fieldElements)
            {
                switch (fieldElement.Attribute("name").Value)
                {
                    case "Longitude":
                        result.Longitude = double.Parse(fieldElement.Value.Trim(), CultureInfo.InvariantCulture);
                        break;

                    case "Latitude":
                        result.Latitude = double.Parse(fieldElement.Value.Trim(), CultureInfo.InvariantCulture);
                        break;

                    case "Elevation":
                        result.Elevation = double.Parse(fieldElement.Value.Trim(), CultureInfo.InvariantCulture);
                        break;

                    case "Map":
                        result.Map = fieldElement.Value.Trim();
                        break;

                    case "Comment":
                        result.Comment = fieldElement.Value.Trim();
                        break;

                    case "PositionAddress":
                        result.PositionAddress = fieldElement.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("CoordinateParser: Unhandled field: " + fieldElement.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiField in corrdinateElement.Elements(xNamespace + "multiField"))
            {
                switch (multiField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("CoordinateParser: Unhandled multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiModelField in corrdinateElement.Elements(xNamespace + "multiModelField"))
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("CoordinateParser: Unhandled multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
    }
}
