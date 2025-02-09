﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Coordinate : ModelBase, IUfedModelParser<Coordinate>
    {
        public static string GetXmlModelType()
        {
            return "Coordinate";
        }

        #region fields
        public string Comment { get; set; }
        public double Elevation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        /// <summary>
        /// Free text map to which the coordinate relates.
        /// </summary>
        public string Map { get; set; }
        public string PositionAddress { get; set; }
        #endregion

        #region Parsers
        public static List<Coordinate> ParseMultiModel(XElement corrdinatesElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Coordinate> result = new List<Coordinate>();

            IEnumerable<XElement> corrdinateElements = corrdinatesElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Coordinate");

            foreach (XElement coordinate in corrdinateElements)
            {
                Coordinate c = ParseModel(coordinate, debugAttributes);
                result.Add(c);
            }

            return result;
        }

        public static Coordinate ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Coordinate result = new Coordinate();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var modelFieldElements = element.Elements(xNamespace + "modelField");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");


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
                            if (fieldElement.Value.Trim() != "")
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
                                Logger.LogAttribute("Coordinate Parser: Unknown field: " + fieldElement.Attribute("name").Value);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing field elements in Coordinate Parser" + ex.ToString());
                }

            }

            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Coordinate Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }


            foreach (var multiField in multiFieldElements)
            {
                try
                {
                    switch (multiField.Attribute("name").Value)
                    {
                        default:
                            if (debugAttributes)
                            {
                                Logger.LogAttribute("Coordinate Parser: Unknown multiField: " + multiField.Attribute("name").Value);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing multiField elements in Coordinate Parser" + ex.ToString());
                }
            }

            foreach (var multiModelField in multiModelFieldElements)
            {
                try
                {
                    switch (multiModelField.Attribute("name").Value)
                    {
                        default:
                            if (debugAttributes)
                            {
                                Logger.LogAttribute("Coordinate Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing multiModelField elements in Coordinate Parser" + ex.ToString());
                }
            }

            return result;
        }
        #endregion

    }
}
