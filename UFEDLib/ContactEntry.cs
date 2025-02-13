using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class ContactEntry : ModelBase, IUfedModelParser<ContactEntry>
    {
        public static string GetXmlModelType()
        {
            return "ContactEntry";
        }

        #region fields
        /// <summary>
        /// Entry category (work, home etc).
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Entry domain (phone number, email, web address etc)
        /// </summary>
        public string Domain { get; set; }

        public string UserMapping { get; set; }

        /// <summary>
        /// Entry value (phone number or email string).
        /// </summary>
        public string Value { get; set; }
        #endregion

        #region Parsers
        public static List<ContactEntry> ParseMultiModel(XElement ContactEntriesElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<ContactEntry> result = new List<ContactEntry>();

            IEnumerable<XElement> contactEntries = ContactEntriesElement.Elements(xNamespace + "model")
                .Where(x => x.Attribute("type").Value == "UserID" ||
                    x.Attribute("type").Value == "PhoneNumber" ||
                    x.Attribute("type").Value == "EMailAddress" ||
                    x.Attribute("type").Value == "WebAddress");

            foreach (XElement contactEntry in contactEntries)
            {
                ContactEntry c = ParseModel(contactEntry, debugAttributes);
                result.Add(c);
            }

            return result;
        }

        public static ContactEntry ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            ContactEntry result = new ContactEntry();
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
                Logger.LogError("ContactEntry: Error parsing xml reader attributes: " + ex.Message);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, ContactEntry result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Value":
                        result.Value = field.Value.Trim();
                        break;

                    case "Category":
                        result.Category = field.Value.Trim();
                        break;

                    case "Domain":
                        result.Domain = field.Value.Trim();
                        break;
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("ContactEntry Parser: Unknown attribute: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, ContactEntry result, bool debugAttributes = false)
        {
            IUfedModelParser<ContactEntry>.CheckModelFields<ContactEntry>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, ContactEntry result, bool debugAttributes = false)
        {
            IUfedModelParser<ContactEntry>.CheckMultiFields<ContactEntry>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, ContactEntry result, bool debugAttributes = false)
        {
            IUfedModelParser<ContactEntry>.CheckMultiModelFields<ContactEntry>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
