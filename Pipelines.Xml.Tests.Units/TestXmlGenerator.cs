using Pipelines.Xml.Tests.Units.Data;
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

        public static string GetPipelineStringXmlWithSingleEmptyProcessor()
        {
            return @"
                <testPipeline>
                  <processor type=""Pipelines.Xml.Tests.Units.Data.EmptyTestProcessor, Pipelines.Xml.Tests.Units"" />
                </testPipeline>
                ";
        }

        public static string GetPipelineStringXmlWithTwoEmptyProcessor()
        {
            return @"
                <testPipeline>
                  <processor type=""Pipelines.Xml.Tests.Units.Data.EmptyTestProcessor, Pipelines.Xml.Tests.Units"" />
                  <processor type=""Pipelines.Xml.Tests.Units.Data.EmptyTestProcessor, Pipelines.Xml.Tests.Units"" />
                </testPipeline>
                ";
        }

        public static string GetPipelineStringXmlWithProcessorHavingStringArgument()
        {
            return @"
                <testPipeline>
                  <processor type=""Pipelines.Xml.Tests.Units.Data.StringArgumentTestProcessor, Pipelines.Xml.Tests.Units"">
                    <parameter>test</parameter>
                  </processor>
                </testPipeline>
                ";
        }

        public static string GetPipelineStringXmlWithTwoProcessorHavingStringArgumentAndNoArguments()
        {
            return @"
                <testPipeline>
                  <processor type=""Pipelines.Xml.Tests.Units.Data.StringArgumentTestProcessor, Pipelines.Xml.Tests.Units"">
                    <parameter>test</parameter>
                  </processor>
                  <processor type=""Pipelines.Xml.Tests.Units.Data.StringArgumentTestProcessor, Pipelines.Xml.Tests.Units"" />
                </testPipeline>
                ";
        }
    }
}