using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class MobileCard : ModelBase, IUfedModelParser<MobileCard>
    {
        public static string GetXmlModelType()
        {
            return "MobileCard";
        }

        #region fields
        public DateTime ActivationTime { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public string Name { get; set; }
        public DateTime PurchaseTime { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        public Organization Organization { get; set; }
        #endregion

        #region multiModels
        public Dictionary<string, string> AdditionalInfo { get; set; } = new Dictionary<string, string>();
        #endregion

        #region Parsers
      
        public static MobileCard ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<MobileCard>(element, debugAttributes);
        }

        public static List<MobileCard> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<MobileCard>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, MobileCard result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "ActivationTime":
                        if (field.Value.Trim() != "")
                            result.ActivationTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Description":
                        result.Description = field.Value.Trim();
                        break;

                    case "ExpirationTime":
                        if (field.Value.Trim() != "")
                            result.ExpirationTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "ModifyTime":
                        if (field.Value.Trim() != "")
                            result.ModifyTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "PurchaseTime":
                        if (field.Value.Trim() != "")
                            result.PurchaseTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
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
                            Logger.LogAttribute("MobileCard Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, MobileCard result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    case "Organization":
                        result.Organization = Organization.ParseModel(modelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("MobileCard Parser: Unknown field: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, MobileCard result, bool debugAttributes = false)
        {
            IUfedModelParser<MobileCard>.CheckMultiFields<MobileCard>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, MobileCard result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "AdditionalInfo":
                        var kvModelsAdditionalInfo = KeyValueModel.ParseMultiModel(multiModelField, debugAttributes);
                        foreach (var kvModel in kvModelsAdditionalInfo)
                        {
                            if (!string.IsNullOrEmpty(kvModel.Key) && !string.IsNullOrEmpty(kvModel.Value))
                            {
                                result.AdditionalInfo[kvModel.Key] = kvModel.Value;
                            }
                        }
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("MobileCard Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
