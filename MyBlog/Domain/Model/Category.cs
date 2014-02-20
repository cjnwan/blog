using System.Collections.Generic;

namespace Domain.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}