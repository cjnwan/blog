using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebUI.Common;

namespace WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
          PagingInfo pagingInfo,
          Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            TagBuilder fir = new TagBuilder("a");
            fir.MergeAttribute("href", pageUrl(1));
            fir.InnerHtml = "首页";
            result.Append(fir.ToString());
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("active");
                result.Append(tag.ToString());
            }
            TagBuilder las = new TagBuilder("a");
            las.MergeAttribute("href", pageUrl(pagingInfo.TotalPages));
            las.InnerHtml = "尾页";
            result.Append(las.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}