﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    public class CallParser
    {
        public static List<Call> ParseCalls(XElement callsElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Call> result = new List<Call>();

            IEnumerable<XElement> callElements = callsElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Call");

            foreach (var callElement in callElements)
            {
                try
                {
                    result.Add(Parse(callElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing call: " + ex.Message);
                }
            }

            return result;
        }
        public static Call Parse(XElement callNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Call result = new Call();

            result.ParseAttributes(callNode);

            try
            {
                var fieldElements = callNode.Elements(xNamespace + "field");
                var multiFieldElements = callNode.Elements(xNamespace + "multiField");
                var multiModelFieldElements = callNode.Elements(xNamespace + "multiModelField");

                foreach ( var field in fieldElements) 
                {
                    switch(field.Attribute("name").Value)
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
                            if( debugAttributes)
                            {
                                Console.WriteLine("CallParse: Unknown field: " + field.Attribute("name").Value);
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
                                Console.WriteLine("CallParser: Unknown multiAttribute: " + multiField.Attribute("name").Value);
                            }
                            break;
                    }
                }

                foreach (var multiModelField in multiModelFieldElements)
                {
                    switch (multiModelField.Attribute("name").Value)
                    {
                        case "Parties":
                            result.Parties = PartyParser.ParseParties(multiModelField, debugAttributes);
                            break;

                        default:
                            if (debugAttributes)
                            {
                                Console.WriteLine("CallParser: Unknown multiModelAttribute: " + multiModelField.Attribute("name").Value);
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
    }

}
