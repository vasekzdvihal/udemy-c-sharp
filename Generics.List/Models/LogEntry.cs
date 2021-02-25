using System;

namespace Generics.List.Models
{
    public class LogEntry
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public DateTime TimeOfEvent { get; set; } = DateTime.UtcNow;
    }
}