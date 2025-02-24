 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class ModelBase
    {
        public string id { get; set; }

        public string type { get; set; }

        public string deleted_state { get; set; }

        public string decoding_confidence { get; set; }

        public string isrelated { get; set; }

        public string source_index { get; set; }

        public string extractionId { get; set; }

        public void ParseAttributes(XElement element)
        {
            if(element == null)
            {
                return;
            }

            try
            {
                this.id = element.Attribute("id")?.Value;
                this.type = element.Attribute("type")?.Value;
                this.deleted_state = element.Attribute("deleted_state")?.Value;
                this.decoding_confidence = element.Attribute("decoding_confidence")?.Value;
                this.isrelated = element.Attribute("isrelated")?.Value;
                this.source_index = element.Attribute("source_index")?.Value;
                this.extractionId = element.Attribute("extractionId")?.Value;
            }
            catch (Exception ex)
            {
                Logger.LogError("ModelBase: Error parsing xml reader attributes " + ex.Message);
            }
        }

        public static T DefaultModelParser<T>(XElement element, bool debugAttributes = false) where T : ModelBase, IUfedModelParser<T>, new()
        {
            if(element == null)
            {
                Logger.LogWarning($"{T.GetXmlModelType()}: element is null");
                return null;
            }

            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            T result = new T();
    
            try
            {
                result.ParseAttributes(element);

                var fieldElements = element.Elements(xNamespace + "field");
                var modelFieldElements = element.Elements(xNamespace + "modelField");
                var multiFieldElements = element.Elements(xNamespace + "multiField");
                var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

                T.ParseFields(fieldElements, result, debugAttributes);
                T.ParseModelFields(modelFieldElements, result, debugAttributes);
                T.ParseMultiFields(multiFieldElements, result, debugAttributes);
                T.ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);
            }
            catch (Exception ex)
            {
                Logger.LogError($"{T.GetXmlModelType()}: Error parsing xml reader attributes {ex.Message}");
            }           

            return result;
        }

        public static List<T> DefaultMultiModelParser<T>(XElement element, bool debugAttributes = false) where T : ModelBase, IUfedModelParser<T>, new()
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<T> result = new List<T>();

            IEnumerable<XElement> modelElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == T.GetXmlModelType());

            try
            {
                foreach (var modelElement in modelElements)
                {
                    T model = DefaultModelParser<T>(modelElement, debugAttributes);
                    result.Add(model);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"{T.GetXmlModelType()}: Error parsing xml reader attributes {ex.Message}");
            }
            return result;
        }
    }
}
