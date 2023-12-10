using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class CoordinateParser
    {
        public static Coordinate Parse(XElement corrdinateElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Coordinate result = new Coordinate();

            var fieldElements = corrdinateElement.Elements(xNamespace + "field");

            foreach (var fieldElement in fieldElements)
            {
                switch (fieldElement.Attribute("name").Value)
                {
                    case "Longitude":
                        result.Longitude = double.Parse(fieldElement.Value.Trim());
                        break;

                    case "Latitude":
                        result.Latitude = double.Parse(fieldElement.Value.Trim());
                        break;

                    case "Elevation":
                        result.Elevation = double.Parse(fieldElement.Value.Trim());
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
                }
            }

            return result;
        }
    }
}
