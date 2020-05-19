using System.Threading.Tasks;

namespace Pipelines.Xml.Tests.Units.Data
{
    public class EmptyTestProcessor : SafeProcessor
    {
        public static string TypeDefinition = "Pipelines.Xml.Tests.Units.Data.EmptyTestProcessor, Pipelines.Xml.Tests.Units";

        public override Task SafeExecute(PipelineContext args)
        {
            return Done;
        }
    }
}