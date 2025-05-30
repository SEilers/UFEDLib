﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class CalendarEntry : ModelBase, IUfedModelParser<CalendarEntry>
    {
        public static string GetXmlModelType()
        {
            return "CalendarEntry";
        }

        #region fields
        public string Account { get; set; }
        public string Availability { get; set; }
        public string Category { get; set; }
        public string Class { get; set; }
        public string Details { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Priority { get; set; }
        public DateTime Reminder { get; set; }
        public string RepeatDay { get; set; }
        public int RepeatInterval { get; set; }
        public string RepeatRule { get; set; }
        public DateTime RepeatUntil { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public string Source { get; set; }
        public string Subject { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        public List<Attachment> Attachments { get;set; }
        public List<Party> Attendees { get; set; }
        #endregion



        #region Parsers

        public static CalendarEntry ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<CalendarEntry>(element, debugAttributes);
        }

        public static List<CalendarEntry> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<CalendarEntry>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, CalendarEntry result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Availability":
                        result.Availability = field.Value.Trim();
                        break;

                    case "Category":
                        result.Category = field.Value.Trim();
                        break;

                    case "Class":
                        result.Class = field.Value.Trim();
                        break;

                    case "Details":
                        result.Details = field.Value.Trim();
                        break;

                    case "EndDate":
                        if (field.Value.Trim() != "")
                            result.EndDate = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Location":
                        result.Location = field.Value.Trim();
                        break;

                    case "Priority":
                        result.Priority = field.Value.Trim();
                        break;

                    case "Reminder":
                        if (field.Value.Trim() != "")
                            result.Reminder = DateTime.Parse(field.Value.Trim());
                        break;

                    case "RepeatDay":
                        result.RepeatDay = field.Value.Trim();
                        break;

                    case "RepeatInterval":
                        if (field.Value.Trim() != "")
                            if (int.TryParse(field.Value.Trim(), out int repeatInterval))
                                result.RepeatInterval = repeatInterval;
                        break;

                    case "RepeatRule":
                        result.RepeatRule = field.Value.Trim();
                        break;

                    case "RepeatUntil":
                        if (field.Value.Trim() != "")
                            result.RepeatUntil = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "StartDate":
                        if (field.Value.Trim() != "")
                            result.StartDate = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Status":
                        result.Status = field.Value.Trim();
                        break;

                    case "Subject":
                        result.Subject = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("CalendarEntry Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, CalendarEntry result, bool debugAttributes = false)
        {
            IUfedModelParser<CalendarEntry>.CheckModelFields<CalendarEntry>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, CalendarEntry result, bool debugAttributes = false)
        {
            IUfedModelParser<CalendarEntry>.CheckMultiFields<CalendarEntry>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, CalendarEntry result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "Attachments":
                        result.Attachments = Attachment.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Attendees":
                        result.Attendees = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("CalendarEntry Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        #endregion
    }
}
