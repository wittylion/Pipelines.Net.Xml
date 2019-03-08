using System;
using System.Threading.Tasks;

namespace Pipelines.Xml.Implementations.GetProcessor.Processors
{
    public class CreateTypeDerivedFromProcessorInterface : SafeProcessor<QueryContext<IProcessor>>
    {
        public override Task SafeExecute(QueryContext<IProcessor> args)
        {
            var typeObject = args.GetPropertyValueOrNull<Type>(GetProcessorProperties.ProcessorType);
            if (!typeof(IProcessor).IsAssignableFrom(typeObject))
            {
                args.AddWarning("Cannot create a processor that is not inherited from IProcessor.");
                return Done;
            }

            var result = Activator.CreateInstance(typeObject) as IProcessor;
            args.SetResultWithInformation(result, $"Processor created for type {typeObject}.");

            return Done;
        }

        public override bool SafeCondition(QueryContext<IProcessor> args)
        {
            return base.SafeCondition(args) && args.GetResult() == null &&
                   args.HasProperty(GetProcessorProperties.ProcessorType);
        }
    }
}