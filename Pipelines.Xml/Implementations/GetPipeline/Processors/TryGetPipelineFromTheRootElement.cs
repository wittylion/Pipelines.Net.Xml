using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Pipelines.Implementations.Pipelines;
using Pipelines.Xml.Implementations.GetProcessor;

namespace Pipelines.Xml.Implementations.GetPipeline.Processors
{
    public class TryGetPipelineFromTheRootElement : GetPipelineFromXmlBaseProcessor
    {
        public override async Task SafeExecute(QueryContext<IPipeline> args)
        {
            var pipelineRoot = args.GetPropertyValueOrNull<XElement>(GetPipelineProperties.XElement);
            var tagName = args.GetPropertyValueOrNull<string>(GetPipelineProperties.ProcessorTagName);
            var typeAttribute = args.GetPropertyValueOrNull<string>(GetPipelineProperties.TypeAttributeName);

            var processorsElements = pipelineRoot.Elements(tagName);
            var processorParser = new ProcessorParser();
            var list = new LinkedList<IProcessor>();

            foreach (var processorElement in processorsElements)
            {
                QueryContext<IProcessor> getProcessorContext = new GetProcessorContext
                {
                    XElement = processorElement,
                    ProcessorAttribute = typeAttribute
                };
                var processor = await processorParser.Execute(getProcessorContext);

                args.AddMessageObjects(getProcessorContext.GetAllMessages());

                if (processor == null) continue;

                list.AddLast(processor);
            }

            var result = PredefinedPipeline.FromProcessors(list);
            args.SetResultWithInformation(result, "Pipeline is created.");
        }

        protected override bool CustomSafeCondition(QueryContext<IPipeline> args)
        {
            var pipelineRoot = args.GetPropertyValueOrNull<XElement>(GetPipelineProperties.XElement);
            var tagName = args.GetPropertyValueOrNull<string>(GetPipelineProperties.ProcessorTagName);
            var typeAttribute = args.GetPropertyValueOrNull<string>(GetPipelineProperties.TypeAttributeName);

            return pipelineRoot != null && !string.IsNullOrWhiteSpace(tagName) && !string.IsNullOrWhiteSpace(typeAttribute);
        }
    }
}
