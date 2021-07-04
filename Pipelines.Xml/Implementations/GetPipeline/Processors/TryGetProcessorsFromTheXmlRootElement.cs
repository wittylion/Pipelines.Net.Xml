using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Pipelines.Implementations.Processors;
using Pipelines.Xml.Implementations.GetProcessor;

namespace Pipelines.Xml.Implementations.GetPipeline.Processors
{
    [ProcessorOrder(60)]
    public class TryGetProcessorsFromTheXmlRootElement : ExplanatoryProcessor
    {

        [ExecuteMethod(Order = 10)]
        public IEnumerable<object> CheckXmlElement(

            [ContextParameter(AbortIfNotExist = true, ErrorMessage = "Xml element is null.")]
            XElement xElement,
            
            string processorTagName
            
            )
        {
            if (!xElement.HasElements || !xElement.Elements(processorTagName).Any())
            {
                string message = "Xml element has no children. Cannot create pipeline.";
                yield return AbortPipelineWithErrorMessage(message);
            }
        }

        [ExecuteMethod(Order = 20)]
        public object GetProcessorsElements(
        
            [ContextParameter(Name = GetPipelineProperties.XElement)]
            XElement pipelineRoot,

            [ContextParameter(Name = GetPipelineProperties.ProcessorTagName)]
            string tagName
            
            )
        {
            return new { ProcessorsElements = pipelineRoot.Elements(tagName) };
        }

        [ExecuteMethod(Order = 30)]
        public object SetProcessorParser()
        {
            return new { ProcessorParser = GetProcessorParser() };
        }

        [ExecuteMethod(Order = 40)]
        public async Task<IEnumerable> CollectProcessors(
            IEnumerable<XElement> processorsElements,
            PipelineExecutor processorParser, [ContextParameter(Name = GetPipelineProperties.TypeAttributeName)] 
            string typeAttribute)
        {
            var result = new ArrayList();
            var list = new LinkedList<IProcessor>();

            foreach (var processorElement in processorsElements)
            {
                QueryContext<IProcessor> getProcessorContext = new GetProcessorContext
                {
                    XElement = processorElement,
                    ProcessorAttribute = typeAttribute
                };
                var processor = await processorParser.Execute(getProcessorContext);

                result.Add(AddMessageObjects(getProcessorContext.GetAllMessages()));

                if (processor == null) continue;

                list.AddLast(processor);
            }

            result.Add(new { NewProcessors = list });
            return result;
        }

        [ExecuteMethod(Order = 50)]
        public void ApplyNewProcessors(QueryContext<IPipeline> args,
            IEnumerable<IProcessor> newProcessors,
            IEnumerable<IProcessor> processors)
        {
            // TODO: make a default value
            var list = processors?.ToList() ?? new List<IProcessor>();
            list.AddRange(newProcessors);
            args.SetOrAddProperty(GetPipelineProperties.Processors, list);
        }

        protected ProcessorParser ProcessorParser { get; } = new ProcessorParser();
        public virtual PipelineExecutor GetProcessorParser()
        {
            return ProcessorParser;
        }
        /*
        protected override bool CustomSafeCondition(QueryContext<IPipeline> args)
        {
            var pipelineRoot = args.GetPropertyValueOrNull<XElement>(GetPipelineProperties.XElement);
            var tagName = args.GetPropertyValueOrNull<string>(GetPipelineProperties.ProcessorTagName);
            var typeAttribute = args.GetPropertyValueOrNull<string>(GetPipelineProperties.TypeAttributeName);

            return pipelineRoot != null && !string.IsNullOrWhiteSpace(tagName) && !string.IsNullOrWhiteSpace(typeAttribute);
        }*/
    }
}
