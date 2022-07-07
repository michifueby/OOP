namespace PipelineBuilder
{
    using System;

    /// <summary>
    /// Represents the Logger class.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Log a text with an foreground color to the console.
        /// </summary>
        /// <param name="foregroundColor">Specified the foreground color.</param>
        /// <param name="text">Specified the text.</param>
        public void LogToConsole(ConsoleColor foregroundColor, string text)
        {
            var defaultForegroundColor = Console.ForegroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(text);

            Console.ForegroundColor = defaultForegroundColor;
        }
    }
}