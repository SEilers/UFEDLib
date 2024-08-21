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
                try 
                { 

                    switch (fieldElement.Attribute("name").Value)
                    {
                        case "Longitude":
                            if (fieldElement.Value.Trim() != "")
                            {
                                result.Longitude = double.Parse(fieldElement.Value.Trim(), CultureInfo.InvariantCulture);
                            }
                            break;

                        case "Latitude":
                            if (fieldElement.Value.Trim() != "")
                            {
                                result.Latitude = double.Parse(fieldElement.Value.Trim(), CultureInfo.InvariantCulture);
                            }
                            break;

                        case "Elevation":
                            if(fieldElement.Value.Trim() != "")
                            { 
                                result.Elevation = double.Parse(fieldElement.Value.Trim(), CultureInfo.InvariantCulture);
                            }
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
                catch( Exception ex) 
                {
                    Console.WriteLine("Error parsing field elements in CoordinateParser" + ex.ToString());
                }

            }
               

            foreach (var multiField in corrdinateElement.Elements(xNamespace + "multiField"))
            {
                try
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
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing multiField elements in CoordinateParser" + ex.ToString());
                }
            }

            foreach (var multiModelField in corrdinateElement.Elements(xNamespace + "multiModelField"))
            {
                try
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
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing multiModelField elements in CoordinateParser" + ex.ToString());
                }
            }

            return result;
        }
    }
}
