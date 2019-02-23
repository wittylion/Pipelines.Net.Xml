using System.Xml.Linq;

namespace Pipelines.Xml.Implementations.GetPipeline
{
    public class GetPipelineContext : QueryContext<IPipeline>
    {
        public XElement XElement
        {
            get => this.GetPropertyValueOrNull<XElement>(nameof(XElement));
            set => this.SetOrAddProperty(nameof(XElement), value);
        }
        
        public string ProcessorTagName
        {
            get => this.GetPropertyValueOrNull<string>(nameof(ProcessorTagName));
            set => this.SetOrAddProperty(nameof(ProcessorTagName), value);
        }

        public string TypeAttributeName
        {
            get => this.GetPropertyValueOrNull<string>(nameof(TypeAttributeName));
            set => this.SetOrAddProperty(nameof(TypeAttributeName), value);
        }
    }
}