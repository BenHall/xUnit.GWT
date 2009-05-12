using System;
using System.Reflection;
using System.Xml;
using Xunit.Sdk;

namespace xUnit.GWT
{
    public class ScenarioFailed : FailedResult
    {
        private readonly string Story;

        public ScenarioFailed(string story, MethodInfo method, Exception ex, string testName, string scenarioPartFailed)
            : base(method, new PartFailed("Then " + scenarioPartFailed + " failed to complete successfully", ex), testName)
        {
            Story = story;
        }

        public override XmlNode ToXml(XmlNode parentNode)
        {
            XmlUtility.AddAttribute(parentNode, "story", Story);
            XmlNode node = base.ToXml(parentNode);
            XmlUtility.AddAttribute(node, "result", "Fail");
            XmlUtility.AddAttribute(node, "reason", "Part Missing");
            base.AddTime(node);
            XmlNode node2 = XmlUtility.AddElement(node, "failure");
            return node;
        }
    }
}