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
        public static WirelessNetwork Parse(XElement wirelessNetworkNode)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            WirelessNetwork result = new WirelessNetwork();

            var fieldElements = wirelessNetworkNode.Elements(xNamespace + "field");

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
                        result.LastConnection = DateTime.Parse(field.Value.Trim());
                        break;

                    case "LastAutoConnection":
                        result.LastAutoConnection = DateTime.Parse(field.Value.Trim());
                        break;
                }
            }

            return result;
        }
    }
}
