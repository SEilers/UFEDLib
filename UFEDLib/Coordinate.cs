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

        public static Coordinate ParseModel(XElement element, bool debugAttributes = false)
        {
           return DefaultModelParser<Coordinate>(element, debugAttributes);
        }
        public static List<Coordinate> ParseMultiModel(XElement corrdinatesElement, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Coordinate>(corrdinatesElement, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Coordinate result, bool debugAttributes = false)
        {
            foreach (var fieldElement in fieldElements)
            {
                try
                {

                    switch (fieldElement.Attribute("name").Value)
                    {
                        case "Comment":
                            result.Comment = fieldElement.Value.Trim();
                            break;

                        case "Elevation":
                            if (fieldElement.Value.Trim() != "")
                            {
                                string elevation = fieldElement.Value.Trim().Replace(",", ".");
                                result.Elevation = double.Parse(elevation, CultureInfo.InvariantCulture);
                            }
                            break;

                        case "Longitude":
                            if (fieldElement.Value.Trim() != "")
                            {
                                string longitude = fieldElement.Value.Trim().Replace(",", ".");
                                result.Longitude = double.Parse(longitude, CultureInfo.InvariantCulture);
                            }
                            break;

                        case "Latitude":
                            if (fieldElement.Value.Trim() != "")
                            {
                                string latitude = fieldElement.Value.Trim().Replace(",", ".");
                                result.Latitude = double.Parse(latitude, CultureInfo.InvariantCulture);
                            }
                            break;

                        case "Map":
                            result.Map = fieldElement.Value.Trim();
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
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Coordinate result, bool debugAttributes = false)
        {
            IUfedModelParser<Coordinate>.CheckModelFields<Coordinate>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Coordinate result, bool debugAttributes = false)
        {
            IUfedModelParser<Coordinate>.CheckMultiFields<Coordinate>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Coordinate result, bool debugAttributes = false)
        {
            IUfedModelParser<Coordinate>.CheckMultiModelFields<Coordinate>(multiModelFieldElements, debugAttributes);
        }
        #endregion

    }
}
