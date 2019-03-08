using System;
using System.Xml.Linq;

namespace Pipelines.Xml.Implementations.GetProcessor
{
    public class GetProcessorContext : QueryContext<IProcessor>
    {
        public XElement XElement
        {
            get => this.GetPropertyValueOrNull<XElement>(GetProcessorProperties.XElement);
            set => this.SetOrAddProperty(GetProcessorProperties.XElement, value);
        }

        public string ProcessorAttribute
        {
            get => this.GetPropertyValueOrNull<string>(nameof(GetProcessorProperties.ProcessorTypeAttribute));
            set => this.SetOrAddProperty(nameof(GetProcessorProperties.ProcessorTypeAttribute), value);
        }

        public Type ProcessorType
        {
            get => this.GetPropertyValueOrNull<Type>(GetProcessorProperties.ProcessorType);
            set => this.SetOrAddProperty(GetProcessorProperties.ProcessorType, value);
        }
    }
}