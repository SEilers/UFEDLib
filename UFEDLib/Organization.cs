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
      
        public static Organization ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Organization>(element, debugAttributes);
        }

        public static List<Organization> ParseMultiModel(XElement oraganizationElement, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Organization>(oraganizationElement, debugAttributes);
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
