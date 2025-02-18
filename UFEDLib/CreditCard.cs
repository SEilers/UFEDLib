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
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            CreditCard result = new CreditCard();

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
                Logger.LogError("CreditCard: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static List<CreditCard> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<CreditCard> result = new List<CreditCard>();

            IEnumerable<XElement> creditCardElements = element.Elements(xNamespace + "model").Where(element => element.Attribute("type").Value == "CreditCard");

            foreach (XElement creditCardElement in creditCardElements)
            {
                CreditCard cc = ParseModel(creditCardElement, debugAttributes);
                result.Add(cc);
            }

            return result;
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
                switch (modelField.Attribute("name").Value)
                {
                    case "BillingAddress":
                        result.BillingAddress = StreetAddress.ParseModel(modelField.Element("model"), debugAttributes);
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
