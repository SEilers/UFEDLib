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
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ServerAddress { get; set; }
        public string ServiceIdentifier { get; set; }
        /// <summary>
        /// The app or service from which the account was extracted.
        /// </summary>
        public string ServiceType { get; set; }
        public string Source { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Username { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region multiFields
        public List<string> Notes { get; set; }
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
        public static List<UserAccount> ParseMultiModel(XElement userAccountsElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            List<UserAccount> result = new List<UserAccount>();

            IEnumerable<XElement> userAccountElements = userAccountsElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "UserAccount");

            foreach (var userAccountElement in userAccountElements)
            {
                try
                {
                    result.Add(ParseModel(userAccountElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing user account: " + ex.Message);
                }
            }

            return result;
        }
        public static UserAccount ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            UserAccount result = new UserAccount();

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
                Logger.LogError("UserAccount: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, UserAccount result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Id":
                        result.Id = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "Username":
                        result.Username = field.Value.Trim();
                        break;

                    case "Password":
                        result.Password = field.Value.Trim();
                        break;

                    case "ServiceType":
                        result.ServiceType = field.Value.Trim();
                        break;

                    case "ServerAddress":
                        result.ServerAddress = field.Value.Trim();
                        break;

                    case "TimeCreated":
                        if (field.Value.Trim() != "")
                            result.TimeCreated = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("UserAccount Parser: Unknown field: " + field.Attribute("name").Value);
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
                switch (multiField.Attribute("name").Value)
                {
                    case "Notes":
                        result.Notes = multiField.Elements().Select(x => x.Value.Trim()).ToList();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("UserAccount Parser: Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, UserAccount result, bool debugAttributes = false)
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
                            Logger.LogAttribute("UserAccount Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
