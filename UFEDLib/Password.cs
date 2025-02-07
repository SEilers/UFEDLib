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
        public string Source { get; set; }
        public string UserMapping { get; set; }

        public string ServiceIdentifier { get; set; }
        public string AccessGroup { get; set; }
        public string Account { get; set; }
        // The password itself
        public string Data { get; set; }
        public string GenericAttribute { get; set; }
        public string Label { get; set; }
        public string Server { get; set; }
        public string Service { get; set; }

        // enum with the following values: "Default", "Key", "Secret", "Token"
        public string Type { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion

        #region Parsers
        public static List<Password> ParseMultiModel(XElement passwordsElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            List<Password> result = new List<Password>();
          
            IEnumerable<XElement> passwordElements = passwordsElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Password");

            foreach (var passwordElement in passwordElements)
            {
                try
                {
                    result.Add(ParseModel(passwordElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing password: " + ex.Message);
                }
            }

            return result;
        }

        public static Password ParseModel(XElement passwordElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Password result = new Password();
            result.ParseAttributes(passwordElement);

            var fieldElements = passwordElement.Elements(xNamespace + "field");
            var multiFieldElements = passwordElement.Elements(xNamespace + "multiField");
            var multiModelFieldElements = passwordElement.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "AccessGroup":
                        result.AccessGroup = field.Value.Trim();
                        break;

                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Data":
                        result.Data = field.Value.Trim();
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

                    case "Type":
                        result.Type = field.Value.Trim();
                        break;

                    case "DateCreated":
                        if (field.Value.Trim() != "")
                            result.DateCreated = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DateModified":
                        if (field.Value.Trim() != "")
                            result.DateModified = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Password Parser: Unhandled field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Password Parser: Unhandled multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Password Parser: Unhandled multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
        #endregion
    }
}
