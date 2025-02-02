using System;
using System.Collections.Generic;
using System.Linq;
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
        public string BSSId { get; set; }
        public DateTime LastAutoConnection { get; set; }
        public DateTime LastConnection { get; set; }
        public string SecurityMode { get; set; }
        public string SSId { get; set; }
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

        public static WirelessNetwork ParseModel(XElement wirelessNetworkNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            WirelessNetwork result = new WirelessNetwork();

            var fieldElements = wirelessNetworkNode.Elements(xNamespace + "field");
            var multiFieldElements = wirelessNetworkNode.Elements(xNamespace + "multiField");
            var multiModelFieldElements = wirelessNetworkNode.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "BSSId":
                        result.BSSId = field.Value.Trim();
                        break;

                    case "SSId":
                        result.SSId = field.Value.Trim();
                        break;

                    case "SecurityMode":
                        result.SecurityMode = field.Value.Trim();
                        break;

                    case "LastConnection":
                        if (field.Value.Trim() != "")
                            result.LastConnection = DateTime.Parse(field.Value.Trim());
                        break;

                    case "LastAutoConnection":
                        if (field.Value.Trim() != "")
                            result.LastAutoConnection = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("WirelessNetwork Parser: Unhandled field: " + field.Attribute("name").Value);
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
                            Console.WriteLine("WirelessNetwork Parser: Unhandled multiField: " + multiField.Attribute("name").Value);
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
                            Console.WriteLine("WirelessNetwork Parser: Unhandled multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
        #endregion
    }
}
