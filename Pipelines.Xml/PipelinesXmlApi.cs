using System;
using System.Xml.Linq;
using System.Xml.XPath;
using Pipelines.Implementations.Pipelines;
using Pipelines.Xml.Implementations.GetPipeline;

namespace Pipelines.Xml
{
    public class PipelinesXmlApi
    {
        public static PipelineParser Parser = new PipelineParser();

        public static IPipeline GetPipelineFromXmlOrNull(XElement xmlElement)
        {
            return GetPipelineFromXml(xmlElement, null);
        }

        public static IPipeline GetPipelineFromXmlOrEmpty(XElement xmlElement)
        {
            return GetPipelineFromXml(xmlElement, PredefinedPipeline.Empty);
        }

        public static IPipeline GetPipelineFromXml(XElement xmlElement, IPipeline defaultValue)
        {
            return Parser.GetPipeline(xmlElement) ?? defaultValue;
        }

        public static IPipeline GetPipelineFromXmlDocument(XDocument xmlDocument, IPipeline defaultValue)
        {
            return GetPipelineFromXml(xmlDocument?.Root, defaultValue);
        }

        public static IPipeline GetPipelineFromXmlByXPathOrEmpty(XNode xmlNode, string xpathExpression)
        {
            return GetPipelineFromXmlByXPath(xmlNode, xpathExpression, PredefinedPipeline.Empty);
        }

        public static IPipeline GetPipelineFromXmlByXPathOrNull(XNode xmlNode, string xpathExpression)
        {
            return GetPipelineFromXmlByXPath(xmlNode, xpathExpression, null);
        }

        public static IPipeline GetPipelineFromXmlByXPath(XNode xmlNode, string xpathExpression, IPipeline defaultValue)
        {
            XElement xmlElement;
            try
            {
                xmlElement = xmlNode?.XPathSelectElement(xpathExpression);
            }
            catch
            {
                return defaultValue;
            }

            return GetPipelineFromXml(xmlElement, defaultValue);
        }
    }
}
