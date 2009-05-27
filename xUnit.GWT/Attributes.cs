using System;

namespace xUnit.GWT
{
    public interface IStoryAttribute
    {
        string Value { get; }
        string Type { get; }
    }
    public class So_ThatAttribute : Attribute, IStoryAttribute
    {
        private readonly string That;

        public So_ThatAttribute(string that)
        {
            That = that;
        }

        public string Value
        {
            get { return That; }
        }

        public string Type
        {
            get { return "So_That"; }
        }
    }

    public class I_WantAttribute : Attribute, IStoryAttribute
    {
        private readonly string Want;

        public I_WantAttribute(string want)
        {
            Want = want;
        }

        public string Value
        {
            get { return Want; }
        }

        public string Type
        {
            get { return "I_Want"; }
        }
    }

    public class As_AAttribute : Attribute, IStoryAttribute
    {
        private readonly string Asa;

        public As_AAttribute(string asa)
        {
            Asa = asa;
        }

        public string Value
        {
            get { return Asa; }
        }

        public string Type
        {
            get { return "As_A"; }
        }
    }


    public class Feature : Attribute, IStoryAttribute
    {
        private readonly string Text;

        public Feature(string text)
        {
            Text = text;
        }

        public string Value
        {
            get { return Text;}
        }

        public string Type
        {
            get { return "Feature"; }
        }
    }
}
