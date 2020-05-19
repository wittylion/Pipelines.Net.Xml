using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using Pipelines.Xml.Tests.Units.Data;
using Xunit;

namespace Pipelines.Xml.Tests.Units
{
    public class PipelinesXmlApiTests
    {
        [Fact]
        public void GetPipelineFromXmlOrEmpty_WhenGeneratingSimplePipeline_DoesNotReturnNull()
        {
            var xmlPipeline = TestXmlGenerator.GetPipelineXmlWithSingleEmptyProcessor();
            var pipeline = PipelinesXmlApi.GetPipelineFromXmlOrNull(xmlPipeline);

            pipeline.Should().NotBeNull();
        }
        
        [Fact]
        public void GetPipelineFromXmlOrEmpty_WhenGeneratingSimplePipeline_ShouldHaveProcessorOfSpecifiedType()
        {
            var xmlPipeline = TestXmlGenerator.GetPipelineXmlWithSingleEmptyProcessor();
            var pipeline = PipelinesXmlApi.GetPipelineFromXmlOrEmpty(xmlPipeline);

            pipeline.GetProcessors().Should().ContainSingle("test element contains an empty processor")
                .And.Subject.First().Should().BeOfType<EmptyTestProcessor>("this processor is specified in test data");
        }
        
        [Fact]
        public void GetPipelineFromXmlByXPath_WhenPassingAnInvalidXPath_ShouldReturnNull()
        {
            var xmlPipeline = TestXmlGenerator.GetDocumentWithPipelinesSectionAndEmptyProcessor();
            var pipeline = PipelinesXmlApi.GetPipelineFromXmlByXPath(xmlPipeline, "/not/valid/path", null);

            pipeline.Should().BeNull("because XPath has invalid value");
        }
        
        [Fact]
        public void GetPipelineFromXmlByXPath_WhenPassingValidXPath_ShouldHaveProcessor()
        {
            var xmlPipeline = TestXmlGenerator.GetDocumentWithPipelinesSectionAndEmptyProcessor();
            var pipeline = PipelinesXmlApi.GetPipelineFromXmlByXPath(xmlPipeline, "/pipelines/testPipeline", null);

            pipeline.GetProcessors().Should().NotBeEmpty("because the XPath points to a pipeline containing processor");
        }

        [Fact]
        public void GetPipelineFromXmlByXPath_WhenPassingStringXmlAndValidXPath_ShouldHaveProcessor()
        {
            var xmlPipeline = TestXmlGenerator.GetPipelineStringXmlWithSingleEmptyProcessor();
            var pipeline = PipelinesXmlApi.GetPipelineFromXmlByXPath(XDocument.Parse(xmlPipeline), "/testPipeline", null);

            pipeline.GetProcessors().Should().NotBeEmpty("because string contains valid xml with one processor");
        }

        [Fact]
        public void GetPipelineFromXmlByXPath_WhenPassingStringXmlWithTwoProcessorsAndValidXPath_ShouldHaveTwoProcessors()
        {
            var xmlPipeline = TestXmlGenerator.GetPipelineStringXmlWithTwoEmptyProcessor();
            var pipeline = PipelinesXmlApi.GetPipelineFromXmlByXPath(XDocument.Parse(xmlPipeline), "/testPipeline", null);

            pipeline.GetProcessors().Should().HaveCount(2, "because there are two processors in xml");
        }

        [Fact]
        public void GetPipelineFromXmlByXPath_WhenPassingNullXPath_ShouldReturnDefaultPipeline()
        {
            var xmlPipeline = TestXmlGenerator.GetDocumentWithPipelinesSectionAndEmptyProcessor();
            var pipeline = PipelinesXmlApi.GetPipelineFromXmlByXPath(xmlPipeline, null, null);

            pipeline.Should().BeNull("because the XPath is null");
        }

        [Fact]
        public void GetPipelineFromXmlOrNull_WhenPassingStringXmlWithProcessorHavingParameter_ShouldHaveProcessorWithParameter()
        {
            var xmlPipeline = TestXmlGenerator.GetPipelineStringXmlWithProcessorHavingStringArgument();
            var pipeline = PipelinesXmlApi.GetPipelineFromXmlOrNull(XDocument.Parse(xmlPipeline).Root);

            pipeline.GetProcessors().Should().Contain(x => x is StringArgumentTestProcessor).Which.As<StringArgumentTestProcessor>().Parameter.Should().Be("test");
        }

        [Fact]
        public void GetPipelineFromXmlOrNull_WhenPassingStringXmlWithTwoProcessorsHavingDifferentConstructors_ShouldHaveTwoExactProcessor()
        {
            var xmlPipeline = TestXmlGenerator.GetPipelineStringXmlWithTwoProcessorHavingStringArgumentAndNoArguments();
            var pipeline = PipelinesXmlApi.GetPipelineFromXmlOrNull(XDocument.Parse(xmlPipeline).Root);

            pipeline.GetProcessors().Should().AllBeAssignableTo<StringArgumentTestProcessor>();
            pipeline.GetProcessors().First().As<StringArgumentTestProcessor>().Parameter.Should().Be("test");
            pipeline.GetProcessors().Last().As<StringArgumentTestProcessor>().Parameter.Should().Be("default");
        }
    }
}
