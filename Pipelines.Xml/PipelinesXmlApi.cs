using System.Xml.Linq;
using Pipelines.Implementations.Pipelines;
using Pipelines.Xml.Implementations.GetPipeline;

namespace Pipelines.Xml
{
    public class PipelinesXmlApi
    {
        public static PipelineParser Parser = new PipelineParser();

        public static IPipeline GetPipelineFromXmlOrNull(XElement xmlElement)
        {
            return GetPipelineFromXml(xmlElement, null);
        }

        public static IPipeline GetPipelineFromXmlOrEmpty(XElement xmlElement)
        {
            return GetPipelineFromXml(xmlElement, PredefinedPipeline.Empty);
        }

        public static IPipeline GetPipelineFromXml(XElement xmlElement, IPipeline defaultValue)
        {
            if (xmlElement == null || !xmlElement.HasElements)
            {
                return defaultValue;
            }

            return Parser.GetPipeline(xmlElement) ?? defaultValue;
        }
    }
}
