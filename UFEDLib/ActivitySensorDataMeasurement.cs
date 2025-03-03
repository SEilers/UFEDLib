using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class ActivitySensorDataMeasurement : ModelBase, IUfedModelParser<ActivitySensorDataMeasurement>
    {
        public static string GetXmlModelType()
        {
            return "ActivitySensorDataMeasurement";
        }

        #region fields
        public double AverageValue { get; set; }
        public string DeviceName { get; set; }
        public double MaximumValue { get; set; }
        public string MeasuredVariableType { get; set; }
        public string Source { get; set; }
        public double TotalValue { get; set; }
        public string Unit { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region multiModels
        public List<ActivitySensorDataSample> Samples { get; set; }
        #endregion

        #region parsers
        public static ActivitySensorDataMeasurement ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<ActivitySensorDataMeasurement>(element, debugAttributes);
        }

        public static List<ActivitySensorDataMeasurement> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<ActivitySensorDataMeasurement>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, ActivitySensorDataMeasurement result, bool debugAttributes = false)
        {

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "AverageValue":
                        if (double.TryParse(field.Value.Trim(), out double averageValue))
                        {
                            result.AverageValue = averageValue;
                        }
                        break;

                    case "DeviceName":
                        result.DeviceName = field.Value.Trim();
                        break;

                    case "MaximumValue":
                        if (double.TryParse(field.Value.Trim(), out double maximumValue))
                        {
                            result.MaximumValue = maximumValue;
                        }
                        break;

                    case "MeasuredVariableType":
                        result.MeasuredVariableType = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "TotalValue":
                        if (double.TryParse(field.Value.Trim(), out double totalValue))
                        {
                            result.TotalValue = totalValue;
                        }
                        break;

                    case "Unit":
                        result.Unit = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ActivitySensorDataMeasurement Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, ActivitySensorDataMeasurement result, bool debugAttributes = false)
        {
            IUfedModelParser<ActivitySensorDataMeasurement>.CheckModelFields<ActivitySensorDataMeasurement>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, ActivitySensorDataMeasurement result, bool debugAttributes = false)
        {
            IUfedModelParser<ActivitySensorDataMeasurement>.CheckMultiFields<ActivitySensorDataMeasurement>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, ActivitySensorDataMeasurement result, bool debugAttributes = false)
        {
            foreach (var multiModelFieldElement in multiModelFieldElements)
            {
                switch (multiModelFieldElement.Attribute("name").Value)
                {
                    case "Samples":
                        result.Samples = ActivitySensorDataSample.ParseMultiModel(multiModelFieldElement, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ActivitySensorDataMeasurement Parser: Unknown multiModelField: " + multiModelFieldElement.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}