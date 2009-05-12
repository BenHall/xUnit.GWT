using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using xUnit.GWT.CustomResults;
using Xunit.Sdk;

namespace xUnit.GWT
{
    public class GwtTestCommand : ITestCommand
    {
        public List<TestPart> ThenList { get; set; }
        private readonly MethodInfo Method;
        private readonly string MethodName;
        private readonly string StoryText;
        private readonly List<TestPart> GivenList;
        private readonly List<TestPart> WhenList;
        private string TestName;

        public GwtTestCommand(MethodInfo method, string name, string story, List<TestPart> givenList,
                                                                              List<TestPart> whenList,
                                                                              List<TestPart> thenList)
        {
            Method = method;
            MethodName = name;
            StoryText = story;
            GivenList = givenList;
            WhenList = whenList;
            ThenList = thenList;
        }

        public MethodResult Execute(object testClass)
        {
            TestName = GetName();

            try  //Hack: Fix!
            {
                foreach (var p in GivenList)
                {
                    if (p.Action == null)
                        return new PendingResult(Method, TestName);
                    p.Action();
                }

                foreach (var p in WhenList)
                {
                    if (p.Action == null)
                        return new PendingResult(Method, TestName);
                    p.Action();
                }
            }
            catch (PendingException px)
            {
                return new PendingResult(Method, TestName);
            }


            if (ThenList.IsEmpty())
                return new PartMissingResult(Method, TestName, "Must have at least one type of then");

            MethodResult result = ExecuteThenPart(ThenList);
            if (result != null)
                return result;

            return new ScenarioPassed(StoryText, Method, TestName);
        }

        private string GetName()
        {
            string name = string.Empty;
            name = GetNameSection(name, GivenList, "Given");

            name = GetNameSection(name, WhenList, "When");

            name = GetNameSection(name, ThenList, "Then");

            return name;
        }

        private string GetNameSection(string name, List<TestPart> parts, string text)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                var part = parts[i];

                if (i == 0)
                    name = name + " " + text + " " + part.Message;
                else
                    name = name + " and " + part.Message;

            }
            return name;
        }

        private MethodResult ExecuteThenPart(List<TestPart> parts)
        {
            string current = string.Empty;
            try
            {
                foreach (var p in parts)
                {
                    current = p.Message;
                    p.Action();
                }
            }
            catch (PendingException px)
            {
                return new PendingResult(Method, TestName);
            }
            catch (Exception ex)
            {
                return new ScenarioFailed(StoryText, Method, ex, TestName, current);
            }

            return null;
        }


        public XmlNode ToStartXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<dummy/>");
            XmlNode testNode = XmlUtility.AddElement(doc.ChildNodes[0], "start");

            XmlUtility.AddAttribute(testNode, "name", Method.Name);
            XmlUtility.AddAttribute(testNode, "type", Method.ReflectedType.FullName);
            XmlUtility.AddAttribute(testNode, "method", Method.Name);

            return testNode;
        }

        public string DisplayName
        {
            get { return MethodName; }
        }

        public bool ShouldCreateInstance
        {
            get { return false; }
        }
    }

    public class TestPart
    {
        private KeyValuePair<string, Action> KeyValuePair;
        public TestPart(string key, Action value)
        {
            KeyValuePair = new KeyValuePair<string, Action>(key, value);
        }

        public string Message
        {
            get { return KeyValuePair.Key; }
        }

        public Action Action
        {
            get { return KeyValuePair.Value; }
        }
    }
}
