using System;
using System.Threading.Tasks;

namespace Pipelines.Xml.Implementations.GetProcessor.Processors
{
    public class TryToObtainType : SafeProcessor<QueryContext<IProcessor>>
    {
        public override Task SafeExecute(QueryContext<IProcessor> args)
        {
            var type = args.GetPropertyValueOrNull<string>(GetProcessorProperties.ProcessorTypeString);
            var typeObject = Type.GetType(type);

            if (typeObject == null)
            {
                args.AddWarning("Cannot obtain a type of the processor.");
                return Done;
            }

            args.AddOrSkipPropertyIfExists(GetProcessorProperties.ProcessorType, typeObject);
            return Done;
        }

        public override bool SafeCondition(QueryContext<IProcessor> args)
        {
            return base.SafeCondition(args) &&
                   args.HasProperty(GetProcessorProperties.ProcessorTypeString) &&
                   !args.ContainsProperty(GetProcessorProperties.ProcessorType) &&
                   args.GetResult() == null;
        }
    }
}