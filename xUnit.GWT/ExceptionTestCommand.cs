using System;
using System.Reflection;
using System.Xml;
using Xunit.Sdk;

namespace xUnit.GWT
{
    public class ExceptionTestCommand : ITestCommand
    {
        private readonly MethodInfo Method;
        private readonly Exception Exception;

        public ExceptionTestCommand(MethodInfo method, Exception exception)
        {
            Method = method;
            Exception = exception;
        }

        public MethodResult Execute(object testClass)
        {
            return new FailedResult(Method, Exception, Method.Name);
        }


        public string DisplayName
        {
            get { return Method.Name; }
        }

        public bool ShouldCreateInstance
        {
            get { return false; }
        }

        public XmlNode ToStartXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<dummy/>");
            XmlNode xmlNode = XmlUtility.AddElement(doc.ChildNodes[0], "start");

            string typeName = Method.ReflectedType.FullName;
            string methodName = Method.Name;

            XmlUtility.AddAttribute(xmlNode, "name", typeName + "." + methodName);
            XmlUtility.AddAttribute(xmlNode, "type", typeName);
            XmlUtility.AddAttribute(xmlNode, "method", methodName);

            return xmlNode;
        }
    }
}