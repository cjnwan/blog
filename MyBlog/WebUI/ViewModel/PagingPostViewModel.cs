using System.Collections.Generic;
using WebUI.Common;
using WebUI.Dto;

namespace WebUI.ViewModel
{
    public class PagingPostViewModel
    {
        public IEnumerable<DtoPost> Post { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}