using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class Category
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public virtual IList<SubCategory> SubCategories { get; set; }

        public Category()
        {
            Id = Guid.NewGuid().ToString();

            Date = DateTime.Now;
        }
    }
}
