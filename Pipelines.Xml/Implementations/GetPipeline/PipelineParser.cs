using System.Xml.Linq;
using Pipelines.Implementations.Pipelines;
using Pipelines.Xml.Implementations.GetPipeline.Processors;

namespace Pipelines.Xml.Implementations.GetPipeline
{
    public class PipelineParser : PipelineExecutor
    {
        public PipelineParser() : base(
            PredefinedPipeline.FromProcessors<CheckXmlElementExistence, TryGetPipelineFromTheRootElement>())
        {
        }

        public virtual IPipeline GetPipeline(XElement element)
        {
            return GetPipeline(new GetPipelineContext
            {
                XElement = element,
                ProcessorTagName = "processor",
                TypeAttributeName = "type"
            });
        }

        public virtual IPipeline GetPipeline(GetPipelineContext context)
        {
            return GetPipeline((QueryContext<IPipeline>)context);
        }

        public virtual IPipeline GetPipeline(QueryContext<IPipeline> context)
        {
            return Execute(context).Result;
        }
    }
}