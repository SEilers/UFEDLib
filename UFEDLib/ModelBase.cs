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
    }
}
