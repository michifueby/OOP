namespace MyLib
{
    using System;

    // Restricts attribute to classes only
    [AttributeUsage(AttributeTargets.Class)]
    public class Description : Attribute
    {
        public Description(string text)
        {
            this.Text = text;
        }

        public string Text
        {
            get;
            set;
        }
    }
}
