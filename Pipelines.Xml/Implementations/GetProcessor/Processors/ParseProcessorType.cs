using Pipelines.Implementations.Processors;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pipelines.Xml.Implementations.GetProcessor.Processors
{
    /// <summary>
    /// Parses the processor type from attribute and put it into the dictionary.
    /// </summary>
    /// <example>
    /// 
    /// Parses the specified below part:
    /// 
    /// <processor type="Namespace.Class, Assembly" />
    ///                  ^^^^^^^^^^^^^^^^^^^^^^^^^
    /// 
    /// </example>
    [ProcessorOrder(40)]
    public class ParseProcessorType : SafeProcessor<QueryContext<IProcessor>>
    {
        public override Task SafeExecute(QueryContext<IProcessor> args)
        {
            // Get needed properties.
            var element = args.GetPropertyValueOrNull<XElement>(GetProcessorProperties.XElement);
            var attribute = args.GetPropertyValueOrNull<string>(GetProcessorProperties.ProcessorTypeAttribute);

            // Check attribute presence.
            XAttribute typeAttribute = element.Attribute(attribute);
            if (typeAttribute == null)
            {
                args.AddWarning($"Attribute that should contain processor type [{attribute}] was not found. Try reviewing the XML element.");
                return Done;
            }

            // Get processor type name and assembly.
            var processorType = typeAttribute.Value;

            // Add property to the dictionary.
            if (!string.IsNullOrWhiteSpace(processorType))
            {
                args.AddOrSkipPropertyIfExists(GetProcessorProperties.ProcessorTypeString, processorType);
            }
            else
            {
                args.AddWarning("The processor type contains an empty string.");
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