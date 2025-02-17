using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Call : ModelBase, IUfedModelParser<Call>
    {
        public static string GetXmlModelType()
        {
            return "Call";
        }

        #region fields
        public string Account { get; set; }
        public string CountryCode { get; set; }
        public string Direction { get; set; }
        public string DisconnectionCause { get; set; }
        public TimeSpan Duration { get; set; }
        public string NetworkCode { get; set; }
        public string NetworkName { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Type { get; set; }
        public string UserMapping { get; set; }
        public string VideoCall { get; set; }
        #endregion

        #region multiModels
        public List<Party> Parties { get; set; } = new List<Party>();
        #endregion

        public override string ToString()
        {
            return Source + " " + Duration + " " + Parties.Count.ToString();
        }

        #region Parsers
        public static List<Call> ParseMultiModel(XElement callsElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Call> result = new List<Call>();

            IEnumerable<XElement> callElements = callsElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Call");

            foreach (var callElement in callElements)
            {
                try
                {
                    result.Add(ParseModel(callElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing call: " + ex.Message);
                }
            }

            return result;
        }
        public static Call ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Call result = new Call();
        
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
            catch (Exception e)
            {
                Logger.LogError("Call: Error parsing xml reader attributes " + e.Message);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Call result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Type":
                        result.Type = field.Value.Trim();
                        break;

                    case "DisconnectionCause":
                        result.DisconnectionCause = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Duration":
                        if (field.Value.Trim() != "")
                            result.Duration = TimeSpan.Parse(field.Value.Trim());
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Direction":
                        result.Direction = field.Value.Trim();
                        break;

                    case "Status":
                        result.Status = field.Value.Trim();
                        break;

                    case "NetworkName":
                        result.NetworkName = field.Value.Trim();
                        break;

                    case "NetworkCode":
                        result.NetworkCode = field.Value.Trim();
                        break;

                    case "VideoCall":
                        result.VideoCall = field.Value.Trim();
                        break;

                    case "CountryCode":
                        result.CountryCode = field.Value.Trim();
                        break;

                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Call Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;

                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Call result, bool debugAttributes = false)
        {
            IUfedModelParser<Call>.CheckModelFields<Call>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Call result, bool debugAttributes = false)
        {
            IUfedModelParser<Call>.CheckMultiFields<Call>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Call result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "Parties":
                        result.Parties = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Call Parser: Unknown multiModelAttribute: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion

    }
}
