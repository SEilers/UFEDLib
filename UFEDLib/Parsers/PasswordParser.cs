using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class PasswordParser
    {
        public static Password Parse(XElement passwordElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Password result = new Password();

            var fieldElements = passwordElement.Elements(xNamespace + "field");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
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
                }
            }

            return result;
        }
    }

}
