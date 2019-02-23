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

            pipeline.GetProcessors().Should().ContainSingle()
                .And.Subject.First().Should().BeOfType<TestEmptyProcessor>();
        }
    }
}
