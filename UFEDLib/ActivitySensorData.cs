
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UFEDLib
{
    public class ActivitySensorData : ModelBase, IUfedModelParser<ActivitySensorData>
    {
        public static string GetXmlModelType()
        {
            return "ActivitySensorData";
        }
        public string UserMapping { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime CreationTime { get; set; }
        public string SourceDeviceType { get; set; }
        public double DistanceTraveled { get; set; }
        public double MaxSpeed { get; set; }
        public double FlightsClimbed { get; set; }

        public double MaxHeartrate { get; set; }
        public int TotalSampleCount { get; set; }

        public static ActivitySensorData ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            ActivitySensorData result = new ActivitySensorData();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var modelFieldElements = element.Elements(xNamespace + "modelField");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            ParseFields(fieldElements, result, debugAttributes);
            ParseModelFields(modelFieldElements, result, debugAttributes);
            ParseMultiFields(multiFieldElements, result, debugAttributes);
            ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);

            return result;
        }

        public static List<ActivitySensorData> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<ActivitySensorData> result = new List<ActivitySensorData>();

            IEnumerable<XElement> asElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "ActivitySensorData");

            foreach (XElement asElement in asElements)
            {
                ActivitySensorData asd = ParseModel(asElement, debugAttributes);
                result.Add(asd);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, ActivitySensorData result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "SourceDeviceType":
                        result.SourceDeviceType = field.Value.Trim();
                        break;

                    case "From":
                        if (field.Value.Trim() != "")
                            result.From = DateTime.Parse(field.Value.Trim());
                        break;

                    case "To":
                        if (field.Value.Trim() != "")
                            result.To = DateTime.Parse(field.Value.Trim());
                        break;

                    case "CreationTime":
                        if (field.Value.Trim() != "")
                            result.CreationTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DistanceTraveled":
                        if (field.Value.Trim() != "")
                        {
                            string distanceTraveled = field.Value.Trim().Replace(",", ".");
                            result.DistanceTraveled = double.Parse(distanceTraveled, CultureInfo.InvariantCulture);
                        }
                        break;

                    case "MaxSpeed":
                        if (field.Value.Trim() != "")
                        {
                            string maxSpeed = field.Value.Trim().Replace(",", ".");
                            result.MaxSpeed = double.Parse(maxSpeed, CultureInfo.InvariantCulture);
                        }
                        break;

                    case "MaxHeartrate":
                        if (field.Value.Trim() != "")
                        {
                            string maxHeartrate = field.Value.Trim().Replace(",", ".");
                            result.MaxHeartrate = double.Parse(maxHeartrate, CultureInfo.InvariantCulture);
                        }
                        break;

                    case "FlightsClimbed":
                        if (field.Value.Trim() != "")
                        {
                            string flightsClimbed = field.Value.Trim().Replace(",", ".");
                            result.FlightsClimbed = double.Parse(flightsClimbed, CultureInfo.InvariantCulture);
                        }
                        break;

                    case "TotalSampleCount":
                        if (field.Value.Trim() != "")
                            result.TotalSampleCount = int.Parse(field.Value.Trim());
                        break;


                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ActivitySensorData Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, ActivitySensorData result, bool debugAttributes = false)
        {
            IUfedModelParser<ActivitySensorData>.CheckModelFields<ActivitySensorData>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, ActivitySensorData result, bool debugAttributes = false)
        {
            IUfedModelParser<ActivitySensorData>.CheckMultiFields<ActivitySensorData>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, ActivitySensorData result, bool debugAttributes = false)
        {
            IUfedModelParser<ActivitySensorData>.CheckMultiModelFields<ActivitySensorData>(multiModelFieldElements, debugAttributes);
        }
    }
}
