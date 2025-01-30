﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


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

        public string AdditionalInfo { get; set; }

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
        /// <summary>
        /// Contact Notes.
        /// </summary>
        public List<string> Notes { get; set; } = new List<string>();
        public List<string> InteractionStatuses { get; set; } = new List<string>();
        public List<string> UserTags { get; set; } = new List<string>();
        #endregion


        #region models
        #endregion

        #region multiModels

        public Dictionary<string, string> AdditionalInfos { get; set; } = new Dictionary<string, string>();

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


        public static Contact ParseModel(XElement contactNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Contact result = new Contact();
            result.ParseAttributes(contactNode);

            var fieldElements = contactNode.Elements(xNamespace + "field");
            var multiFieldElements = contactNode.Elements(xNamespace + "multiField");
            var multiModelFieldElements = contactNode.Elements(xNamespace + "multiModelField");

            List<string> debugAttributesList = new List<string>();

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "AdditionalInfo":
                        result.AdditionalInfo = field.Value.Trim();
                        break;

                    case "Id":
                        result.Id = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Group":
                        result.Group = field.Value.Trim();
                        break;

                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "TimeContacted":
                        if (field.Value.Trim() != "")
                            result.TimeContacted = DateTime.Parse(field.Value.Trim());
                        break;

                    case "TimeCreated":
                        if (field.Value.Trim() != "")
                            result.TimeCreated = DateTime.Parse(field.Value.Trim());
                        break;

                    case "TimeModified":
                        if (field.Value.Trim() != "")
                            result.TimeModified = DateTime.Parse(field.Value.Trim());
                        break;

                    case "TimesContacted":
                        result.TimesContacted = int.Parse(field.Value.Trim());
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
                            string debugAttrubuteText = "ContactParser: Unknown field: " + field.Attribute("name").Value;
                            if (!debugAttributesList.Contains(debugAttrubuteText))
                            {
                                debugAttributesList.Add(debugAttrubuteText);
                            }
                        }
                        break;
                }
            }

            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "Notes":
                        result.Notes = multiField.Elements(xNamespace + "field").Select(x => x.Value.Trim()).ToList();
                        break;

                    case "InteractionStatuses":
                        result.InteractionStatuses = multiField.Elements(xNamespace + "field").Select(x => x.Value.Trim()).ToList();
                        break;

                    case "UserTags":
                        result.UserTags = multiField.Elements(xNamespace + "field").Select(x => x.Value.Trim()).ToList();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            string debugAttrubuteText = "ContactParser: Unknown multiField: " + multiField.Attribute("name").Value;
                            if (!debugAttributesList.Contains(debugAttrubuteText))
                            {
                                debugAttributesList.Add(debugAttrubuteText);
                            }
                        }
                        break;
                }
            }

            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "AdditionalInfo":
                        break;

                    case "Photos":
                        result.Photos = ContactPhoto.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Entries":
                        result.Entries = ContactEntry.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Addresses":
                        result.Addresses = StreetAddress.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Organizations":
                        result.Organizations = Organization.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            string debugAttrubuteText = "ContactParser: Unknown multiModelField: " + multiModelField.Attribute("name").Value;
                            if (!debugAttributesList.Contains(debugAttrubuteText))
                            {
                                debugAttributesList.Add(debugAttrubuteText);
                            }
                        }
                        break;
                }
            }

            if (debugAttributes)
            {
                foreach (string debugAttributeText in debugAttributesList)
                {
                    Console.WriteLine(debugAttributeText);
                }
            }

            return result;
        }
        #endregion


    }
}
