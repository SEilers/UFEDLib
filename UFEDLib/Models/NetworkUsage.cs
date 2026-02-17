using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class NetworkUsage : ModelBase, IUfedModelParser<NetworkUsage>
    {
        public static string GetXmlModelType()
        {
            return "NetworkUsage";
        }

        #region fields
        public string ArtifactFamily { get; set; } = "";
        public DateTime DateEnded { get; set; } = DateTime.MinValue;
        public DateTime DateStarted { get; set; } = DateTime.MinValue;
        public string IsRoaming { get; set; } = "";
        public string NetworkConnectionType { get; set; } = "";
        public long NumberOfBytesReceived { get; set; } = 0;
        public long NumberOfBytesSent { get; set; } = 0;
        public string SSId { get; set; } = "";
        public string UsageMode { get; set; } = "";
        #endregion

        #region multiFields
        public List<string> ApplicationId { get; set; } = new List<string>();
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
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
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
                            Logger.LogAttribute("NetworkUsage Parser: Unknown field: " + fieldName);
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
                var multiFieldName = ModelBase.GetAttributeValueOrLog(multiField, "name", debugAttributes);
                if (multiFieldName == null) continue;

                switch (multiFieldName)
                {
                    case "ApplicationId":
                        result.ApplicationId = multiField.Elements().Select(x => x.Value).ToList();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("NetworkUsage Parser: Unknown multiField: " + multiFieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, NetworkUsage result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                var multiModelFieldName = ModelBase.GetAttributeValueOrLog(multiModelField, "name", debugAttributes);
                if (multiModelFieldName == null) continue;

                switch (multiModelFieldName)
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
                            Logger.LogAttribute("NetworkUsage Parser: Unknown multiModelField: " + multiModelFieldName);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}