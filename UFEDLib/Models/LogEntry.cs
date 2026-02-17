using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class LogEntry : ModelBase, IUfedModelParser<LogEntry>
    {
        public static string GetXmlModelType()
        {
            return "LogEntry";
        }

        #region fields
        public string Application { get; set; } = "";
        public string Body { get; set; } = "";
        public int EffectiveUID { get; set; } = 0;
        public DateTime EndTime { get; set; } = DateTime.MinValue;
        public string Identifier { get; set; } = "";
        public int PID { get; set; } = 0;
        public string Severity { get; set; } = "";
        public int TID { get; set; } = 0;
        public DateTime TimeStamp { get; set; } = DateTime.MinValue;
        #endregion

        #region Parsers
        public static LogEntry ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<LogEntry>(element, debugAttributes);
        }

        public static List<LogEntry> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<LogEntry>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, LogEntry result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "Application":
                        result.Application = field.Value.Trim();
                        break;

                    case "Body":
                        result.Body = field.Value.Trim();
                        break;

                    case "EffectiveUID":
                        if (field.Value.Trim() != "")
                            if (int.TryParse(field.Value.Trim(), out int effectiveUID))
                                result.EffectiveUID = effectiveUID;
                        break;

                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Identifier":
                        result.Identifier = field.Value.Trim();
                        break;

                    case "PID":
                        if (field.Value.Trim() != "")
                            if (int.TryParse(field.Value.Trim(), out int pid))
                                result.PID = pid;
                        break;

                    case "Severity":
                        result.Severity = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "TID":
                        if (field.Value.Trim() != "")
                            if (int.TryParse(field.Value.Trim(), out int tid))
                                result.TID = tid;
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("LogEntry Parser: Unknown field: " + fieldName);
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
