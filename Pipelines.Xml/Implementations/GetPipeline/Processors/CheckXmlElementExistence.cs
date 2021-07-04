using Pipelines.Implementations.Processors;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Pipelines.Xml.Implementations.GetPipeline.Processors
{
    [ProcessorOrder(20)]
    public class CheckXmlElementExistence : ExplanatoryProcessor
    {
    }
}