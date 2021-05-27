using System;

namespace Api.Controllers
{
    public class PostReplyModel
    {
        public string Discussion { get; set; }
        public DateTime date { get; set; }
        public string PostId { get; set; }

    }
}