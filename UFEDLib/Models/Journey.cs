using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Journey : ModelBase, IUfedModelParser<Journey>
    {
        public static string GetXmlModelType()
        {
            return "Journey";
        }

        #region fields
        public string Account { get; set; } = "";
        public DateTime EndTime { get; set; } = DateTime.MinValue;
        public string Name { get; set; } = "";
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public string Type { get; set; } = "";
        #endregion

        #region models
        public Location? FromPoint { get; set; } = null;
        public Location? ToPoint { get; set; } = null;
        #endregion

        #region multiModels
        /// <summary>
        /// Journey locations
        /// </summary>
        public List<Location> WayPoints { get; set; } = new List<Location>();
        #endregion

        #region Parsers
     
        public static Journey ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Journey>(element, debugAttributes);
        }

        public static List<Journey> ParseMultiModel(XElement journeysElement, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Journey>(journeysElement, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Journey result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "StartTime":
                        if (field.Value.Trim() != "")
                            result.StartTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Type":
                        result.Type = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Journey Parser: Unknown field: " + fieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Journey result, bool debugAttributes = false)
        {
            foreach (var modelFieldElement in modelFieldElements)
            {
                XNamespace ns = modelFieldElement.Name.Namespace;
                XElement? modelElement = modelFieldElement.Element(ns + "model");
                if (modelElement == null) continue;

                var modelFieldName = ModelBase.GetAttributeValueOrLog(modelFieldElement, "name", debugAttributes);
                if (modelFieldName == null) continue;

                switch (modelFieldName)
                {
                    case "FromPoint":
                        result.FromPoint = Location.ParseModel(modelElement, debugAttributes);
                        break;

                    case "ToPoint":
                        result.ToPoint = Location.ParseModel(modelElement, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Journey Parser: Unknown modelField: " + modelFieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Journey result, bool debugAttributes = false)
        {
            IUfedModelParser<Journey>.CheckMultiFields<Journey>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Journey result, bool debugAttributes = false)
        {
            foreach (var multiModelFieldElement in multiModelFieldElements)
            {
                var multiModelFieldName = ModelBase.GetAttributeValueOrLog(multiModelFieldElement, "name", debugAttributes);
                if (multiModelFieldName == null) continue;

                switch (multiModelFieldName)
                {
                    case "WayPoints":
                        result.WayPoints = Location.ParseMultiModel(multiModelFieldElement, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Journey Parser: Unknown multiModelField: " + multiModelFieldName);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
