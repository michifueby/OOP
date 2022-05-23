namespace MyLib
{
    [Description("My Description of MyClass")]
    public class MyClass
    {
        public MyClass()
        {
        }

        public string Info
        {
            get;
            set;
        }

        public void MyMethod()
        {

        }

        public override string ToString()
        {
            return "This is my lib!";
        }
    }
}
