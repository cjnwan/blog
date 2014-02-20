using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Domain.Model;
using Service;
using WebUI.Common;
using WebUI.ViewModel;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IPostService _postService;
        private ICategoryService _categoryService;
        private ITagService _tagService;
        private IImageService _imageService;
        private IPostCatService _postCatService;
        private int PageSize = 4;

        public HomeController(IPostService post, ICategoryService category, ITagService tag, IImageService image,IPostCatService postCat)
        {
            _postService = post;
            _categoryService = category;
            _tagService = tag;
            _imageService = image;
            _postCatService = postCat;
           
        }

        public ActionResult Index(int page=1)
        {
            List<Post>posts = _postService.Entities.Where(p=>p.IsPrivate==false).OrderByDescending(p => p.PostAddedTime).Skip((page - 1) * PageSize)
                                 .Take(PageSize).ToList();

            List<OnePost> onePosts = new List<OnePost>();

            foreach (var post in posts)
            {
                OnePost one = new OnePost();

                List<Category> categories = new List<Category>();
                List<Tag> tags = new List<Tag>();
                Image image = new Image();

                foreach (var pc in post.Categories)
                {
                   categories.Add(_categoryService.Entities.Single(c=>c.CategoryId==pc.CatId));
                }

                foreach (var pt in post.Tags)
                {
                    tags.Add(_tagService.Entities.Single(t => t.TagId == pt.TagId));
                }


                post.PostContent = GetHtmlImageUrlList(post.PostContent);

                image = _imageService.Entities.SingleOrDefault(i => i.PostImageId == post.PostId);
                one.Categories = categories;
                one.Post = post;
                one.Tags = tags;
                if (image == null)
                {
                    image = _imageService.Entities.Single(i => i.ImageName == "moren.jpg");
                }
                one.Image = image;
                onePosts.Add(one);
            }
            var briefPostViewModel = new PagingBriefPostViewModel()
                {
                    OnePosts = onePosts,
                    PagingInfo = new PagingInfo()
                        {
                            CurrentPage = page,
                            ItemsPerPage = PageSize,
                            TotalItems = _postService.Entities.Where(p=>p.IsPrivate==false).Count()

                        }
                };
            ViewData["Category"] = _categoryService.Entities;
            if (_postService.Entities.Count() < 5)
            {
                ViewData["RecentPost"] = _postService.Entities.OrderByDescending(p => p.PostAddedTime);
            }
            else
            {
                ViewData["RecentPost"] = _postService.Entities.OrderByDescending(p => p.PostAddedTime).Take(5);
            }
            
            return View("PostBrief", briefPostViewModel);
        }

        public ActionResult Category(string name,int page=1)
        {
            int id = _categoryService.Entities.Single(c => c.CategoryName == name).CategoryId;


            List<PostCat> postCats=_postCatService.Entities.Where(p => p.CatId == id).ToList();

            List<Post> posts = new List<Post>();

            foreach (var postCat in postCats)
            {
                foreach (var p in _postService.Entities)
                {
                    if (postCat.PostId == p.PostId)
                    {
                       posts.Add(p); 
                    }
                }
            }

             posts = posts.OrderBy(p => p.PostId).Skip((page - 1) * PageSize)
                                 .Take(PageSize).ToList();

           
            

            List<OnePost> onePosts = new List<OnePost>();

            foreach (var post in posts)
            {
                OnePost one = new OnePost();

                List<Category> categories = new List<Category>();
                List<Tag> tags = new List<Tag>();
                Image image = new Image();

                foreach (var pc in post.Categories)
                {
                    categories.Add(_categoryService.Entities.Single(c => c.CategoryId == pc.CatId));
                }

                foreach (var pt in post.Tags)
                {
                    tags.Add(_tagService.Entities.Single(t => t.TagId == pt.TagId));
                }
                post.PostContent = GetHtmlImageUrlList(post.PostContent);
                image = _imageService.Entities.SingleOrDefault(i => i.PostImageId == post.PostId);
                one.Categories = categories;
                one.Post = post;
                one.Tags = tags;
                if (image == null)
                {
                    image = _imageService.Entities.Single(i => i.ImageName == "moren.jpg");
                }
                one.Image = image;
                onePosts.Add(one);
            }
            var briefPostViewModel = new PagingBriefPostViewModel()
            {
                OnePosts = onePosts,
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = posts.Count()

                }
            };
            if (_postService.Entities.Count() < 5)
            {
                ViewData["RecentPost"] = _postService.Entities.OrderByDescending(p => p.PostAddedTime);
            }
            else
            {
                ViewData["RecentPost"] = _postService.Entities.OrderByDescending(p => p.PostAddedTime).Take(5);
            }
            ViewData["Category"] = _categoryService.Entities;
            return View("PostBrief", briefPostViewModel);
        }

        public string  GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            Regex regh = new Regex(@"<h1>.*</h1>");
            Regex rega = new Regex(@"<a .*>.*</a>");
            sHtmlText = regh.Replace(sHtmlText, "").Trim();
            sHtmlText = rega.Replace(sHtmlText, "").Trim();

            return regImg.Replace(sHtmlText, "").Replace("<p>","").Replace("</p>","").Replace(@"<br />","");
        }
    }
}
