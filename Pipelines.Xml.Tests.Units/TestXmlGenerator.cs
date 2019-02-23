using System.Xml.Linq;

namespace Pipelines.Xml.Tests.Units
{
    public static class TestXmlGenerator
    {
        public static XElement GetPipelineXmlWithSingleEmptyProcessor()
        {
            var xmlPipeline = new XElement("testPipeline");
            var xmlProcessor = new XElement("processor");
            xmlProcessor.SetAttributeValue(XName.Get("type"), typeof(TestEmptyProcessor).AssemblyQualifiedName);

            xmlPipeline.Add(xmlProcessor);

            return xmlPipeline;
        }
    }
}