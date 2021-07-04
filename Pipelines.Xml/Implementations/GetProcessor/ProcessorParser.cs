using System.Xml.Linq;
using Pipelines.ExtensionMethods;
using Pipelines.Implementations.Pipelines;

namespace Pipelines.Xml.Implementations.GetProcessor
{
    public class ProcessorParser : PipelineExecutor
    {
        public ProcessorParser() : base(
            new NamespaceBasedPipeline("Pipelines.Xml.Implementations.GetProcessor.Processors").CacheInMemory())
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