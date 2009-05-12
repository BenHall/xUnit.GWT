using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace xUnit.GWT
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ScenarioAttribute : FactAttribute
    {
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(MethodInfo method)
        {
            try
            {
                object obj = Activator.CreateInstance(method.ReflectedType);
                string story = GetStoryAttributes(obj);
                method.Invoke(obj, null);
                return Story.ToTestCommands(story, method);
            }
            catch (Exception ex)
            {
                return new ITestCommand[] { new ExceptionTestCommand(method, ex) };
            }
        }

        private string GetStoryAttributes(object o)
        {
            string AsA = string.Empty;
            string IWant = string.Empty;
            string SoThat = string.Empty;
            foreach (var attribute in o.GetType().GetCustomAttributes(true))
            {
                IStoryAttribute a = attribute as IStoryAttribute;
                switch(a.Type)
                {
                    case "As_A":
                        AsA = a.Value;
                        break;
                    case "I_Want":
                        IWant = a.Value;
                        break;
                    case "So_That":
                        SoThat = a.Value;
                        break;
                }
            }
            return string.Format("As a {0} I want {1} so that {2}", AsA, IWant, SoThat);
        }
    }
}