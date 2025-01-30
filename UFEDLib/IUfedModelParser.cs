using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    internal interface IUfedModelParser<T>
    {
        public abstract static T ParseModel(XElement element, bool debugAttributes = false);

        public abstract static List<T> ParseMultiModel (XElement element, bool debugAttributes = false);

        public abstract static string GetXmlModelType();
    }
}
