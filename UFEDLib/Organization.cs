using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Organization : ModelBase, IUfedModelParser<Organization>
    {
        public static string GetXmlModelType()
        {
            return "Organization";
        }

        #region fields
        /// <summary>
        /// Oragnization name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Contact’s position in the organization
        /// </summary>
        public string Position { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion

        #region Parsers
        public static List<Organization> ParseMultiModel(XElement oraganizationElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Organization> result = new List<Organization>();

            IEnumerable<XElement> organizations = oraganizationElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Organization");

            foreach (XElement organization in organizations)
            {
                Organization o = ParseModel(organization, debugAttributes);
                result.Add(o);
            }

            return result;
        }

        public static Organization ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Organization result = new Organization();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var modelFieldElements = element.Elements(xNamespace + "modelField");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            ParseFields(fieldElements, result, debugAttributes);
            ParseModelFields(modelFieldElements, result, debugAttributes);
            ParseMultiFields(multiFieldElements, result, debugAttributes);
            ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Organization result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "Position":
                        result.Position = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Organization Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Organization result, bool debugAttributes = false)
        {
            IUfedModelParser<Organization>.CheckModelFields<Organization>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Organization result, bool debugAttributes = false)
        {
            IUfedModelParser<Organization>.CheckMultiFields<Organization>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Organization result, bool debugAttributes = false)
        {
            IUfedModelParser<Organization>.CheckMultiModelFields<Organization>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
