using Pipelines.Implementations.Processors;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Pipelines.Xml.Implementations.GetProcessor.Processors
{
    [ProcessorOrder(115)]
    public class CreateAnInstanceOfTheProcessor : SafeProcessor<QueryContext<IProcessor>>
    {
        public override Task SafeExecute(QueryContext<IProcessor> args)
        {
            var constructor = args.GetPropertyValueOrNull<ConstructorInfo>(GetProcessorProperties.Constructor);
            var childValues = args.GetPropertyValueOrDefault(
                GetProcessorProperties.ChildrenValues, Enumerable.Empty<KeyValuePair<string, string>>());

            var parameters = constructor.GetParameters().Select(x => x.Name).Join(childValues, x => x, x => x.Key, (x, y) => y.Value).Cast<object>().ToArray();

            var instance = constructor.Invoke(parameters);

            args.AddOrSkipPropertyIfExists(GetProcessorProperties.Instance, instance);
            return Done;
        }

        public override bool SafeCondition(QueryContext<IProcessor> args)
        {
            return base.SafeCondition(args) &&
                   args.HasProperty(GetProcessorProperties.Constructor) &&
                   !args.ContainsProperty(GetProcessorProperties.Instance) &&
                   args.DoesNotContainResult();
        }
    }
}