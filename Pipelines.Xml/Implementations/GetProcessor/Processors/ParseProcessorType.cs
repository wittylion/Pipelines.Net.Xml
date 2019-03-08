using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pipelines.Xml.Implementations.GetProcessor.Processors
{
    public class ParseProcessorType : SafeProcessor<QueryContext<IProcessor>>
    {
        public override Task SafeExecute(QueryContext<IProcessor> args)
        {
            var element = args.GetPropertyValueOrNull<XElement>(GetProcessorProperties.XElement);
            var attribute = args.GetPropertyValueOrNull<string>(GetProcessorProperties.ProcessorTypeAttribute);

            var processorType = element.Attribute(attribute)?.Value;

            if (!string.IsNullOrWhiteSpace(processorType))
            {
                args.AddOrSkipPropertyIfExists(GetProcessorProperties.ProcessorTypeString, processorType);
            }

            return Done;
        }

        public override bool SafeCondition(QueryContext<IProcessor> args)
        {
            return base.SafeCondition(args) && 
                   args.GetResult() == null &&
                   args.ContainsProperty(GetProcessorProperties.XElement) &&
                   args.ContainsProperty(GetProcessorProperties.ProcessorTypeAttribute) &&
                   !args.HasProperty(GetProcessorProperties.ProcessorTypeString);
        }
    }
}