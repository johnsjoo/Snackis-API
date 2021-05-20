using Api.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class Post
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("SubCategory")]
        public string SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsReported { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User { get; set; }

        public Post()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
