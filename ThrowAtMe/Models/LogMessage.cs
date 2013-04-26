using System;

namespace ThrowAtMe.Models
{
    public class LogMessage
    {
        public LogMessage()
        {
            LogDate = DateTime.Now;
        }

        public string Id { get; set; }
        public DateTime LogDate { get; set; }

        public string LogType { get; set; }
        public string ErrorMessage { get; set; }
        public string Url { get; set; }
        public int LineNumber { get; set; }

        

        public override string ToString()
        {
            return string.Format("ErrorMessage: {0}, Url: {1}, LineNumber: {2}", ErrorMessage, Url, LineNumber);
        }
    }
}