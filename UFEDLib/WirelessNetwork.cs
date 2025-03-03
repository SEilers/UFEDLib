using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public string ArtifactFamily { get; set; }
        public string BSSId { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime LastAutoConnection { get; set; }
        public DateTime LastConnection { get; set; }
        public string NWConnectionType { get; set; }
        public string Package { get; set; }
        public string Password { get; set; }
        public string SecurityMode { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public string SSId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        public Coordinate Position { get; set; }
        #endregion

        #region Parsers
        public static WirelessNetwork ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<WirelessNetwork>(element, debugAttributes);
        }
        public static List<WirelessNetwork> ParseMultiModel(XElement wirelessNetworksElement, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<WirelessNetwork>(wirelessNetworksElement, debugAttributes);
        }
        #endregion

        public static void ParseFields(IEnumerable<XElement> fieldElements, WirelessNetwork result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "ArtifactFamily":
                        result.ArtifactFamily = field.Value.Trim();
                        break;

                    case "BSSId":
                        result.BSSId = field.Value.Trim();
                        break;

                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "LastAutoConnection":
                        if (field.Value.Trim() != "")
                            result.LastAutoConnection = DateTime.Parse(field.Value.Trim());
                        break;

                    case "LastConnection":
                        if (field.Value.Trim() != "")
                            result.LastConnection = DateTime.Parse(field.Value.Trim());
                        break;

                    case "NWConnectionType":
                        result.NWConnectionType = field.Value.Trim();
                        break;

                    case "Package":
                        result.Package = field.Value.Trim();
                        break;

                    case "Password":
                        result.Password = field.Value.Trim();
                        break;

                    case "SecurityMode":
                        result.SecurityMode = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "SSId":
                        result.SSId = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("WirelessNetwork Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, WirelessNetwork result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    case "Position":
                        result.Position = Coordinate.ParseModel(modelField);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("WirelessNetwork Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, WirelessNetwork result, bool debugAttributes = false)
        {
            IUfedModelParser<WirelessNetwork>.CheckMultiFields<WirelessNetwork>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, WirelessNetwork result, bool debugAttributes = false)
        {
            IUfedModelParser<WirelessNetwork>.CheckMultiModelFields<WirelessNetwork>(multiModelFieldElements, debugAttributes);
        }
    }
}
