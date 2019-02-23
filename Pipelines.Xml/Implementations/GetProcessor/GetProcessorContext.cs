using System.Xml.Linq;

namespace Pipelines.Xml.Implementations.GetProcessor
{
    public class GetProcessorContext : QueryContext<IProcessor>
    {
        public XElement XElement
        {
            get => this.GetPropertyValueOrNull<XElement>(nameof(XElement));
            set => this.SetOrAddProperty(nameof(XElement), value);
        }

        public string ProcessorType
        {
            get => this.GetPropertyValueOrNull<string>(nameof(ProcessorType));
            set => this.SetOrAddProperty(nameof(ProcessorType), value);
        }

    }
}