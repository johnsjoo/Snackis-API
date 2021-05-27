using Api.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class PostDiscussion
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public string Discussion { get; set; }
        public DateTime Date { get; set; }
        public bool IsReported { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }


        public PostDiscussion()
        {
            Id = Guid.NewGuid().ToString();
            IsReported = false;
        }
    }
}
