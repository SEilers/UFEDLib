using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{

    [Serializable]
    public class UserAccount : ModelBase, IUfedModelParser<UserAccount>
    {
        public static string GetXmlModelType()
        {
            return "UserAccount";
        }


        #region fields
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Password { get; set; } = "";
        public string ServerAddress { get; set; } = "";
        /// <summary>
        /// The app or service from which the account was extracted.
        /// </summary>
        public string ServiceType { get; set; } = "";
        public DateTime TimeCreated { get; set; } = DateTime.MinValue;
        public string Username { get; set; } = "";
        #endregion

        #region multiFields
        public List<string> Notes { get; set; } = new List<string>();
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
        /// UserAccount entries collection.
        /// </summary>
        public List<ContactEntry> Entries { get; set; } = new List<ContactEntry>();

        /// <summary>
        /// Organizations collection.
        /// </summary>
        public List<Organization> Organizations { get; set; } = new List<Organization>();

        /// <summary>
        /// UserAccount Photos
        /// </summary>
        public List<ContactPhoto> Photos { get; set; } = new List<ContactPhoto>();
        #endregion

        #region Parsers
       
        public static UserAccount ParseModel(XElement element, bool debugAttributes = false)
        {
           return DefaultModelParser<UserAccount>(element, debugAttributes);
        }
        public static List<UserAccount> ParseMultiModel(XElement userAccountsElement, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<UserAccount>(userAccountsElement, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, UserAccount result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "Id":
                        result.Id = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "Password":
                        result.Password = field.Value.Trim();
                        break;

                    case "ServerAddress":
                        result.ServerAddress = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "ServiceType":
                        result.ServiceType = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "TimeCreated":
                        if (field.Value.Trim() != "")
                            result.TimeCreated = DateTime.Parse(field.Value.Trim());
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Username":
                        result.Username = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("UserAccount Parser: Unknown field: " + fieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, UserAccount result, bool debugAttributes = false)
        {
            IUfedModelParser<UserAccount>.CheckModelFields<UserAccount>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, UserAccount result, bool debugAttributes = false)
        {
            foreach (var multiField in multiFieldElements)
            {
                var multiFieldName = ModelBase.GetAttributeValueOrLog(multiField, "name", debugAttributes);
                if (multiFieldName == null) continue;

                switch (multiFieldName)
                {
                    case "Notes":
                        result.Notes = multiField.Elements().Select(x => x.Value.Trim()).ToList();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("UserAccount Parser: Unknown multiField: " + multiFieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, UserAccount result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                var multiModelFieldName = ModelBase.GetAttributeValueOrLog(multiModelField, "name", debugAttributes);
                if (multiModelFieldName == null) continue;

                switch (multiModelFieldName)
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
                            Logger.LogAttribute("UserAccount Parser: Unknown multiModelField: " + multiModelFieldName);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
