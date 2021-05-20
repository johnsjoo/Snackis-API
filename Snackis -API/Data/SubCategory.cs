using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class SubCategory
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public virtual Category Categories { get; set; }
        [ForeignKey("Category")]
        public string CategoryId { get; set; }
        public virtual IList<Post> Posts { get; set; }
        public SubCategory()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
