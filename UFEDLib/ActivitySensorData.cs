
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int TotalSampleCount { get; set; }

        public static ActivitySensorData ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            ActivitySensorData result = new ActivitySensorData();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

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
                            result.From = DateTime.Parse( field.Value.Trim() ); 
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
                            result.DistanceTraveled = double.Parse(field.Value.Trim());
                        break;

                    case "MaxSpeed":
                        if (field.Value.Trim() != "")
                            result.MaxSpeed = double.Parse(field.Value.Trim());
                        break;

                    case "FlightsClimbed":
                        if (field.Value.Trim() != "")
                            result.FlightsClimbed = double.Parse(field.Value.Trim());
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

            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ActivitySensorData Parser: Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ActivitySensorData Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

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
    }
}
