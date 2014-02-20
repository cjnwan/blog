using System.Collections.Generic;
using WebUI.Common;

namespace WebUI.ViewModel
{
    public class PostViewModel
    {
        public List<OnePost> OnePost { get; set; }

        public PagingInfo PagingInfo { get; set; } 
    }
}