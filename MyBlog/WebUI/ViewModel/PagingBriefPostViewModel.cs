using System.Collections.Generic;
using WebUI.Common;

namespace WebUI.ViewModel
{
    public class PagingBriefPostViewModel
    {
        public List<OnePost> OnePosts { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}