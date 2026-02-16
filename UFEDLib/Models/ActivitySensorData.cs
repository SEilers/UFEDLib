
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
    [Serializable]
    public class ActivitySensorData : ModelBase, IUfedModelParser<ActivitySensorData>
    {
        public static string GetXmlModelType()
        {
            return "ActivitySensorData";
        }

        #region fields
        public DateTime CreationTime { get; set; } = DateTime.MinValue;
        public string DeviceName { get; set; } = "";
        public double DistanceTraveled { get; set; } = 0.0;
        public double FlightsClimbed { get; set; } = 0.0;
        public DateTime From { get; set; } = DateTime.MinValue;
        public string Name { get; set; } = "";          
        public double MaxHeartrate { get; set; } = 0.0;
        public double MaxSpeed { get; set; } = 0.0;
        public string UserMapping { get; set; } = "";
        public string Source { get; set; } = "";
        public string SourceDeviceType { get; set; } = "";  
        public DateTime To { get; set; } = DateTime.MinValue;
        public int TotalSampleCount { get; set; } = 0;
        #endregion

        #region multiModels
        public List<ActivitySensorDataMeasurement> Measurements { get; set; }
        #endregion



        public static ActivitySensorData ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<ActivitySensorData>(element, debugAttributes);
        }

        public static List<ActivitySensorData> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<ActivitySensorData>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, ActivitySensorData result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "CreationTime":
                        if (field.Value.Trim() != "")
                            result.CreationTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DeviceName":
                        result.DeviceName = field.Value.Trim();
                        break;

                    case "DistanceTraveled":
                        if (field.Value.Trim() != "")
                        {
                            string distanceTraveled = field.Value.Trim().Replace(",", ".");
                            if (double.TryParse(distanceTraveled, CultureInfo.InvariantCulture, out double distanceTraveledValue))
                                result.DistanceTraveled = distanceTraveledValue;
                        }
                        break;

                    case "FlightsClimbed":
                        if (field.Value.Trim() != "")
                        {
                            string flightsClimbed = field.Value.Trim().Replace(",", ".");
                            if (double.TryParse(flightsClimbed, CultureInfo.InvariantCulture, out double flightsClimbedValue))
                                result.FlightsClimbed = flightsClimbedValue;
                        }
                        break;

                    case "From":
                        if (field.Value.Trim() != "")
                            result.From = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "MaxHeartrate":
                        if (field.Value.Trim() != "")
                        {
                            string maxHeartrate = field.Value.Trim().Replace(",", ".");
                            if (double.TryParse(maxHeartrate, CultureInfo.InvariantCulture, out double maxHeartrateValue))
                                result.MaxHeartrate = maxHeartrateValue;
                        }
                        break;

                    case "MaxSpeed":
                        if (field.Value.Trim() != "")
                        {
                            string maxSpeed = field.Value.Trim().Replace(",", ".");
                            if (double.TryParse(maxSpeed, CultureInfo.InvariantCulture, out double maxSpeedValue))
                                result.MaxSpeed = maxSpeedValue;
                        }
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "SourceDeviceType":
                        result.SourceDeviceType = field.Value.Trim();
                        break;

                    case "To":
                        if (field.Value.Trim() != "")
                            result.To = DateTime.Parse(field.Value.Trim());
                        break;

                    case "TotalSampleCount":
                        if (field.Value.Trim() != "")
                        {
                            if (int.TryParse(field.Value.Trim(), out int totalSampleCount))
                                result.TotalSampleCount = totalSampleCount;
                        }
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;


                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ActivitySensorData Parser: Unknown field: " + fieldName);
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
            foreach (var multiModelFieldElement in multiModelFieldElements)
            {
                switch (multiModelFieldElement.Attribute("name").Value)
                {
                    case "Measurements":
                        result.Measurements = ActivitySensorDataMeasurement.ParseMultiModel(multiModelFieldElement, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ActivitySensorData Parser: Unknown multiModelField: " + multiModelFieldElement.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
    }
}
