using System;

namespace ChatApplication.Models
{
    public class Message
    {
        public string username { get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
    }
}