using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Model
{
    public class Post
    {
        public Post()
        {
            Categories = new Collection<PostCat>();
            Tags = new Collection<PostTag>();
        }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public DateTime PostAddedTime { get; set; }
        public DateTime PostEditedTime { get; set; }
        public bool IsPrivate { get; set; }
        public virtual ICollection<PostCat> Categories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostTag> Tags { get; set; }
        public int  User { get; set; }
    }
}