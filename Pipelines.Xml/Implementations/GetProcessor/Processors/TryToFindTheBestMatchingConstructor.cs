using Pipelines.Implementations.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Pipelines.Xml.Implementations.GetProcessor.Processors
{
    [ProcessorOrder(105)]
    public class TryToFindTheBestMatchingConstructor : SafeProcessor<QueryContext<IProcessor>>
    {
        public override Task SafeExecute(QueryContext<IProcessor> args)
        {
            var typeObject = args.GetPropertyValueOrNull<Type>(GetProcessorProperties.ProcessorType);
            var childValues = args.GetPropertyValueOrDefault(
                GetProcessorProperties.ChildrenValues, Enumerable.Empty<KeyValuePair<string, string>>());

            var constructors = typeObject.GetConstructors();

            foreach (var constructor in constructors.OrderByDescending(x => x.GetParameters().Length))
            {
                var parameters = constructor.GetParameters();
                if (parameters.Length > childValues.Count()) continue;
                if (parameters.Select(x => x.Name).Intersect(childValues.Select(x => x.Key)).Count() != parameters.Length) continue;

                args.AddOrSkipPropertyIfExists(GetProcessorProperties.Constructor, constructor);
                break;
            }

            return Done;
        }


        public override bool SafeCondition(QueryContext<IProcessor> args)
        {
            return base.SafeCondition(args) &&
                   args.HasProperty(GetProcessorProperties.ProcessorType) &&
                   !args.ContainsProperty(GetProcessorProperties.Constructor) &&
                   args.DoesNotContainResult();
        }
    }
}