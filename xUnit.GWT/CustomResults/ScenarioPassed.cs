using System.Reflection;
using System.Xml;
using Xunit.Sdk;

namespace xUnit.GWT
{
    public class ScenarioPassed : PassedResult
    {
        private readonly string Story;
        public MethodInfo Method { get; set; }

        public ScenarioPassed(string story, MethodInfo method, string name) : base(method, name)
        {
            Story = story;
            Method = method;
        }

        public override XmlNode ToXml(XmlNode parentNode)
        {
            XmlUtility.AddAttribute(parentNode, "story", Story);
            XmlNode node = base.ToXml(parentNode);
            XmlUtility.AddAttribute(node, "result", "Pass");
            return node;
        }
    }
}