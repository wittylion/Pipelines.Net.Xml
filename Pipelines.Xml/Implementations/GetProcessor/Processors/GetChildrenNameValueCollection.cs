using Pipelines.Implementations.Processors;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pipelines.Xml.Implementations.GetProcessor.Processors
{
    [ProcessorOrder(100)]
    public class GetChildrenNameValueCollection : SafeProcessor<QueryContext<IProcessor>>
    {
        public override Task SafeExecute(QueryContext<IProcessor> args)
        {
            var children = args.GetPropertyValueOrNull<IEnumerable<XElement>>(GetProcessorProperties.Children);
            var childrenValues = children.ToDictionary(child => child.Name.LocalName, child => child.Value);

            args.AddOrSkipPropertyIfExists(GetProcessorProperties.ChildrenValues, childrenValues);

            return Done;
        }

        public override bool SafeCondition(QueryContext<IProcessor> args)
        {
            return base.SafeCondition(args) &&
                   args.DoesNotContainResult() &&
                   args.ContainsProperty(GetProcessorProperties.Children) &&
                   args.DoesNotContainProperty(GetProcessorProperties.ChildrenValues);
        }
    }
}