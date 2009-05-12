using System;
using System.Reflection;
using System.Xml;
using Xunit.Sdk;

namespace xUnit.GWT
{
    public class PartMissingResult : FailedResult
    {
        private readonly string PartMissing;

        public PartMissingResult(MethodInfo method, string name, string partMissing) : base(method, new PartMissing(partMissing), name)
        {
            PartMissing = partMissing;
        }

        public override XmlNode ToXml(XmlNode parentNode)
        {
            XmlNode node = base.ToXml(parentNode);
            XmlUtility.AddAttribute(node, "result", "Fail");
            XmlUtility.AddAttribute(node, "reason", "Part Missing");
            base.AddTime(node);
            XmlNode node2 = XmlUtility.AddElement(node, "failure");
            XmlNode element = XmlUtility.AddElement(node2, "message");
            element.InnerText = PartMissing;
            return node;
        }
    }

    public class PartMissing : Exception
    {
        public PartMissing(string part)
            : base(part)
        { }
    }
}