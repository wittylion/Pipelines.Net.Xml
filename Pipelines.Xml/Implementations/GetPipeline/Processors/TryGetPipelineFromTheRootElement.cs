using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Pipelines.Implementations.Pipelines;
using Pipelines.Xml.Implementations.GetProcessor;

namespace Pipelines.Xml.Implementations.GetPipeline.Processors
{
    public class TryGetPipelineFromTheRootElement : GetPipelineFromXmlBaseProcessor
    {
        public override async Task SafeExecute(GetPipelineContext args)
        {
            var processorsElements = args.XElement.Elements(args.ProcessorTagName);
            var processorParser = new ProcessorParser();
            var list = new LinkedList<IProcessor>();

            foreach (var processorElement in processorsElements)
            {
                QueryContext<IProcessor> getProcessorContext = new GetProcessorContext
                {
                    XElement = processorElement,
                    ProcessorAttribute = args.TypeAttributeName
                };
                var processor = await processorParser.Execute(getProcessorContext);

                if (processor == null) continue;
                
                list.AddLast(processor);
            }

            var result = PredefinedPipeline.FromProcessors(list);
            args.SetResultWithInformation(result, "Pipeline is created.");
        }

        protected override bool CustomSafeCondition(GetPipelineContext args)
        {
            return args.XElement != null && 
                   !string.IsNullOrWhiteSpace(args.ProcessorTagName) &&
                   !string.IsNullOrWhiteSpace(args.TypeAttributeName);
        }
    }
}
