using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class SubCategory
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public virtual Category Categories { get; set; }
        public virtual IList<Post> Posts { get; set; }
        public SubCategory()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
