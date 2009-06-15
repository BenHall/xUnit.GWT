using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using Xunit.Sdk;

namespace xUnit.GWT.CustomResults
{
    public class PendingResult : SkipResult
    {
        public PendingResult(MethodInfo method, string name)
            : base(method.Name, "Pending", name, new Dictionary<string, string>(), name + " is still pending to be implemented...")
        {}

        public override XmlNode ToXml(XmlNode parentNode)
        {
            XmlUtility.AddAttribute(parentNode, "story", "");
            XmlNode node = base.ToXml(parentNode);
            XmlUtility.AddAttribute(node, "result", "Pending");
            XmlUtility.AddCDataSection(XmlUtility.AddElement(XmlUtility.AddElement(node, "reason"), "message"), Reason);
            return node;
        }
    }

    public class PendingException : Exception
    {
        
    }
}