using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class NetworkUsage : ModelBase, IUfedModelParser<NetworkUsage>
    {
        public static string GetXmlModelType()
        {
            return "NetworkUsage";
        }

        #region fields
        public string ArtifactFamily { get; set; }
        public DateTime DateEnded { get; set; }
        public DateTime DateStarted { get; set; }
        public string IsRoaming { get; set; }
        public string NetworkConnectionType { get; set; }
        public long NumberOfBytesReceived { get; set; }
        public long NumberOfBytesSent { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public string SSId { get; set; }
        public string UsageMode { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region multiFields
        public List<String> ApplicationId { get; set; }
        #endregion


        #region multiModels
        public Dictionary<string, string> AdditionalInfo { get; set; } = new Dictionary<string, string>();
        #endregion

        #region parsers
        public static NetworkUsage ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<NetworkUsage>(element, debugAttributes);
        }

        public static List<NetworkUsage> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<NetworkUsage>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, NetworkUsage result, bool debugAttributes = false)
        {

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "ArtifactFamily":
                        result.ArtifactFamily = field.Value.Trim();
                        break;

                    case "DateEnded":
                        result.DateEnded = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DateStarted":
                        result.DateStarted = DateTime.Parse(field.Value.Trim());
                        break;

                    case "IsRoaming":
                        result.IsRoaming = field.Value.Trim();
                        break;

                    case "NetworkConnectionType":
                        result.NetworkConnectionType = field.Value.Trim();
                        break;

                    case "NumberOfBytesReceived":
                        if (long.TryParse(field.Value.Trim(), out long bytesReceived))
                        {
                            result.NumberOfBytesReceived = bytesReceived;
                        }
                        break;

                    case "NumberOfBytesSent":
                        if (long.TryParse(field.Value.Trim(), out long bytesSent))
                        {
                            result.NumberOfBytesSent = bytesSent;
                        }
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

                    case "UsageMode":
                        result.UsageMode = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("NetworkUsage Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }

        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, NetworkUsage result, bool debugAttributes = false)
        {
            IUfedModelParser<NetworkUsage>.CheckModelFields<NetworkUsage>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, NetworkUsage result, bool debugAttributes = false)
        {
            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "ApplicationId":
                        result.ApplicationId = multiField.Elements().Select(x => x.Value).ToList();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("NetworkUsage Parser: Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, NetworkUsage result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "AdditionalInfo":
                        var kvModelsAdditionalInfo = KeyValueModel.ParseMultiModel(multiModelField, debugAttributes);
                        foreach (var kvModel in kvModelsAdditionalInfo)
                        {
                            if (!string.IsNullOrEmpty(kvModel.Key) && !string.IsNullOrEmpty(kvModel.Value))
                            {
                                result.AdditionalInfo[kvModel.Key] = kvModel.Value;
                            }
                        }
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("FileUpload Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}