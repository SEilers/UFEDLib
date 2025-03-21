﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class VisitedPage : ModelBase, IUfedModelParser<VisitedPage>
    {
        public static string GetXmlModelType()
        {
            return "VisitedPage";
        }

        #region fields
        public string Account { get; set; }
        public string ArtifactFamily { get; set; }
        public string CanRebuildCacheFile { get; set; }
        public DateTime LastVisited { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string UrlCacheFile { get; set; }
        public string UserMapping { get; set; }
        public int VisitCount { get; set; }
        #endregion

        #region Parsers
        public static VisitedPage ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<VisitedPage>(element, debugAttributes);
        }

        public static List<VisitedPage> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<VisitedPage>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, VisitedPage result, bool debugAttributes = false)
        {
            foreach (XElement field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "ArtifactFamily":
                        result.ArtifactFamily = field.Value.Trim();
                        break;

                    case "CanRebuildCacheFile":
                        result.CanRebuildCacheFile = field.Value.Trim();
                        break;

                    case "LastVisited":
                        if (field.Value.Trim() != "")
                            result.LastVisited = DateTime.Parse(field.Value.Trim());
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Title":
                        result.Title = field.Value.Trim();
                        break;

                    case "Url":
                        result.Url = field.Value.Trim();
                        break;

                    case "UrlCacheFile":
                        result.UrlCacheFile = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "VisitCount":
                        if (field.Value.Trim() != "")
                            if (int.TryParse(field.Value.Trim(), out int visitCount))
                                result.VisitCount = visitCount;
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("VisitedPage Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, VisitedPage result, bool debugAttributes = false)
        {
            IUfedModelParser<VisitedPage>.CheckModelFields<VisitedPage>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, VisitedPage result, bool debugAttributes = false)
        {
            IUfedModelParser<VisitedPage>.CheckMultiFields<VisitedPage>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, VisitedPage result, bool debugAttributes = false)
        {
            IUfedModelParser<VisitedPage>.CheckMultiModelFields<VisitedPage>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
