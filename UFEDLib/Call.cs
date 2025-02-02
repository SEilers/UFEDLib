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

        public TimeSpan Duration { get; set; }

        public string NetworkCode { get; set; }

        public string NetworkName { get; set; }

        public string Source { get; set; }

        public string Status { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Type { get; set; }

        public string UserMapping { get; set; }

        public bool VideoCall { get; set; }
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
        public static Call ParseModel(XElement callNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Call result = new Call();

            result.ParseAttributes(callNode);

            try
            {
                var fieldElements = callNode.Elements(xNamespace + "field");
                var multiFieldElements = callNode.Elements(xNamespace + "multiField");
                var multiModelFieldElements = callNode.Elements(xNamespace + "multiModelField");

                foreach (var field in fieldElements)
                {
                    switch (field.Attribute("name").Value)
                    {
                        case "Type":
                            result.Type = field.Value.Trim();
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
                            result.VideoCall = bool.Parse(field.Value.Trim());
                            break;
                        case "CountryCode":
                            result.CountryCode = field.Value.Trim();
                            break;
                        default:
                            if (debugAttributes)
                            {
                                Console.WriteLine("Call Parser:  Unknown field: " + field.Attribute("name").Value);
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
                                Console.WriteLine("Call Parser: Unknown multiAttribute: " + multiField.Attribute("name").Value);
                            }
                            break;
                    }
                }

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
                                Console.WriteLine("Call Parser: Unknown multiModelAttribute: " + multiModelField.Attribute("name").Value);
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }
        #endregion

    }
}
