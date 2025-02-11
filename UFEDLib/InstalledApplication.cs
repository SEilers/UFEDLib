﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class InstalledApplication : ModelBase, IUfedModelParser<InstalledApplication>
    {
        public static string GetXmlModelType()
        {
            return "InstalledApplication";
        }

        #region fields
        public string UserMapping { get; set; }
        public string Source { get; set; }
        public string AppGUID { get; set; }
        public string Copyright { get; set; }
        public DateTime LastLaunched { get; set; }
        public string DecodingStatus { get; set; }
        public DateTime DeletedDate { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public string IsEmulatable { get; set; }
        public string OperationMode { get; set; }
        public string Name { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Version { get; set; }
        #endregion

        #region Parsers

        public static InstalledApplication ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            InstalledApplication result = new InstalledApplication();
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

        public static List<InstalledApplication> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<InstalledApplication> result = new List<InstalledApplication>();

            IEnumerable<XElement> iAElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "InstalledApplication");

            foreach (var iAElement in iAElements)
            {
                try
                {
                    result.Add(ParseModel(iAElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing InstalledApplication: " + ex.Message);
                }
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, InstalledApplication result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "AppGUID":
                        result.AppGUID = field.Value.Trim();
                        break;

                    case "Copyright":
                        result.Copyright = field.Value.Trim();
                        break;

                    case "DecodingStatus":
                        result.DecodingStatus = field.Value.Trim();
                        break;

                    case "Identifier":
                        result.Identifier = field.Value.Trim();
                        break;

                    case "Description":
                        result.Description = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "OperationMode":
                        result.OperationMode = field.Value.Trim();
                        break;

                    case "IsEmulatable":
                        result.IsEmulatable = field.Value.Trim();
                        break;

                    case "Version":
                        result.Version = field.Value.Trim();
                        break;

                    case "DeletedDate":
                        if (field.Value.Trim() != "")
                            result.DeletedDate = DateTime.Parse(field.Value.Trim());
                        break;

                    case "LastLaunched":
                        if (field.Value.Trim() != "")
                            result.LastLaunched = DateTime.Parse(field.Value.Trim());
                        break;

                    case "PurchaseDate":
                        if (field.Value.Trim() != "")
                            result.PurchaseDate = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("InstalledApplication Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, InstalledApplication result, bool debugAttributes = false)
        {
            IUfedModelParser<InstalledApplication>.CheckModelFields<InstalledApplication>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, InstalledApplication result, bool debugAttributes = false)
        {
            IUfedModelParser<InstalledApplication>.CheckMultiFields<InstalledApplication>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, InstalledApplication result, bool debugAttributes = false)
        {
            IUfedModelParser<InstalledApplication>.CheckMultiModelFields<InstalledApplication>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
