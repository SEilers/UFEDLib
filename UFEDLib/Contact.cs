using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib;


namespace UFEDLib
{
    [Serializable]
    public class Contact : ModelBase, IUfedModelParser<Contact>
    {
        public static string GetXmlModelType()
        {
            return "Contact";
        }

        #region fields
        public string Account { get; set; }
        public string Group { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// Contact Name.
        /// </summary>
        public string Name { get; set; }
        public string ServiceIdentifier { get; set; }
        /// <summary>
        /// Contact Source.
        /// </summary>
        public string Source { get; set; }
        public DateTime TimeContacted { get; set; }
        public int TimesContacted { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeModified { get; set; }
        public string UserMapping { get; set; }
        public string Type { get; set; }
        #endregion

        #region multiFields
        public List<string> InteractionStatuses { get; set; } = new List<string>();
        /// <summary>
        /// Contact Notes.
        /// </summary>
        public List<string> Notes { get; set; } = new List<string>();
        public List<string> UserTags { get; set; } = new List<string>();
        #endregion

        #region models
        #endregion

        #region multiModels
        public Dictionary<string, string> AdditionalInfo { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// Addresses collection.
        /// </summary>
        public List<StreetAddress> Addresses { get; set; } = new List<StreetAddress>();
        /// <summary>
        /// Contact entries collection.
        /// </summary>
        public List<ContactEntry> Entries { get; set; } = new List<ContactEntry>();
        /// <summary>
        /// Organizations collection.
        /// </summary>
        public List<Organization> Organizations { get; set; } = new List<Organization>();
        /// <summary>
        /// Contact Photos.
        /// </summary>
        public List<ContactPhoto> Photos { get; set; } = new List<ContactPhoto>();
        #endregion

        #region Parsers
        public static List<Contact> ParseMultiModel(XElement contactsElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Contact> result = new List<Contact>();

            IEnumerable<XElement> contacts = contactsElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Contact");

            foreach (XElement contact in contacts)
            {
                Contact c = ParseModel(contact, debugAttributes);
                result.Add(c);
            }

            return result;
        }


        public static Contact ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Contact result = new Contact();
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
                Logger.LogError("Contact: Error parsing xml reader attributes  " + ex.Message);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Contact result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Group":
                        result.Group = field.Value.Trim();
                        break;

                    case "Id":
                        result.Id = field.Value.Trim();
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

                    case "TimeContacted":
                        if (field.Value.Trim() != "")
                            result.TimeContacted = DateTime.Parse(field.Value.Trim());
                        break;

                    case "TimesContacted":
                        if (int.TryParse(field.Value.Trim(), out int timesContacted))
                        {
                            result.TimesContacted = timesContacted;
                        }
                        break;

                    case "TimeCreated":
                        if (field.Value.Trim() != "")
                            result.TimeCreated = DateTime.Parse(field.Value.Trim());
                        break;

                    case "TimeModified":
                        if (field.Value.Trim() != "")
                            result.TimeModified = DateTime.Parse(field.Value.Trim());
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
                            string debugAttrubuteText = "Contact Parser: Unknown field: " + field.Attribute("name").Value;
                            Logger.LogAttribute(debugAttrubuteText);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Contact result, bool debugAttributes = false)
        {
            IUfedModelParser<Contact>.CheckModelFields<Contact>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Contact result, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "InteractionStatuses":
                        result.InteractionStatuses = multiField.Elements(xNamespace + "field").Select(x => x.Value.Trim()).ToList();
                        break;

                    case "Notes":
                        result.Notes = multiField.Elements(xNamespace + "field").Select(x => x.Value.Trim()).ToList();
                        break;

                    case "UserTags":
                        result.UserTags = multiField.Elements(xNamespace + "field").Select(x => x.Value.Trim()).ToList();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            string debugAttrubuteText = "Contact Parser: Unknown multiField: " + multiField.Attribute("name").Value;
                            Logger.LogAttribute(debugAttrubuteText);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Contact result, bool debugAttributes = false)
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

                    case "Addresses":
                        result.Addresses = StreetAddress.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Entries":
                        result.Entries = ContactEntry.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Photos":
                        result.Photos = ContactPhoto.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Organizations":
                        result.Organizations = Organization.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            string debugAttrubuteText = "Contact Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value;
                            Logger.LogAttribute(debugAttrubuteText);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
