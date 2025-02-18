using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class StreetAddress : ModelBase, IUfedModelParser<StreetAddress>
    {
        public static string GetXmlModelType()
        {
            return "StreetAddress";
        }

        #region fields
        /// <summary>
        /// Same values as ContactEntry categories
        /// </summary>
        public string Category { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string HouseNumber { get; set; }
        public string Neighborhood { get; set; }
        public string POBox { get; set; }
        /// <summary>
        /// Address Postal Code or ZIP.
        /// </summary>
        public string PostalCode { get; set; }
        public string State { get; set; }
        /// <summary>
        /// Street information.
        /// </summary>
        public string Street1 { get; set; }
        /// <summary>
        /// Additional street information
        /// </summary>
        public string Street2 { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion

        #region Parsers
        public static List<StreetAddress> ParseMultiModel(XElement streetAddresssElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<StreetAddress> result = new List<StreetAddress>();

            IEnumerable<XElement> streetAddresses = streetAddresssElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "StreetAddress");

            foreach (XElement streetAddress in streetAddresses)
            {
                try
                {
                    StreetAddress s = ParseModel(streetAddress, debugAttributes);
                    result.Add(s);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing street address: " + ex.Message);
                }
            }

            return result;
        }

        public static StreetAddress ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            StreetAddress result = new StreetAddress();

            try
            {
                result.ParseAttributes(element);

                var fieldElements = element?.Elements(xNamespace + "field");
                var modelFieldElements = element?.Elements(xNamespace + "modelField");
                var multiFieldElements = element?.Elements(xNamespace + "multiField");
                var multiModelFieldElements = element?.Elements(xNamespace + "multiModelField");

                if(fieldElements != null) ParseFields(fieldElements, result, debugAttributes);
                if(modelFieldElements != null) ParseModelFields(modelFieldElements, result, debugAttributes);
                if(multiFieldElements != null) ParseMultiFields(multiFieldElements, result, debugAttributes);
                if(multiModelFieldElements != null) ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);
            }
            catch (Exception ex)
            {
                Logger.LogError("StreetAddress: Error parsing xml reader attributes: " + ex.Message);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, StreetAddress result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                try
                {

                    switch (field.Attribute("name").Value)
                    {
                        case "Category":
                            result.Category = field.Value.Trim();
                            break;

                        case "City":
                            result.City = field.Value.Trim();
                            break;

                        case "Country":
                            result.Country = field.Value.Trim();
                            break;

                        case "HouseNumber":
                            result.HouseNumber = field.Value.Trim();
                            break;

                        case "Neighborhood":
                            result.Neighborhood = field.Value.Trim();
                            break;

                        case "POBox":
                            result.POBox = field.Value.Trim();
                            break;

                        case "PostalCode":
                            result.PostalCode = field.Value.Trim();
                            break;

                        case "State":
                            result.State = field.Value.Trim();
                            break;

                        case "Street1":
                            result.Street1 = field.Value.Trim();
                            break;
                        case "Street2":
                            result.Street2 = field.Value.Trim();
                            break;

                        default:
                            if (debugAttributes)
                            {
                                Logger.LogAttribute("StreetAddress Parser: Unknown field: " + field.Attribute("name").Value);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing field elements in StreetAddress Parser" + ex.ToString());
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, StreetAddress result, bool debugAttributes = false)
        {
            IUfedModelParser<StreetAddress>.CheckModelFields<StreetAddress>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, StreetAddress result, bool debugAttributes = false)
        {
            IUfedModelParser<StreetAddress>.CheckMultiFields<StreetAddress>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, StreetAddress result, bool debugAttributes = false)
        {
            IUfedModelParser<StreetAddress>.CheckMultiModelFields<StreetAddress>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
