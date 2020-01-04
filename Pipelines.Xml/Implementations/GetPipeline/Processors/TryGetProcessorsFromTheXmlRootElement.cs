using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Pipelines.Xml.Implementations.GetProcessor;

namespace Pipelines.Xml.Implementations.GetPipeline.Processors
{
    public class TryGetProcessorsFromTheXmlRootElement : GetPipelineFromXmlBaseProcessor
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

            var processors = args.GetPropertyValueOrDefault(
                    GetPipelineProperties.Processors,
                    Enumerable.Empty<IProcessor>()
                ).ToList();

            processors.AddRange(list);
            args.SetOrAddProperty(GetPipelineProperties.Processors, processors);
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
