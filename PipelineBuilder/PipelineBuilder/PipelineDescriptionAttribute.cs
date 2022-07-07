namespace PipelineBuilder
{
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public class PipelineDescriptionAttribute : Attribute
    {
        private string description;

        public PipelineDescriptionAttribute(string description)
        {
            this.description = description;
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(this.Description), "The specified value must not be null!");
                }
            }
        }
    }
}
