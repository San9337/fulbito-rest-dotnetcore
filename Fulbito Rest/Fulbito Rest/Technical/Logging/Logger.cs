using System;

namespace FulbitoRest.Technical.Logging
{
    public class Logger : ICustomLogger
    {
        public void Log(string message)
        {
            message = EmbeddCurrentTime(message);

            Console.WriteLine(message);
            System.Diagnostics.Debug.Write(message);
        }

        private static string EmbeddCurrentTime(string message)
        {
            message = DateTime.Now.ToString("hh:mm:ss") + "  -  " + message;

            return message;
        }
    }
}
