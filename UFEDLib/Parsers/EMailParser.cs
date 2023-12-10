using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class EMailParser
    {
        public static EMail Parse(XElement emailNode)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            EMail result = new EMail();

            var fieldElements = emailNode.Elements(xNamespace + "field");
            var multiModelFieldElements = emailNode.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Folder":
                        result.Folder = field.Value.Trim();
                        break;

                    case "Status":
                        result.Status = field.Value.Trim();
                        break;

                    case "From":
                        result.From = PartyParser.Parse(field);
                        break;  

                    case "Subject":
                        result.Subject = field.Value.Trim();
                        break;

                    case "Body":
                        result.Body = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Priority":
                        result.Priority = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;
                }
            }

            foreach (var multiField in multiModelFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "To":
                        result.To = PartyParser.ParseParties(multiField);
                        break;

                    case "Cc":
                        result.Cc = PartyParser.ParseParties(multiField);
                        break;

                    case "Bcc":
                        result.Bcc = PartyParser.ParseParties(multiField);
                        break;

                    case "Attachments":
                        result.Attachments = AttachmentParser.ParseAttachments(multiField);
                        break;
                }
            }


            return result;
        }
    }
}
