using System;

namespace Api.Controllers
{
    public class PostCreateModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime date { get; set; }
        public string SubCategoryId { get; set; }



    }
}