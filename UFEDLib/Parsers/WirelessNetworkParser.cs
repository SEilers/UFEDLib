using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class WirelessNetworkParser
    {
        public static WirelessNetwork Parse(XElement wirelessNetworkNode, bool debugAttributes = false)
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
                            Console.WriteLine("WirelessNetworkParser: Unhandled field: " + field.Attribute("name").Value);
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
                            Console.WriteLine("WirelessNetworkParser: Unhandled multiField: " + multiField.Attribute("name").Value);
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
                            Console.WriteLine("WirelessNetworkParser: Unhandled multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
    }
}
