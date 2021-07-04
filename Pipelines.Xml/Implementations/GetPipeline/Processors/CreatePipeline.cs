using Pipelines.Implementations.Pipelines;
using Pipelines.Implementations.Processors;
using System.Collections.Generic;

namespace Pipelines.Xml.Implementations.GetPipeline.Processors
{
    [ProcessorOrder(100)]
    public class CreatePipeline : ExplanatoryProcessor
    {
        [ExecuteMethod]
        public void PropertyExecution(QueryContext<IPipeline> args, List<IProcessor> processors)
        {
            var result = PredefinedPipeline.FromProcessors(processors);
            args.SetResultWithInformation(result, "Pipeline is created.");
        }
    }
}
