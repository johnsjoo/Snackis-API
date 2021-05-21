using System;

namespace Api.Controllers
{
    public class PostSubCategoryModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string CategoryId { get; set; }
    }
}