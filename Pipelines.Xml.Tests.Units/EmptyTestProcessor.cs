using System.Threading.Tasks;

namespace Pipelines.Xml.Tests.Units
{
    public class EmptyTestProcessor : SafeProcessor
    {
        public override Task SafeExecute(PipelineContext args)
        {
            return Done;
        }
    }
}