using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class FileUpload : ModelBase, IUfedModelParser<FileUpload>
    {
        public static string GetXmlModelType()
        {
            return "FileUpload";
        }

        #region fields
        public string Account { get; set; }
        public DateTime DateUploaded { get; set; }
        public DateTime DateLastModified { get; set; }
        public string FileType { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        public Party Owner { get; set; }
        #endregion

        #region multiModels
        public Dictionary<string, string> AdditionalInfo { get; set; } = new Dictionary<string, string>();
        public List<Party> Participants { get; set; }
        #endregion

        #region parsers
        public static FileUpload ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            FileUpload result = new FileUpload();
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

        public static List<FileUpload> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<FileUpload> result = new List<FileUpload>();

            IEnumerable<XElement> FileUploadElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "FileUpload");

            foreach (XElement FileUploadElement in FileUploadElements)
            {
                FileUpload em = ParseModel(FileUploadElement, debugAttributes);
                result.Add(em);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, FileUpload result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "DateUploaded":
                        if( field.Value.Trim() != "")
                            result.DateUploaded = DateTime.Parse(field.Value.Trim());
                        break;

                    case "DateLastModified":
                        if (field.Value.Trim() != "")
                            result.DateLastModified = DateTime.Parse(field.Value.Trim());
                        break;

                    case "FileType":
                        result.FileType = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
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

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("FileUpload Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, FileUpload result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    case "Owner":
                        result.Owner = Party.ParseModel(modelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("FileUpload Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, FileUpload result, bool debugAttributes = false)
        {
            IUfedModelParser<FileUpload>.CheckMultiFields<FileUpload>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, FileUpload result, bool debugAttributes = false)
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

                    case "Participants":
                        result.Participants = Party.ParseMultiModel(multiModelField, debugAttributes);
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