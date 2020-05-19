using Pipelines.Implementations.Processors;
using System;
using System.Threading.Tasks;

namespace Pipelines.Xml.Implementations.GetProcessor.Processors
{
    /// <summary>
    /// Obtains a type from the string in the dictionary.
    /// </summary>
    /// <example>
    /// 
    /// Imagine we have context:
    /// ["ProcessorTypeString", "Namespace.Class, Assembly"]
    /// 
    /// This processor finds a type Namespace.Class and put Type object in the dictionary.
    /// 
    /// In the end context should have this property:
    /// ["ProcessorType", typeof(Namespace.Class)]
    /// 
    /// </example>
    [ProcessorOrder(80)]
    public class TryToObtainType : SafeProcessor<QueryContext<IProcessor>>
    {
        public override Task SafeExecute(QueryContext<IProcessor> args)
        {
            var type = args.GetPropertyValueOrNull<string>(GetProcessorProperties.ProcessorTypeString);
            var typeObject = Type.GetType(type);

            if (typeObject == null)
            {
                args.AddWarning($"Cannot obtain a type of the processor [{type}].");
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
                   args.DoesNotContainResult();
        }
    }
}