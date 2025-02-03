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
        public string UserMapping { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Type { get; set; }
        public string MCC { get; set; }
        public string MNC { get; set; }
        public string LAC { get; set; }
        public string CID { get; set; }
        public string NID { get; set; }
        public string BID { get; set; }
        public string SID { get; set; }
        #endregion 

        #region Parsers
        public static CellTower ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            CellTower result = new CellTower();

            var fieldElements = element.Elements(xNamespace + "field");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Type":
                        result.Type = field.Value.Trim();
                        break;

                    case "MCC":
                        result.MCC = field.Value.Trim();
                        break;

                    case "MNC":
                        result.MNC = field.Value.Trim();
                        break;

                    case "LAC":
                        result.LAC = field.Value.Trim();
                        break;

                    case "CID":
                        result.CID = field.Value.Trim();
                        break;

                    case "NID":
                        result.NID = field.Value.Trim();
                        break;

                    case "BID":
                        result.BID = field.Value.Trim();
                        break;

                    case "SID":
                        result.SID = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("CellTower Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("CellTower Parser: Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("CellTower Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }

        public static List<CellTower> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<CellTower> result = new List<CellTower>();

            IEnumerable<XElement> cellTowers = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "CellTower");

            foreach (XElement cellTower in cellTowers)
            {
                CellTower ct = ParseModel(cellTower, debugAttributes);
                result.Add(ct);
            }

            return result;
        }

        #endregion
    }
}
