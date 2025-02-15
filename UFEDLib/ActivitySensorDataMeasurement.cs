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
        public string DeviceName { get; set; }
        public string MeasuredVariableType { get; set; }
        public string Source { get; set; }
        public string Unit { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region multiModels
        public List<ActivitySensorDataSample> Samples { get; set; }
        #endregion

        #region parsers
        public static ActivitySensorDataMeasurement ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            ActivitySensorDataMeasurement result = new ActivitySensorDataMeasurement();

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
                Logger.LogError("ActivitySensorDataMeasurement: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static List<ActivitySensorDataMeasurement> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<ActivitySensorDataMeasurement> result = new List<ActivitySensorDataMeasurement>();

            IEnumerable<XElement> ActivitySensorDataMeasurementElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "ActivitySensorDataMeasurement");

            foreach (XElement ActivitySensorDataMeasurementElement in ActivitySensorDataMeasurementElements)
            {
                ActivitySensorDataMeasurement em = ParseModel(ActivitySensorDataMeasurementElement, debugAttributes);
                result.Add(em);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, ActivitySensorDataMeasurement result, bool debugAttributes = false)
        {

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "DeviceName":
                        result.DeviceName = field.Value.Trim();
                        break;

                    case "MeasuredVariableType":
                        result.MeasuredVariableType = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
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