using FluentAssertions;
using Pipelines.Implementations.Contexts;
using Pipelines.Xml.Implementations.GetProcessor.Processors;
using Xunit;

namespace Pipelines.Xml.Tests.Units.Implementations.GetProcessor.Processors
{
    public class TryToObtainTypeTests
    {
        [Fact]
        public async void Execute_WhenTypeAndAssemblyPassed_ShouldCreateResult()
        {
            TryToObtainType processor = new TryToObtainType();

            var context = 
                ContextConstructor.BuildQueryContext<IProcessor>()
                    .Use("ProcessorTypeString", "Pipelines.Xml.Tests.Units.Data.EmptyTestProcessor, Pipelines.Xml.Tests.Units")
                    .OriginalContext;

            await processor.Execute(context);

            context.HasProperty("ProcessorType").Should().BeTrue("library should build a type from type name and library name as a minimum requirement");
        }
    }
}
