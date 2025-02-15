using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class SocialMediaActivity : ModelBase, IUfedModelParser<SocialMediaActivity>
    {
        public static string GetXmlModelType()
        {
            return "SocialMediaActivity";
        }

        #region fields
        public string Account { get; set; }
        public string Body { get; set; }
        public string ChannelName { get; set; }
        public string ChannelType { get; set; }
        public int CommentCount { get; set; }
        public string IsFromActivityLog { get; set; }
        public string Platform { get; set; }
        public string PrivacySetting { get; set; }
        public int ReactionsCount { get; set; }
        public string ServiceIdentifier { get; set; }
        public int SharesCount { get; set; }
        public string SocialActivityType { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        public Party Author { get; set; }
        #endregion

        #region multiModels
        public List<Attachment> Attachments { get; set; }
        public List<Party> TaggedParties { get; set; }
        #endregion

        #region parsers
        public static SocialMediaActivity ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            SocialMediaActivity result = new SocialMediaActivity();

            try
            {
                result.ParseAttributes(element);

                var fieldElements = element.Elements(xNamespace + "field");
                var modelFieldElements = element.Elements(xNamespace + "modelField");
                var multiFieldElements = element.Elements(xNamespace + "multiField");
                var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

                ParseFields(fieldElements, result, debugAttributes);
                ParseModelFields(modelFieldElements, result, debugAttributes);
                ParseMultiFields(multiFieldElements, result, debugAttributes);
                ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);
            }
            catch (Exception ex)
            {
                Logger.LogError("SocialMediaActivity: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static List<SocialMediaActivity> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<SocialMediaActivity> result = new List<SocialMediaActivity>();

            IEnumerable<XElement> SocialMediaActivityElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "SocialMediaActivity");

            foreach (XElement SocialMediaActivityElement in SocialMediaActivityElements)
            {
                SocialMediaActivity em = ParseModel(SocialMediaActivityElement, debugAttributes);
                result.Add(em);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, SocialMediaActivity result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Body":
                        result.Body = field.Value.Trim();
                        break;

                    case "ChannelName":
                        result.ChannelName = field.Value.Trim();
                        break;

                    case "ChannelType":
                        result.ChannelType = field.Value.Trim();
                        break;

                    case "CommentCount":
                        if (int.TryParse(field.Value.Trim(), out int commentsCount))
                        {
                            result.CommentCount = commentsCount;
                        }
                        break;

                    case "IsFromActivityLog":
                        result.IsFromActivityLog = field.Value.Trim();
                        break;

                    case "Platform":
                        result.Platform = field.Value.Trim();
                        break;

                    case "PrivacySetting":
                        result.PrivacySetting = field.Value.Trim();
                        break;

                    case "ReactionsCount":
                        if (int.TryParse(field.Value.Trim(), out int reactionsCount))
                        {
                            result.ReactionsCount = reactionsCount;
                        }
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "SharesCount":
                        if (int.TryParse(field.Value.Trim(), out int sharesCount))
                        {
                            result.SharesCount = sharesCount;
                        }
                        break;

                    case "SocialActivityType":
                        result.SocialActivityType = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Status":
                        result.Status = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
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
                            Logger.LogAttribute("SocialMediaActivity Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }

        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, SocialMediaActivity result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    case "Author":
                        result.Author = Party.ParseModel(modelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("SocialMediaActivity Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, SocialMediaActivity result, bool debugAttributes = false)
        {
            IUfedModelParser<SocialMediaActivity>.CheckMultiFields<SocialMediaActivity>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, SocialMediaActivity result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "Attachments":
                        result.Attachments = Attachment.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "TaggedParties":
                        result.TaggedParties = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("SocialMediaActivity Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}