namespace SourceUnit
{
    using PipelineBuilder;
    using System;

    public class SourceUnit
    {
        [Pipeline]
        [PipelineDescription("This method generates a random number between 1 and 10000.")]
        public string GenerateNumber()
        {
            Random random = new Random();

            return random.Next(1, 10000).ToString();
        }
    }
}
