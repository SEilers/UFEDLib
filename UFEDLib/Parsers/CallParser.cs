using System;
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
        public static Call Parse(XElement callNode)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Call result = new Call();

            try
            {
                var fieldElements = callNode.Elements(xNamespace + "field");

                foreach ( var fieldElement in fieldElements) 
                {
                    switch(fieldElement.Attribute("name").Value)
                    {
                        case "Type":
                            result.Type = fieldElement.Value.Trim();
                            break;
                        case "TimeStamp":
                            result.TimeStamp = DateTime.Parse(fieldElement.Value.Trim());
                            break;
                        case "Duration":
                            result.Duration = TimeSpan.Parse(fieldElement.Value.Trim());
                            break;
                        case "Source":
                            result.Source = fieldElement.Value.Trim();
                            break;
                        case "NetworkName":
                            result.NetworkName = fieldElement.Value.Trim();
                            break;
                        case "NetworkCode":
                            result.NetworkCode = fieldElement.Value.Trim();
                            break;
                        case "VideoCall":
                            result.VideoCall = bool.Parse(fieldElement.Value.Trim());
                            break;
                        case "CountryCode":
                            result.CountryCode = fieldElement.Value.Trim();
                            break;
                        default:
                            break;

                    }
                }

                var multiModelFieldElements = callNode.Elements(xNamespace + "multiModelField");

                foreach (var multiField in multiModelFieldElements)
                {
                    switch (multiField.Attribute("name").Value)
                    {
                        case "Parties":
                            result.Parties = PartyParser.ParseParties(multiField);
                            break;

                        default:
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
