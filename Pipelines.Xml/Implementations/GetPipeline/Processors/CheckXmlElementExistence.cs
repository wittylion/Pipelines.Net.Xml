using System.Threading.Tasks;

namespace Pipelines.Xml.Implementations.GetPipeline.Processors
{
    public class CheckXmlElementExistence : GetPipelineFromXmlBaseProcessor
    {
        public override Task SafeExecute(GetPipelineContext args)
        {
            if (args.XElement == null)
            {
                args.AbortPipelineWithErrorAndNoResult("Xml element is null.");
            }

            return Done;
        }

        protected override bool CustomSafeCondition(GetPipelineContext args)
        {
            return true;
        }
    }
}