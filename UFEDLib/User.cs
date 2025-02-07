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
        public string UserMapping { get; set; }

        public string Identifier { get; set; }

        public long SerialNumber { get; set; }

        public string Name { get; set; }

        public DateTime TimeLastLoggedIn { get; set; }

        public string UserType { get; set; }
        #endregion


        #region Parsers
        public static User ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            User result = new User();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var modelFieldElements = element.Elements(xNamespace + "modelField");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Identifier":
                        result.Identifier = field.Value.Trim();
                        break;

                    case "SerialNumber":
                        if (field.Value.Trim() != "")
                            result.SerialNumber = long.Parse(field.Value.Trim());
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
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

            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("User Parser: Unknown modelField: " + modelField.Attribute("name").Value);
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
                            Logger.LogAttribute("User Parser: Unknown multiField: " + multiField.Attribute("name").Value);
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
                            Logger.LogAttribute("User Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }

        public static List<User> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<User> result = new List<User>();

            IEnumerable<XElement> uElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "User");

            foreach (XElement uElement in uElements)
            {
                User aul = ParseModel(uElement, debugAttributes);
                result.Add(aul);
            }

            return result;
        }
        #endregion
    }
}
