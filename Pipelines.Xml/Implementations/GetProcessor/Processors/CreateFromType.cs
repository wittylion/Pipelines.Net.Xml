using System;
using System.Threading.Tasks;

namespace Pipelines.Xml.Implementations.GetProcessor.Processors
{
    public class CreateFromType : SafeProcessor<GetProcessorContext>
    {
        public override Task SafeExecute(GetProcessorContext args)
        {
            var type = args.GetPropertyValueOrNull<string>("processorType");
            if (string.IsNullOrWhiteSpace(type))
            {
                return Done;
            }

            var typeObject = Type.GetType(type);

            if (typeObject == null)
            {
                args.AbortPipelineWithErrorMessage("Cannot obtain a type of the processor.");
                return Done;
            }

            if (!typeof(IProcessor).IsAssignableFrom(typeObject))
            {
                args.AbortPipelineWithErrorMessage("Cannot create a processor that is not inherited from IProcessor.");
                return Done;
            }

            var result = Activator.CreateInstance(typeObject) as IProcessor;
            args.SetResultWithInformation(result, $"Created processor for {type}");

            return Done;
        }
    }
}