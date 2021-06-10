using Api.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class Message
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public string message { get; set; }
        public DateTime Date { get; set; }
        public string MessageReceiver { get; set; }
        public virtual User User { get; set; }



        public Message()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
