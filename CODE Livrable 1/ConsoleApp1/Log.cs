using System;
namespace consoleApp
{
    public class Log
    {
        public double timestamp;
        public string message;

        public Log(string message, double timestamp)
        {
            this.message = message;
            this.timestamp = timestamp;
        }
    }
}
