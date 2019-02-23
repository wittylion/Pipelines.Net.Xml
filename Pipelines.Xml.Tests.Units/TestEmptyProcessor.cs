using System.Threading.Tasks;

namespace Pipelines.Xml.Tests.Units
{
    public class TestEmptyProcessor : SafeProcessor
    {
        public override Task SafeExecute(PipelineContext args)
        {
            return Done;
        }
    }
}