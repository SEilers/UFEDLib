using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class ActivitySensorDataSample : ModelBase, IUfedModelParser<ActivitySensorDataSample>
    {
        public static string GetXmlModelType()
        {
            return "ActivitySensorDataSample";
        }

        #region fields
        public DateTime DateSampled { get; set; } = DateTime.MinValue;
        public DateTime DateEnded { get; set; } = DateTime.MinValue;
        public double Quantity { get; set; } = 0;
        #endregion

        #region parsers
        public static ActivitySensorDataSample ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<ActivitySensorDataSample>(element, debugAttributes);
        }

        public static List<ActivitySensorDataSample> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<ActivitySensorDataSample>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, ActivitySensorDataSample result, bool debugAttributes = false)
        {


            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "DateSampled":
                        if(DateTime.TryParse(field.Value.Trim(), out DateTime dateSampled))
                        {
                            result.DateSampled = dateSampled;
                        }
                        break;

                    case "DateEnded":
                        if (DateTime.TryParse(field.Value.Trim(), out DateTime dateEnded))
                        {
                            result.DateEnded = dateEnded;
                        }
                        break;

                    case "Quantity":
                        if (double.TryParse(field.Value.Trim(), out double quantity))
                        {
                            result.Quantity = quantity;
                        }
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ActivitySensorDataSample Parser: Unknown field: " + fieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, ActivitySensorDataSample result, bool debugAttributes = false)
        {
            IUfedModelParser<ActivitySensorDataSample>.CheckModelFields<ActivitySensorDataSample>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, ActivitySensorDataSample result, bool debugAttributes = false)
        {
            IUfedModelParser<ActivitySensorDataSample>.CheckMultiFields<ActivitySensorDataSample>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, ActivitySensorDataSample result, bool debugAttributes = false)
        {
            IUfedModelParser<ActivitySensorDataSample>.CheckMultiModelFields<ActivitySensorDataSample>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}