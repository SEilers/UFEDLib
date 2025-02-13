using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Location : ModelBase, IUfedModelParser<Location>
    {
        public static string GetXmlModelType()
        {
            return "Location";
        }

        #region fields
        public string Category { get; set; }
        public string Confidence { get; set; }
        public string Description { get; set; }
        public string Map { get; set; }
        public string Name { get; set; }
        public string ServiceName { get; set; }
        public string LocationOrigin { get; set; }
        public string Origin { get; set; }
        public string PositionAddress { get; set; }
        public string Precision { get; set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime EndTime { get; set; }
        public string Account { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Type { get; set; }
        public string UserMapping { get; set; }
        public int AggregatedLocationsCount { get; set; }
        public string AccountLocationAffiliation { get; set; }
        public string DeviceLocationAffiliation { get; set; }
        public double GpsHorizontalAccuracy { get; set; }
        #endregion

        #region models
        public StreetAddress Address { get; set; }
        public Coordinate Position { get; set; }
        #endregion

        #region multiModels
        #endregion

        #region Parsers
        public static List<Location> ParseMultiModel(XElement locationsElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Location> result = new List<Location>();

            IEnumerable<XElement> locationElements = locationsElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Location");

            foreach (var locationElement in locationElements)
            {
                try
                {
                    result.Add(ParseModel(locationElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing location: " + ex.Message);
                }

            }

            return result;
        }

        public static Location ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Location result = new Location();

            try
            {
                result.ParseAttributes(element);

                var fieldElements = element.Elements(xNamespace + "field");
                var modelFieldElements = element.Elements(xNamespace + "modelField");
                var multiFieldElements = element.Elements(xNamespace + "multiField");
                var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

                ParseFields(fieldElements, result, debugAttributes);
                ParseModelFields(modelFieldElements, result, debugAttributes);
                ParseMultiFields(multiFieldElements, result, debugAttributes);
                ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);
            }
            catch (Exception ex)
            {
                Logger.LogError("Location: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Location result, bool debugAttributes = false)
        {
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

                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "ServiceName":
                        result.ServiceName = field.Value.Trim();
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

                    case "AggregatedLocationsCount":
                        if (field.Value.Trim() != "")
                            if( int.TryParse(field.Value.Trim(), out int aggregatedLocationsCount))
                                result.AggregatedLocationsCount = aggregatedLocationsCount;
                        break;

                    case "AccountLocationAffiliation":
                        result.AccountLocationAffiliation = field.Value.Trim();
                        break;

                    case "DeviceLocationAffiliation":
                        result.DeviceLocationAffiliation = field.Value.Trim();
                        break;

                    case "GpsHorizontalAccuracy":
                        if (field.Value.Trim() != "")
                        {
                            string gpsHorizontalAccuracy = field.Value.Trim().Replace(",", ".");
                            result.GpsHorizontalAccuracy = Double.Parse(gpsHorizontalAccuracy, CultureInfo.InvariantCulture);
                        }
                            
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Location Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Location result, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            foreach (var modelField in modelFieldElements)
            {
                try
                {

                    switch (modelField.Attribute("name").Value)
                    {
                        case "Position":
                            var coordinateModel = modelField.Element(xNamespace + "model");

                            if (coordinateModel == null)
                            {
                                break;
                            }

                            result.Position = Coordinate.ParseModel(coordinateModel);
                            break;

                        case "Address":
                            var addressModel = modelField.Element(xNamespace + "model");

                            if (addressModel == null)
                            {
                                break;
                            }
                            result.Address = StreetAddress.ParseModel(addressModel);
                            break;

                        default:
                            if (debugAttributes)
                            {
                                Logger.LogAttribute("Location Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                            }
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing location modelField: " + ex.Message);
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Location result, bool debugAttributes = false)
        {
            IUfedModelParser<Location>.CheckMultiFields<Location>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Location result, bool debugAttributes = false)
        {
            IUfedModelParser<Location>.CheckMultiModelFields<Location>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
