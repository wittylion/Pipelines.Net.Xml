using Pipelines.Implementations.Processors;
using System;
using System.Threading.Tasks;

namespace Pipelines.Xml.Implementations.GetProcessor.Processors
{
    /// <summary>
    /// If the previously obtained type is <see cref="IProcessor"/>
    /// creates an instance of this type.
    /// </summary>
    /// <example>
    /// 
    /// Imagine we have context:
    /// ["Instance", new Namespace.Class()]
    /// 
    /// and the class is derived from IProcessor like this:
    /// 
    /// namespace Namespace {
    ///     class Class : IProcessor { }
    /// }
    /// 
    /// after execution context will have:
    /// Result = new Namespace.Class()
    /// 
    /// </example>
    [ProcessorOrder(130)]
    public class UseInstanceDerivedFromProcessorInterface : SafeProcessor<QueryContext<IProcessor>>
    {
        public override Task SafeExecute(QueryContext<IProcessor> args)
        {
            var instance = args.GetPropertyValueOrNull<object>(GetProcessorProperties.Instance);
            if (!(instance is IProcessor result))
            {
                args.AddWarning("Cannot create a processor that is not inherited from IProcessor.");
                return Done;
            }

            args.SetResultWithInformation(result, $"Processor created.");

            return Done;
        }

        public override bool SafeCondition(QueryContext<IProcessor> args)
        {
            return base.SafeCondition(args) && args.DoesNotContainResult() &&
                   args.ContainsProperty(GetProcessorProperties.Instance);
        }
    }
}