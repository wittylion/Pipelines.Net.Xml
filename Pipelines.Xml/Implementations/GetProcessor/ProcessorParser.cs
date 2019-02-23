using System.Xml.Linq;
using Pipelines.Implementations.Pipelines;
using Pipelines.Xml.Implementations.GetProcessor.Processors;

namespace Pipelines.Xml.Implementations.GetProcessor
{
    public class ProcessorParser : PipelineExecutor
    {
        public ProcessorParser() : base(
            PredefinedPipeline.FromProcessors<ParseProcessorType, CreateFromType>())
        {
        }

        public virtual IProcessor GetProcessor(XElement element)
        {
            return this.GetProcessor(new GetProcessorContext
            {
                XElement = element
            });
        }

        public virtual IProcessor GetProcessor(QueryContext<IProcessor> args)
        {
            return Execute(args).Result;
        }
    }
}