using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pipelines.Xml.Implementations.GetProcessor.Processors
{
    public class ParseProcessorType : SafeProcessor<GetProcessorContext>
    {
        public override Task SafeExecute(GetProcessorContext args)
        {
            args.ProcessorType = args.XElement.Attribute(args.ProcessorAttribute)?.Value;

            return Done;
        }

        public override bool SafeCondition(GetProcessorContext args)
        {
            return base.SafeCondition(args) && string.IsNullOrWhiteSpace(args.ProcessorType);
        }
    }
}