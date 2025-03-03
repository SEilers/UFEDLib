using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class User : ModelBase, IUfedModelParser<User>
    {
        public static string GetXmlModelType()
        {
            return "User";
        }

        #region fields
        public string Identifier { get; set; }
        public string Name { get; set; }
        public long SerialNumber { get; set; }
        public DateTime TimeLastLoggedIn { get; set; }
        public string UserMapping { get; set; }
        public string UserType { get; set; }
        #endregion

        #region multiModels
        public Dictionary<string, string> AdditionalInfo { get; set; } = new Dictionary<string, string>();
        #endregion


        #region Parsers
        public static User ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<User>(element, debugAttributes);
        }

        public static List<User> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<User>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, User result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Identifier":
                        result.Identifier = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "SerialNumber":
                        if (field.Value.Trim() != "")
                            result.SerialNumber = long.Parse(field.Value.Trim());
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "UserType":
                        result.UserType = field.Value.Trim();
                        break;

                    case "TimeLastLoggedIn":
                        if (field.Value.Trim() != "")
                            result.TimeLastLoggedIn = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("User Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, User result, bool debugAttributes = false)
        {
            IUfedModelParser<User>.CheckModelFields<User>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, User result, bool debugAttributes = false)
        {
            IUfedModelParser<User>.CheckMultiFields<User>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, User result, bool debugAttributes = false)
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

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("User Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
