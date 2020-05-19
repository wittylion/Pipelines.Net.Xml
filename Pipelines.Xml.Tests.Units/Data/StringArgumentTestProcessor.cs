using System.Threading.Tasks;

namespace Pipelines.Xml.Tests.Units.Data
{
    public class StringArgumentTestProcessor : SafeProcessor
    {
        public StringArgumentTestProcessor()
        {
            Parameter = "default";
        }

        public StringArgumentTestProcessor(string parameter)
        {
            Parameter = parameter;
        }

        public string Parameter { get; }

        public override Task SafeExecute(PipelineContext args)
        {
            return Done;
        }
    }
}