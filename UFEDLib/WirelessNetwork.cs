﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class WirelessNetwork : ModelBase, IUfedModelParser<WirelessNetwork>
    {
        public static string GetXmlModelType()
        {
            return "WirelessNetwork";
        }

        #region fields
        public string Source { get; set; }
        public string UserMapping { get; set; }
        public string ServiceIdentifier { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime EndTime { get; set; }
        public string BSSId { get; set; }
        public DateTime LastAutoConnection { get; set; }
        public DateTime LastConnection { get; set; }
        public string SecurityMode { get; set; }
        public string SSId { get; set; }
        public string NWConnectionType { get; set; }
        public string Password { get; set; }
        #endregion

        #region models
        public Coordinate Position { get; set; }
        #endregion



        #region Parsers
        public static List<WirelessNetwork> ParseMultiModel(XElement wirelessNetworksElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<WirelessNetwork> result = new List<WirelessNetwork>();

            IEnumerable<XElement> wirelessNetworkElements = wirelessNetworksElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "WirelessNetwork");

            foreach (var wirelessNetworkElement in wirelessNetworkElements)
            {
                try
                {
                    result.Add(ParseModel(wirelessNetworkElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing wireles network: " + ex.Message);
                }
            }

            return result;
        }

        public static WirelessNetwork ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            WirelessNetwork result = new WirelessNetwork();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var modelFieldElements = element.Elements(xNamespace + "modelField");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            ParseFields(fieldElements, result, debugAttributes);
            ParseModelFields(modelFieldElements, result, debugAttributes);
            ParseMultiFields(multiFieldElements, result, debugAttributes);
            ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);

            return result;
        }
        #endregion


        public static void ParseFields(IEnumerable<XElement> fieldElements, WirelessNetwork result, bool debugAttributes = false)
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

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "BSSId":
                        result.BSSId = field.Value.Trim();
                        break;

                    case "SSId":
                        result.SSId = field.Value.Trim();
                        break;

                    case "SecurityMode":
                        result.SecurityMode = field.Value.Trim();
                        break;

                    case "NWConnectionType":
                        result.NWConnectionType = field.Value.Trim();
                        break;

                    case "LastConnection":
                        if (field.Value.Trim() != "")
                            result.LastConnection = DateTime.Parse(field.Value.Trim());
                        break;

                    case "LastAutoConnection":
                        if (field.Value.Trim() != "")
                            result.LastAutoConnection = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Password":
                        result.Password = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("WirelessNetwork Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, WirelessNetwork result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    case "Position":
                        result.Position = Coordinate.ParseModel(modelField);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("WirelessNetwork Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, WirelessNetwork result, bool debugAttributes = false)
        {
            IUfedModelParser<WirelessNetwork>.CheckMultiFields<WirelessNetwork>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, WirelessNetwork result, bool debugAttributes = false)
        {
            IUfedModelParser<WirelessNetwork>.CheckMultiModelFields<WirelessNetwork>(multiModelFieldElements, debugAttributes);
        }
    }
}
