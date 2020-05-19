using Pipelines.Implementations.Processors;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pipelines.Xml.Implementations.GetPipeline.Processors
{
    [ProcessorOrder(20)]
    public class CheckXmlElementExistence : GetPipelineFromXmlBaseProcessor
    {
        public override Task SafeExecute(QueryContext<IPipeline> args)
        {
            var xmlElement = args.GetPropertyValueOrNull<XElement>(GetPipelineProperties.XElement);
            if (xmlElement == null)
            {
                args.AbortPipelineWithErrorAndNoResult("Xml element is null.");
                return Done;
            }

            var tagName = args.GetPropertyValueOrNull<string>(GetPipelineProperties.ProcessorTagName);
            if (!xmlElement.HasElements || !xmlElement.Elements(tagName).Any())
            {
                args.AbortPipelineWithErrorAndNoResult("Xml element has no children. Cannot create pipeline.");
                return Done;
            }

            return Done;
        }

        protected override bool CustomSafeCondition(QueryContext<IPipeline> args)
        {
            return true;
        }
    }
}