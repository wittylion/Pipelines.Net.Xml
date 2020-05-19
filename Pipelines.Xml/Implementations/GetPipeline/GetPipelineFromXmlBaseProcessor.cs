namespace Pipelines.Xml.Implementations.GetPipeline
{
    public abstract class GetPipelineFromXmlBaseProcessor : SafeProcessor<QueryContext<IPipeline>>
    {
        public override bool SafeCondition(QueryContext<IPipeline> args)
        {
            return base.SafeCondition(args) && this.CustomSafeCondition(args);
        }

        protected abstract bool CustomSafeCondition(QueryContext<IPipeline> args);
    }
}