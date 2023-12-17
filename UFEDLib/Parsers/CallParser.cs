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
