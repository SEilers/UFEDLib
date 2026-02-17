using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class TransferOfFunds : ModelBase, IUfedModelParser<TransferOfFunds>
    {
        public static string GetXmlModelType()
        {
            return "TransferOfFunds";
        }

        #region fields
        public DateTime DateProcessed { get; set; } = DateTime.MinValue;
        public DateTime DateSent { get; set; } = DateTime.MinValue;
        public string Description { get; set; } = "";
        public string Status { get; set; } = "";
        public string TransferType { get; set; } = "";
        #endregion

        #region models
        public Price? Fee { get; set; } = null;
        public Price? TransferAmount { get; set; } = null;
        #endregion

        #region multiModels
        public List<Party> Participants { get; set; } = new List<Party>();
        #endregion

        #region parsers
        public static TransferOfFunds ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<TransferOfFunds>(element, debugAttributes);
        }

        public static List<TransferOfFunds> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<TransferOfFunds>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, TransferOfFunds result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "DateProcessed":
                        if (field.Value.Trim() != "")
                            result.DateProcessed = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DateSent":
                        if (field.Value.Trim() != "")
                            result.DateSent = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Description":
                        result.Description = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Status":
                        result.Status = field.Value.Trim();
                        break;

                    case "TransferType":
                        result.TransferType = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("TransferOfFunds Parser: Unknown field: " + fieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, TransferOfFunds result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                XNamespace ns = modelField.Name.Namespace;
                XElement modelElement = modelField.Element(ns + "model");

                switch (modelField.Attribute("name").Value)
                {
                    case "Fee":
                        result.Fee = Price.ParseModel(modelElement, debugAttributes);
                        break;

                    case "TransferAmount":
                        result.TransferAmount = Price.ParseModel(modelElement, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("TransferOfFunds Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, TransferOfFunds result, bool debugAttributes = false)
        {
            IUfedModelParser<TransferOfFunds>.CheckMultiFields<TransferOfFunds>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, TransferOfFunds result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                var multiModelFieldName = ModelBase.GetAttributeValueOrLog(multiModelField, "name", debugAttributes);
                if (multiModelFieldName == null) continue;

                switch (multiModelFieldName)
                {
                    case "Participants":
                        result.Participants = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("TransferOfFunds Parser: Unknown multiModelField: " + multiModelFieldName);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}