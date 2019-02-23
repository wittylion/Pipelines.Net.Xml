namespace Pipelines.Xml.Implementations.GetPipeline
{
    public abstract class GetPipelineFromXmlBaseProcessor : SafeProcessor<GetPipelineContext>
    {
        public override bool SafeCondition(GetPipelineContext args)
        {
            return base.SafeCondition(args) && this.CustomSafeCondition(args);
        }

        protected abstract bool CustomSafeCondition(GetPipelineContext args);
    }
}