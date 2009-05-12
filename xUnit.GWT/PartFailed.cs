using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using Xunit.Sdk;

namespace xUnit.GWT
{
    public class PartFailed : Exception
    {
        public PartFailed(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}