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
        public string Account { get; set; } = "";
        public DateTime ScheduledDepartureTime { get; set; } = DateTime.MinValue;
        #endregion

        #region models
        public StreetAddress ArrivalAddress { get; set; } = new StreetAddress();
        public StreetAddress DepartureAddress { get; set; } = new StreetAddress();
        #endregion

        #region multiModels
        public List<Party> Passengers { get; set; } = new List<Party>();
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
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
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
                            Logger.LogAttribute("PublicTransportationTicket Parser: Unknown field: " + fieldName);
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
                XElement? modelElement = modelField.Element(ns + "model");
                if (modelElement == null) continue;

                var modelFieldName = ModelBase.GetAttributeValueOrLog(modelField, "name", debugAttributes);
                if (modelFieldName == null) continue;

                switch (modelFieldName)
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
                            Logger.LogAttribute("PublicTransportationTicket Parser: Unknown modelField: " + modelFieldName);
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
                var multiModelFieldName = ModelBase.GetAttributeValueOrLog(multiModelField, "name", debugAttributes);
                if (multiModelFieldName == null) continue;

                switch (multiModelFieldName)
                {
                    case "Passengers":
                        result.Passengers = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("PublicTransportationTicket Parser: Unknown multiModelAttribute: " + multiModelFieldName);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}