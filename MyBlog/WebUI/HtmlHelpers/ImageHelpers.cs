using System;
using System.Text;
using System.Web.Mvc;
using Domain.Model;
using WebUI.Common;

namespace WebUI.HtmlHelpers
{
    public static class ImageHelpers
    {
          public static MvcHtmlString BuildImage(this HtmlHelper html, Image image)
          {
              StringBuilder result = new StringBuilder();

              TagBuilder img = new TagBuilder("img");

              img.MergeAttribute("src","/Images/gallery/"+image.ImageName);
              img.MergeAttribute("alt",image.ImageDescription);

              result.Append(img.ToString());

              return MvcHtmlString.Create(result.ToString());
          }

          public static MvcHtmlString BuildPostImage(this HtmlHelper html, Image image)
          {
              StringBuilder result = new StringBuilder();

              TagBuilder img = new TagBuilder("img");

              img.MergeAttribute("src", "/Images/blogpic/" + image.ImageName);
              img.MergeAttribute("alt", image.ImageDescription);
              //img.MergeAttribute("class", "img-circle");

              result.Append(img.ToString());

              return MvcHtmlString.Create(result.ToString());
          }
    }
}