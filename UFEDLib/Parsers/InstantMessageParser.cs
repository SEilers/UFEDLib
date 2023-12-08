using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class InstantMessageParser
    {

        public static InstantMessage Parse(XElement xElement)
        {

            InstantMessage result = new InstantMessage();
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            var fieldElements = xElement.Elements(xNamespace + "field");

            foreach (XElement field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Body":
                        result.Body = field.Value.Trim();
                        break;
                    case "Id":
                        result.Id = field.Value.Trim();
                        break;
                    case "SourceApplication":
                        result.SourceApplication = field.Value.Trim();
                        break;
                    case "TimeStamp":
                        result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;
                    case "Subject":
                        result.Subject = field.Value.Trim();
                        break;
                    case "DateRead":
                        result.DateRead = DateTime.Parse(field.Value.Trim());
                        break;
                    case "DateDelivered":
                        result.DateDelivered = DateTime.Parse(field.Value.Trim());
                        break;
                    case "Label":
                        result.Label = field.Value.Trim();
                        break;
                    case "PositionAddress":
                        result.PositionAddress = field.Value.Trim();
                        break;
                    case "ChatId":
                        result.ChatId = field.Value.Trim();
                        break;
                    case "Erased":
                        result.Erased = field.Value.Trim();
                        break;
                    case "Source":
                        result.Source = field.Value.Trim();
                        break;
                    case "FromIsOwner":
                        result.FromIsOwner = field.Value.Trim();
                        break;
                    case "Identifier":
                        result.Identifier = field.Value.Trim();
                        break;
                    case "Status":
                        result.Status = field.Value.Trim();
                        break;
                    case "Type":
                        result.Type = field.Value.Trim();
                        break;
                    case "AttachmentFileName":
                        result.AttachentFileName = field.Value.Trim();
                        break;
                    case "AttachmentType":
                        result.AttachmentType = field.Value.Trim();
                        break;

                    default:
                        break;
                }
            }

            return result;
        }
    }
}
