namespace Converter
{
    using System;
    using PipelineBuilder;

    public class StringToIntConverter
    {
        [Pipeline]
        [PipelineDescription("This method converts a string to an int.")]
        public int ConvertStringToInt(string parameter)
        {
            int number = 0;
            
            bool isNumberValid = int.TryParse(parameter, out number);

            if (!isNumberValid)
            {
                Console.WriteLine("Not valid number!");
            }

            return number;
        }
    }
}
