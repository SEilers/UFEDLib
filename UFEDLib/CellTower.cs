using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class CellTower : ModelBase, IUfedModelParser<CellTower>
    {
        public static string GetXmlModelType()
        {
            return "CellTower";
        }

        #region fields
        public string BID { get; set; }
        public string CID { get; set; }
        public string LAC { get; set; }
        public string MCC { get; set; }
        public string MNC { get; set; }
        public string NID { get; set; }
        public string Package { get; set; }
        public string SID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Type { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        public Coordinate Position { get; set; }
        #endregion

        #region Parsers
        public static CellTower ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<CellTower>(element, debugAttributes);
        }

        public static List<CellTower> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<CellTower>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, CellTower result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "BID":
                        result.BID = field.Value.Trim();
                        break;

                    case "CID":
                        result.CID = field.Value.Trim();
                        break;

                    case "LAC":
                        result.LAC = field.Value.Trim();
                        break;

                    case "MCC":
                        result.MCC = field.Value.Trim();
                        break;

                    case "MNC":
                        result.MNC = field.Value.Trim();
                        break;

                    case "NID":
                        result.NID = field.Value.Trim();
                        break;

                    case "Package":
                        result.Package = field.Value.Trim();
                        break;

                    case "SID":
                        result.SID = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Type":
                        result.Type = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("CellTower Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, CellTower result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    case "Position":
                        result.Position = Coordinate.ParseModel(modelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("CellTower Parser: Unknown field: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, CellTower result, bool debugAttributes = false)
        {
            IUfedModelParser<CellTower>.CheckMultiFields<CellTower>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, CellTower result, bool debugAttributes = false)
        {
            IUfedModelParser<CellTower>.CheckMultiModelFields<CellTower>(multiModelFieldElements, debugAttributes);
        }

        #endregion
    }
}
