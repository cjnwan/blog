using System.Collections.Generic;

namespace Domain.Model
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public string TagSlug { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}