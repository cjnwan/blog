using System;
using System.Collections.Generic;
using Domain.Model;

namespace WebUI.Dto
{
    public class DtoPost
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public string  PostAddedTime { get; set; }
        public string  PostEditedTime { get; set; }
        public bool IsPrivate { get; set; }
    }
}