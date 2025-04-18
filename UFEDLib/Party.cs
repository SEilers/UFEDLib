﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Party : ModelBase, IUfedModelParser<Party>
    {
        public static string GetXmlModelType()
        {
            return "Party";
        }

        #region fields
        public DateTime DateDelivered { get; set; }
        public DateTime DatePlayed { get; set; }
        public DateTime DateRead { get; set; }
        public string Distance { get; set; }
        public string Id { get; set; }
        public string Identifier { get; set; }
        public string IPAddress { get; set; }
        public string IsGroupAdmin { get; set; }
        public bool IsPhoneOwner { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region multiFields
        public List<string> IPAddresses { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion

        public override string ToString()
        {
            return Name + " " + Identifier + " " + Role;
        }

        #region Parsers
     
        public static Party ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Party>(element, debugAttributes);
        }

        public static List<Party> ParseMultiModel(XElement partiesElement, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Party>(partiesElement, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Party result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "DateDelivered":
                        if (field.Value.Trim() != "")
                            result.DateDelivered = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DatePlayed":
                        if (field.Value.Trim() != "")
                            result.DatePlayed = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DateRead":
                        if (field.Value.Trim() != "")
                            result.DateRead = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Distance":
                        result.Distance = field.Value.Trim();
                        break;

                    case "Id":
                        result.Id = field.Value.Trim();
                        break;

                    case "Identifier":
                        result.Identifier = field.Value.Trim();
                        break;

                    case "IPAddress":
                        result.IPAddress = field.Value.Trim();
                        break;

                    case "IsGroupAdmin":
                        result.IsGroupAdmin = field.Value.Trim();
                        break;

                    case "IsPhoneOwner":
                        if (!string.IsNullOrEmpty(field.Value.Trim()))
                        {
                            result.IsPhoneOwner = bool.Parse(field.Value.Trim());
                        }
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "Role":
                        result.Role = field.Value.Trim();
                        break;

                    case "Status":
                        result.Status = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Party Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Party result, bool debugAttributes = false)
        {
            IUfedModelParser<Party>.CheckModelFields<Party>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Party result, bool debugAttributes = false)
        {
            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "IPAddresses":
                        result.IPAddresses = multiField.Elements().Select(x => x.Value.Trim()).ToList();
                        break;
                    
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Party Parser: Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Party result, bool debugAttributes = false)
        {
            IUfedModelParser<Party>.CheckMultiModelFields<Party>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}

