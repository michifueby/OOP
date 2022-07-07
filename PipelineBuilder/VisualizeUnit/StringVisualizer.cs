namespace VisualizeUnit
{
    using System;
    using PipelineBuilder;

    public class StringVisualizer
    {
        [Pipeline]
        [PipelineDescription("This method prints a integer to console.")]
        public void PrintInteger(int parameter)
        {
            Console.WriteLine(parameter);
        }
    }
}
