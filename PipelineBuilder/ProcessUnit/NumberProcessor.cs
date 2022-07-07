namespace ProcessUnit
{
    using System;
    using PipelineBuilder;

    public class NumberProcessor
    {
        [Pipeline]
        [PipelineDescription("This method adds a number with a random number between 0 and 100.")]
        public int AddNumbers(int parameter)
        {
            Random random = new Random();
            return parameter + random.Next(0, 100);
        }
    }
}
