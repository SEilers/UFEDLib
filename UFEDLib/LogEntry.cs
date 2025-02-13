using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class LogEntry : ModelBase, IUfedModelParser<LogEntry>
    {
        public static string GetXmlModelType()
        {
            return "LogEntry";
        }

        #region fields
        public string UserMapping { get; set; }
        public string Source { get; set; }
        public string Identifier { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime EndTime { get; set; }
        public string Application { get; set; }
        public string Body { get; set; }
        public string Severity { get; set; }
        public int PID { get; set; }
        public int TID { get; set; }
        public int EffectiveUID { get; set; }
        #endregion


        #region Parsers
        public static LogEntry ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            LogEntry result = new LogEntry();

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
                Logger.LogError("LogEntry: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static List<LogEntry> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<LogEntry> result = new List<LogEntry>();

            IEnumerable<XElement> leElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "LogEntry");

            foreach (XElement leElement in leElements)
            {
                LogEntry dc = ParseModel(leElement, debugAttributes);
                result.Add(dc);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, LogEntry result, bool debugAttributes = false)
        {
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

                    case "Identifier":
                        result.Identifier = field.Value.Trim();
                        break;

                    case "Application":
                        result.Application = field.Value.Trim();
                        break;

                    case "Body":
                        result.Body = field.Value.Trim();
                        break;

                    case "Severity":
                        result.Severity = field.Value.Trim();
                        break;

                    case "PID":
                        if (field.Value.Trim() != "")
                            if( int.TryParse(field.Value.Trim(), out int pid))
                                result.PID = pid;
                        break;

                    case "TID":
                        if (field.Value.Trim() != "")
                            if( int.TryParse(field.Value.Trim(), out int tid))
                                result.TID = tid;
                        break;

                    case "EffectiveUID":
                        if (field.Value.Trim() != "")
                            if( int.TryParse(field.Value.Trim(), out int effectiveUID))
                                result.EffectiveUID = effectiveUID;
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("LogEntry Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, LogEntry result, bool debugAttributes = false)
        {
            IUfedModelParser<LogEntry>.CheckModelFields<LogEntry>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, LogEntry result, bool debugAttributes = false)
        {
            IUfedModelParser<LogEntry>.CheckMultiFields<LogEntry>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, LogEntry result, bool debugAttributes = false)
        {
            IUfedModelParser<LogEntry>.CheckMultiModelFields<LogEntry>(multiModelFieldElements, debugAttributes);
        }

        #endregion
    }
}
