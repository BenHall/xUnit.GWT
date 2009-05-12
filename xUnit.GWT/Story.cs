using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using xUnit.GWT.CustomResults;
using Xunit.Sdk;

namespace xUnit.GWT
{
    public class Story
    {
        protected Action Pending = () => { throw new PendingException(); };   //This is simply to allow us to have a pending block...

        static List<TestPart> given;
        static List<TestPart> when;
        static List<TestPart> then;
        static List<TestPart> setupGivens;
        static List<TestPart> setupWhen;
        static List<TestPart> setupThen;

        public Story()
        {
            setupGivens = new List<TestPart>();
            setupWhen = new List<TestPart>();
            setupThen = new List<TestPart>();
            given = new List<TestPart>();
            when = new List<TestPart>();
            then = new List<TestPart>();
        }

        public static void SetGiven(string message, Action givenAction)
        {
            setupGivens.Add(new TestPart(message, givenAction));
        }

        public static void Given(string message, Action givenAction)
        {
            TestPart pair = new TestPart(message, givenAction);
            given.Add(pair);
            setupGivens.Add(pair);
        }

        public void Given(string message)
        {
            TestPart keyValuePair = GetTestPart(setupGivens, message);

            given.Add(keyValuePair);
        }


        public static void SetWhen(string message, Action givenAction)
        {
            setupWhen.Add(new TestPart(message, givenAction));
        }

        public static void When(string message, Action givenAction)
        {
            TestPart pair = new TestPart(message, givenAction);
            when.Add(pair);
            setupWhen.Add(pair);
        }

        public void When(string message)
        {
            TestPart keyValuePair = GetTestPart(setupWhen, message);

            when.Add(keyValuePair);
        }

        public static void SetThen(string message, Action givenAction)
        {
            setupThen.Add(new TestPart(message, givenAction));
        }

        public static void Then(string message, Action givenAction)
        {
            TestPart pair = new TestPart(message, givenAction);
            then.Add(pair);
            setupThen.Add(pair);
        }

        public void Then(string message)
        {
            TestPart keyValuePair = GetTestPart(setupThen, message);

            then.Add(keyValuePair);
        }

        private TestPart GetTestPart(List<TestPart> list, string message)
        {
            TestPart keyValuePair = list.Find(r => r.Message == message);
            if (keyValuePair == null)
                keyValuePair = new TestPart(message, null);
            return keyValuePair;
        }

        public static IEnumerable<ITestCommand> ToTestCommands(string story, MethodInfo method)
        {
            yield return new GwtTestCommand(method, method.Name, story, given, when, then);
        }
    }
}