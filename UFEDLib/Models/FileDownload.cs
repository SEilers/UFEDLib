using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class FileDownload : ModelBase, IUfedModelParser<FileDownload>
    {
        public static string GetXmlModelType()
        {
            return "FileDownload";
        }

        #region fields
        public long BytesReceived { get; set; } = 0;
        public string DownloadState { get; set; } = "";
        public DateTime EndTime { get; set; } = DateTime.MinValue;
        public long FileSize { get; set; } = 0;
        public DateTime LastAccessed { get; set; } = DateTime.MinValue;
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public string TargetPath { get; set; } = "";
        public string Url { get; set; } = "";
        #endregion

        #region multiFields
        public List<string>? DownloadURLChains { get; set; }
        #endregion

        #region multiModels
        public Dictionary<string, string> AdditionalInfo { get; set; } = new Dictionary<string, string>();
        #endregion

        #region parsers
        public static FileDownload ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<FileDownload>(element, debugAttributes);
        }

        public static List<FileDownload> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
           return DefaultMultiModelParser<FileDownload>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, FileDownload result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "BytesReceived":
                        if (long.TryParse(field.Value.Trim(), out long bytesReceived))
                        {
                            result.BytesReceived = bytesReceived;
                        }
                        break;

                    case "DownloadState":
                        result.DownloadState = field.Value.Trim();
                        break;

                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "FileSize":
                        if (long.TryParse(field.Value.Trim(), out long fileSize))
                        {
                            result.FileSize = fileSize;
                        }
                        break;

                    case "LastAccessed":
                        if (field.Value.Trim() != "")
                            result.LastAccessed = DateTime.Parse(field.Value.Trim());
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "StartTime":
                        if (field.Value.Trim() != "")
                            result.StartTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "TargetPath":
                        result.TargetPath = field.Value.Trim();
                        break;

                    case "Url":
                        result.Url = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("FileDownload Parser: Unknown field: " + fieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, FileDownload result, bool debugAttributes = false)
        {
            IUfedModelParser<FileDownload>.CheckModelFields<FileDownload>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, FileDownload result, bool debugAttributes = false)
        {
            foreach (var field in multiFieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "DownloadURLChains":
                        result.DownloadURLChains = field.Elements().Select(x => x.Value.Trim()).ToList();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("FileDownload Parser: Unknown multiField: " + fieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, FileDownload result, bool debugAttributes = false)
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
                            Logger.LogAttribute("FileDownload Parser: Unknown multiModelField: " + multiModelFieldName);
                        }
                        break;
                }
            }

        }
        #endregion
    }
}