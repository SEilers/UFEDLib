using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Password : ModelBase, IUfedModelParser<Password>
    {
        public static string GetXmlModelType()
        {
            return "Password";
        }

        #region fields
        public string AccessGroup { get; set; } = "";
        public string Account { get; set; } = "";
        // The password itself
        public string Data { get; set; } = "";
        public DateTime DateCreated { get; set; } = DateTime.MinValue;
        public DateTime DateModified { get; set; } = DateTime.MinValue;
        public string GenericAttribute { get; set; } = "";
        public string Label { get; set; } = "";
        public string Server { get; set; } = "";
        public string Service { get; set; } = "";
        public string Type { get; set; } = "";
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion

        #region Parsers
        public static Password ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Password>(element, debugAttributes);
        }
        public static List<Password> ParseMultiModel(XElement passwordsElement, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Password>(passwordsElement, debugAttributes);
        }


        public static void ParseFields(IEnumerable<XElement> fieldElements, Password result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "AccessGroup":
                        result.AccessGroup = field.Value.Trim();
                        break;

                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Data":
                        result.Data = field.Value.Trim();
                        break;

                    case "DateCreated":
                        if (field.Value.Trim() != "")
                            result.DateCreated = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DateModified":
                        if (field.Value.Trim() != "")
                            result.DateModified = DateTime.Parse(field.Value.Trim());
                        break;

                    case "GenericAttribute":
                        result.GenericAttribute = field.Value.Trim();
                        break;

                    case "Label":
                        result.Label = field.Value.Trim();
                        break;

                    case "Server":
                        result.Server = field.Value.Trim();
                        break;

                    case "Service":
                        result.Service = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
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
                            Logger.LogAttribute("Password Parser: Unknown field: " + fieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Password result, bool debugAttributes = false)
        {
            IUfedModelParser<Password>.CheckModelFields<Password>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Password result, bool debugAttributes = false)
        {
            IUfedModelParser<Password>.CheckMultiFields<Password>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Password result, bool debugAttributes = false)
        {
            IUfedModelParser<Password>.CheckMultiModelFields<Password>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
