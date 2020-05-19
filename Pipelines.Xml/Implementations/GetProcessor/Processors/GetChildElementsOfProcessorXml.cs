using Pipelines.Implementations.Processors;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pipelines.Xml.Implementations.GetProcessor.Processors
{
    /// <summary>
    /// Gets all child elements and if they exist adds them to dictionary.
    /// </summary>
    /// <example>
    /// 
    /// Imagine we have an xml:
    /// 
    /// <processor type="Namespace.Class, Assembly">
    ///     <strategy>Base</strategy>
    ///     <level>Two</level>
    /// </processor>
    /// 
    /// the output context will have something like this:
    /// ["Children", new XElement[] { strategy, level } ]
    /// 
    /// </example>
    [ProcessorOrder(90)]
    public class GetChildElementsOfProcessorXml : SafeProcessor<QueryContext<IProcessor>>
    {
        public override Task SafeExecute(QueryContext<IProcessor> args)
        {
            var element = args.GetPropertyValueOrNull<XElement>(GetProcessorProperties.XElement);

            if (!element.HasElements)
            {
                return Done;
            }

            args.AddOrSkipPropertyIfExists(GetProcessorProperties.Children, element.Elements());
            return Done;
        }

        public override bool SafeCondition(QueryContext<IProcessor> args)
        {
            return base.SafeCondition(args) &&
                   args.DoesNotContainResult() &&
                   args.ContainsProperty(GetProcessorProperties.XElement) &&
                   args.DoesNotContainProperty(GetProcessorProperties.Children);
        }
    }
}