using System;

namespace Domain.Model
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string  CommentUserName { get; set; }
        public string  CommentEmail { get; set; }
        public string  CommentContent { get; set; }
        public DateTime CommnetDate { get; set; }
        public int CommnetStatus { get; set; }
        public virtual Post Post { get; set; }
    }
}