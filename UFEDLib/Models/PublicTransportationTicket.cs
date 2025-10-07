using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class PublicTransportationTicket : ModelBase, IUfedModelParser<PublicTransportationTicket>
    {
        public static string GetXmlModelType()
        {
            return "PublicTransportationTicket";
        }

        #region fields
        public string Account { get; set; }
        public DateTime ScheduledDepartureTime { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        public StreetAddress ArrivalAddress { get; set; }
        public StreetAddress DepartureAddress { get; set; }
        #endregion

        #region multiModels
        public List<Party> Passengers { get; set; }
        #endregion


        #region parsers
        public static PublicTransportationTicket ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<PublicTransportationTicket>(element, debugAttributes);
        }

        public static List<PublicTransportationTicket> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<PublicTransportationTicket>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, PublicTransportationTicket result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "ScheduledDepartureTime":
                        if (field.Value.Trim() != "")
                            result.ScheduledDepartureTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("PublicTransportationTicket Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, PublicTransportationTicket result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                XNamespace ns = modelField.Name.Namespace;
                XElement modelElement = modelField.Element(ns + "model");

                switch (modelField.Attribute("name").Value)
                {
                    case "ArrivalAddress":
                        result.ArrivalAddress = StreetAddress.ParseModel(modelElement, debugAttributes);
                        break;

                    case "DepartureAddress":
                        result.DepartureAddress = StreetAddress.ParseModel(modelElement, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("PublicTransportationTicket Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, PublicTransportationTicket result, bool debugAttributes = false)
        {
            IUfedModelParser<PublicTransportationTicket>.CheckMultiFields<PublicTransportationTicket>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, PublicTransportationTicket result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "Passengers":
                        result.Passengers = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("PublicTransportationTicket Parser: Unknown multiModelAttribute: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}