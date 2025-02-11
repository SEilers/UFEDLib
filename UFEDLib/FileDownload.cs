﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class FileDownload : ModelBase, IUfedModelParser<FileDownload>
    {
        public static string GetXmlModelType()
        {
            return "FileDownload";
        }

        #region fields
        public long BytesReceived { get; set; }
        public string DownloadState { get; set; }
        public DateTime EndTime { get; set; }
        public long FileSize { get; set; }
        public string Source { get; set; }
        public DateTime StartTime { get; set; }
        public string TargetPath { get; set; }
        public string Url { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region parsers
        public static FileDownload ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            FileDownload result = new FileDownload();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var modelFieldElements = element.Elements(xNamespace + "modelField");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            ParseFields(fieldElements, result, debugAttributes);
            ParseModelFields(modelFieldElements, result, debugAttributes);
            ParseMultiFields(multiFieldElements, result, debugAttributes);
            ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);

            return result;
        }

        public static List<FileDownload> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<FileDownload> result = new List<FileDownload>();

            IEnumerable<XElement> FileDownloadElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "FileDownload");

            foreach (XElement FileDownloadElement in FileDownloadElements)
            {
                FileDownload em = ParseModel(FileDownloadElement, debugAttributes);
                result.Add(em);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, FileDownload result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
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

                    case "FileSize":
                        if (long.TryParse(field.Value.Trim(), out long fileSize))
                        {
                            result.FileSize = fileSize;
                        }
                        break;

                    case "EndTime":
                        if (field.Value.Trim() != "")
                            result.EndTime = DateTime.Parse(field.Value.Trim());
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
                            Logger.LogAttribute("FileDownload Parser: Unknown field: " + field.Attribute("name").Value);
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
            IUfedModelParser<FileDownload>.CheckMultiFields<FileDownload>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, FileDownload result, bool debugAttributes = false)
        {
            IUfedModelParser<FileDownload>.CheckMultiModelFields<FileDownload>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}