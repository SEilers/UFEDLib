using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class CreditCard : ModelBase, IUfedModelParser<CreditCard>
    {
        public static string GetXmlModelType()
        {
            return "CreditCard";
        }

        #region fields
        public string Company { get; set; }
        public string CreditCardNumber { get; set; }
        public string CVV { get; set; }
        public DateTime DateLastUsed { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string NameOnCard { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        public StreetAddress BillingAddress { get; set; }
        #endregion

        #region Parsers
        public static CreditCard ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<CreditCard>(element, debugAttributes);
        }

        public static List<CreditCard> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<CreditCard>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, CreditCard result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Company":
                        result.Company = field.Value.Trim();
                        break;

                    case "CreditCardNumber":
                        result.CreditCardNumber = field.Value.Trim();
                        break;

                    case "CVV":
                        result.CVV = field.Value.Trim();
                        break;

                    case "DateLastUsed":
                        if (field.Value.Trim() != "")
                            result.DateLastUsed = DateTime.Parse(field.Value.Trim());
                        break;

                    case "ExpirationDate":
                        if (field.Value.Trim() != "")
                            result.ExpirationDate = DateTime.Parse(field.Value.Trim());
                        break;

                    case "NameOnCard":
                        result.NameOnCard = field.Value.Trim();
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
                            Logger.LogAttribute("CreditCard Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, CreditCard result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                XNamespace ns = modelField.Name.Namespace;
                XElement modelElement = modelField.Element(ns + "model");

                switch (modelField.Attribute("name").Value)
                {
                    case "BillingAddress":
                        result.BillingAddress = StreetAddress.ParseModel(modelElement.Element("model"), debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("CreditCard Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, CreditCard result, bool debugAttributes = false)
        {
            IUfedModelParser<CreditCard>.CheckMultiFields<CreditCard>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, CreditCard result, bool debugAttributes = false)
        {
            IUfedModelParser<CreditCard>.CheckMultiModelFields<CreditCard>(multiModelFieldElements, debugAttributes);
        }

        #endregion
    }
}
