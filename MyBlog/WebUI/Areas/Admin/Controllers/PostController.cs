using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Domain.Model;
using Service;
using WebGrease.Css.Extensions;
using WebUI.Common;
using WebUI.Dto;
using WebUI.ViewModel;
using Webdiyer.WebControls.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;
        private readonly IImageService _imageService;       
        private static string imgdesc;
        

        private readonly List<Category> _listCategory;
        private readonly List<Tag> _listTag; 


        private const int PageSize = 4;
 
        public PostController(IPostService service,ICategoryService category,ITagService tag,IUserService user,IImageService image)
        {
            _postService = service;
            _categoryService = category;
            _tagService = tag;
            _userService = user;
            _imageService = image;
            _listCategory = _categoryService.Entities.ToList();
            _listTag = _tagService.Entities.ToList();

        }

        [Authorize]
        public ActionResult Index(int page = 1)
        {
            AutoMapper.Mapper.CreateMap<Post, DtoPost>()
                         .ForMember(p => p.PostAddedTime, opt => opt.MapFrom(src => src.PostAddedTime.ToShortDateString()))
                         .ForMember(p => p.PostEditedTime, opt => opt.MapFrom(src => src.PostEditedTime.ToShortDateString()));

            var listpost = new List<DtoPost>();

            var model = _postService.Entities.OrderBy(p => p.PostId).Skip((page - 1) * PageSize)
                                    .Take(PageSize);            
            model.ForEach(p=>
                {
                    DtoPost dtoPost = Mapper.Map<Post ,DtoPost>(p);
                    listpost.Add(dtoPost);
                });

            var viewModel = new PagingPostViewModel()
                {
                    Post = listpost,
                    PagingInfo = new PagingInfo()
                        {
                            CurrentPage = page,
                            ItemsPerPage = PageSize,
                            TotalItems = _postService.Entities.Count()

                        }
                };
            return View(viewModel);
        }

        public ActionResult Create()
        {
            var perPost = new PerPost();

            perPost.Tags = _tagService.Entities.ToList();
            perPost.Categories = _categoryService.Entities.ToList();

            return View(perPost);
        }

        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(bool issave=true)
        {          
            var post = new Post();

            post.PostTitle = Request.Unvalidated.Form["Post.PostTitle"];
            post.PostContent = Request.Unvalidated.Form["Post.PostContent"];

            post.PostContent =post.PostContent;
            post.PostAddedTime = DateTime.Now;
            post.PostEditedTime = DateTime.Now;

            string isprivate = Request.Unvalidated.Form["Post.IsPrivate"];
            post.IsPrivate = isprivate.Contains("true") ? true : false;
                
            GetCategories(post);
            GetTags(post);

            post.User=_userService.Entities.FirstOrDefault().UserId;
            _postService.Insert(post);

            return RedirectToAction("index");
        }    

        [HttpPost]
        public ActionResult UploadPicture(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string result = "";
            if (upload != null && upload.ContentLength > 0)
            {
                
                upload.SaveAs(Server.MapPath("~/Images/" + upload.FileName));


                var imageUrl = Url.Content("~/Images/" + upload.FileName);

                var vMessage = string.Empty;

                result = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + imageUrl + "\", \"" + vMessage + "\");</script></body></html>";
            }
            return Content(result);
        }

        public ActionResult Details(int id)
        {
            Post post=_postService.Entities.Single(p => p.PostId == id);

            List<Category> categories = new List<Category>();
            foreach (PostCat c in post.Categories)
            {
                foreach (var category in _listCategory)
                {
                    if (c.CatId == category.CategoryId)
                    {
                        categories.Add(category);
                    }
                }
            }

            List<Tag> tags = new List<Tag>();
            foreach (PostTag t in post.Tags)
            {
                foreach (var tag in _listTag)
                {
                    if (t.TagId == tag.TagId)
                    {
                        tags.Add(tag);
                    }
                }
            }

            ViewData["Cat"] = categories;
            ViewData["Tag"] = tags;
            return View(post);
        }

        public ActionResult Delete(int id)
        {
            return View(_postService.Entities.Single(p=>p.PostId==id));
        }

        [HttpPost]
        public ActionResult Delete(int id,bool isdelete=true)
        {
            _postService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Post post = _postService.Entities.Single(p => p.PostId == id);

            List<Category> categories = new List<Category>();
            foreach (PostCat c in post.Categories)
            {
                foreach (var category in _listCategory)
                {
                    if (c.CatId == category.CategoryId)
                    {
                        categories.Add(category);
                    }
                }
            }

            List<Tag> tags = new List<Tag>();
            foreach (PostTag t in post.Tags)
            {
                foreach (var tag in _listTag)
                {
                    if (t.TagId == tag.TagId)
                    {
                        tags.Add(tag);
                    }
                }
            }

            ViewData["Cat"] = categories;
            ViewData["Tag"] = tags;
            return View(post); 
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id ,bool isedit=true)
        {
            
            _postService.Delete(id);

            var post = new Post();

            post.PostTitle = Request.Unvalidated.Form["PostTitle"];
            post.PostContent = Request.Unvalidated.Form["PostContent"];

            post.PostAddedTime = DateTime.Now;
            post.PostEditedTime = DateTime.Now;

            string isprivate = Request.Unvalidated.Form["IsPrivate"];
            post.IsPrivate = isprivate.Contains("true") ? true : false;

            GetCategories(post);
            GetTags(post);

            _postService.Insert(post);

            return RedirectToAction("index");
           
        }

        [HttpGet]
        public ActionResult Upload(int id)
        {
            return View(_postService.Entities.Single(p=>p.PostId==id));
        }

        

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase Filedata,int id)
        {
            
           
            if (Filedata == null ||
                string.IsNullOrEmpty(Filedata.FileName) ||
                Filedata.ContentLength == 0)
            {
                return this.HttpNotFound();
            }

            
            string filename = System.IO.Path.GetFileName(Filedata.FileName);
            string virtualPath =
                string.Format("~/Images/blogpic/{0}", filename);
            
            string path = this.Server.MapPath(virtualPath);

            if (!Directory.Exists(this.Server.MapPath("~/Images/blogpic/")))
            {
                
                    Directory.CreateDirectory(this.Server.MapPath("~/Images/blogpic/"));
            }

            Filedata.SaveAs(path);
            

            _imageService.Insert(new Image() { PostImageId = id, ImageUrl = path, ImageName = filename ,ImageDescription =imgdesc});

            return this.Json(new { });
        }

        [HttpPost]
        public ActionResult SetImageDesc(string desc)
        {
            imgdesc = desc;
            return Json(new{Content="设置成功"});
        }
        
        private List<Category> GetCategories(Post post)
        {
            List<Category> categories = new List<Category>();

            foreach (var c in _listCategory)
            {
                string ca = Request.Unvalidated.Form[c.CategoryName];
                if (!string.IsNullOrEmpty(ca) && ca.Contains("true"))
                {
                    post.Categories.Add(new PostCat() { PostId = post.PostId, CatId = c.CategoryId });
                    
                }
            }


            return categories;
        }

        private List<Tag> GetTags(Post post)
        {
            var tags = new List<Tag>();


            foreach (var t in _listTag)
            {
                string ta = Request.Unvalidated.Form[t.TagName];
                if (!string.IsNullOrEmpty(ta) && ta.Contains("true"))
                {
                    post.Tags.Add(new PostTag() { PostId = post.PostId, TagId = t.TagId });

                }
            }


            return tags;
        }
      
    }
}
