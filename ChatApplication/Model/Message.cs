using MongoDB.Bson;
using System;

namespace ChatApplication.Models
{
    public class Message
    {
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public DateTime SentOn { get; set; }
    }
}