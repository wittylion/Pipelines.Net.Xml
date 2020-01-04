using Pipelines.Implementations.Pipelines;
using Pipelines.Implementations.Processors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pipelines.Xml.Implementations.GetPipeline.Processors
{
    public class CreatePipeline : ExecuteActionForPropertyProcessorConcept<QueryContext<IPipeline>, List<IProcessor>>
    {
        public override string GetPropertyName(QueryContext<IPipeline> args)
        {
            return GetPipelineProperties.Processors;
        }

        public override Task PropertyExecution(QueryContext<IPipeline> args, List<IProcessor> property)
        {
            var result = PredefinedPipeline.FromProcessors(property);
            args.SetResultWithInformation(result, "Pipeline is created.");
            return Done;
        }
    }
}
