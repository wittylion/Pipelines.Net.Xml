using System.Linq;
using System.Threading.Tasks;

namespace Pipelines.Xml.Implementations.GetPipeline.Processors
{
    public class CheckXmlElementExistence : GetPipelineFromXmlBaseProcessor
    {
        public override Task SafeExecute(GetPipelineContext args)
        {
            var xmlElement = args.XElement;
            if (xmlElement == null)
            {
                args.AbortPipelineWithErrorAndNoResult("Xml element is null.");
                return Done;
            }

            if (!xmlElement.HasElements || !xmlElement.Elements(args.ProcessorTagName).Any())
            {
                args.AbortPipelineWithErrorAndNoResult("Xml element has no children. Can not create empty pipeline.");
                return Done;
            }

            return Done;
        }

        protected override bool CustomSafeCondition(GetPipelineContext args)
        {
            return true;
        }
    }
}