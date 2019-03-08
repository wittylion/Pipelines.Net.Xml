using System.Xml.Linq;

namespace Pipelines.Xml.Tests.Units
{
    public static class TestXmlGenerator
    {
        public static XElement GetPipelineXmlWithSingleEmptyProcessor()
        {
            return new XElement("testPipeline",
                new XElement("processor",
                    new XAttribute("type", typeof(EmptyTestProcessor).AssemblyQualifiedName)
                )
            );
        }

        public static XDocument GetDocumentWithPipelinesSectionAndEmptyProcessor()
        {
            return new XDocument(
                new XElement("pipelines", GetPipelineXmlWithSingleEmptyProcessor())
            );
        }
    }
}