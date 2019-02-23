using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Pipelines.Implementations.Pipelines;
using Pipelines.Xml.Implementations.GetProcessor;

namespace Pipelines.Xml.Implementations.GetPipeline.Processors
{
    public class TryGetPipelineFromTheRootElement : GetPipelineFromXmlBaseProcessor
    {
        public override Task SafeExecute(GetPipelineContext args)
        {
            var processorsElements = args.XElement.Elements(XName.Get("processor"));
            var processorParser = new ProcessorParser();
            var list = new LinkedList<IProcessor>();

            foreach (var processorElement in processorsElements)
            {
                var processor = processorParser.GetProcessor(processorElement);
                list.AddLast(processor);
            }

            var result = PredefinedPipeline.FromProcessors(list);
            args.SetResultWithInformation(result, "Pipeline is created.");

            return Done;
        }

        protected override bool CustomSafeCondition(GetPipelineContext args)
        {
            return args.XElement != null && 
                   !string.IsNullOrWhiteSpace(args.ProcessorTagName) &&
                   !string.IsNullOrWhiteSpace(args.TypeAttributeName);
        }
    }
}