using System.Linq;
using FluentAssertions;
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
                .And.Subject.First().Should().BeOfType<TestEmptyProcessor>("this processor is specified in test data");
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
        public void GetPipelineFromXmlByXPath_WhenPassingNullXPath_ShouldReturnDefaultPipeline()
        {
            var xmlPipeline = TestXmlGenerator.GetDocumentWithPipelinesSectionAndEmptyProcessor();
            var pipeline = PipelinesXmlApi.GetPipelineFromXmlByXPath(xmlPipeline, null, null);

            pipeline.Should().BeNull("because the XPath is null");
        }
    }
}
