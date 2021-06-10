using System;

namespace Api.Controllers
{
    public class MessageModel
    {
        public string UserId { get; set; }
        public string message { get; set; }
        public DateTime Date { get; set; }
        public string MessageReceiver { get; set; }
    }
}