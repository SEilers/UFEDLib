using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public interface IUfedModelParser<T>
    {
        public abstract static T ParseModel(XElement element, bool debugAttributes = false);

        public abstract static List<T> ParseMultiModel(XElement element, bool debugAttributes = false);

        public abstract static string GetXmlModelType();

        public abstract static void ParseFields(IEnumerable<XElement> fieldElements, T result, bool debugAttributes = false);

        public abstract static void ParseModelFields(IEnumerable<XElement> modelFieldElements, T result, bool debugAttributes = false);

        public abstract static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, T result, bool debugAttributes = false);

        public abstract static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, T result, bool debugAttributes = false);

        static void CheckFields<TModel>(IEnumerable<XElement> fieldElements, bool debugAttributes = false) where TModel : IUfedModelParser<T>, new()
        {
            if (!debugAttributes) { return; }

            string modeltype = TModel.GetXmlModelType();

            foreach (var fieldElement in fieldElements)
            {
                Logger.LogAttribute($"{modeltype} Parser: Unknown field: {fieldElement.Attribute("name")?.Value}");
            }
        }

        static void CheckModelFields<TModel>(IEnumerable<XElement> modelFieldElements, bool debugAttributes = false) where TModel : IUfedModelParser<T>, new()
        {
            if (!debugAttributes) { return; }

            string modeltype = TModel.GetXmlModelType();

            foreach (var modelFieldElement in modelFieldElements)
            {
                Logger.LogAttribute($"{modeltype} Parser: Unknown modelField: {modelFieldElement.Attribute("name")?.Value}");
            }
        }

        static void CheckMultiFields<TModel>(IEnumerable<XElement> multiFieldElements, bool debugAttributes = false) where TModel : IUfedModelParser<T>, new()
        {
            if (!debugAttributes) { return; }

            string modeltype = TModel.GetXmlModelType();

            foreach (var multiFieldElement in multiFieldElements)
            {
                Logger.LogAttribute($"{modeltype} Parser: Unknown multiField: {multiFieldElement.Attribute("name")?.Value}");
            }
        }

        static void CheckMultiModelFields<TModel>(IEnumerable<XElement> multiModelFieldElements, bool debugAttributes = false) where TModel : IUfedModelParser<T>, new()
        {
            if (!debugAttributes) { return; }

            string modeltype = TModel.GetXmlModelType();

            foreach (var multiModelFieldElement in multiModelFieldElements)
            {
                Logger.LogAttribute($"{modeltype} Parser: Unknown multiModelField: {multiModelFieldElement.Attribute("name")?.Value}");
            }
        }
    }
}
