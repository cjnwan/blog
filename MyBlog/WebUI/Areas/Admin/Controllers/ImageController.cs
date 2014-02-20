using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Domain.Model;
using Newtonsoft.Json;
using Service;

namespace WebUI.Areas.Admin.Controllers
{
    public class ImageController : Controller
    {
        
        private readonly IImageService _imageService;
        private static string imgdesc;
        private static string issmall;
        private int PageSize = 13;
        
        public ImageController(IImageService image)
        {
            _imageService = image;
           
        }

        [Authorize]
        public ActionResult Index()
        {
            var model = _imageService.Entities.Where(i => i.PostImageId == 0).OrderBy(i => i.ImageId).Skip((1 - 1) * PageSize)
                                    .Take(PageSize);
            return View(model);
        }

        [HttpGet]
        public string  GetImages(int page)
        {
            var model = _imageService.Entities.Where(i => i.PostImageId == 0).OrderBy(i => i.ImageId).Skip((page - 1) * PageSize)
                                     .Take(PageSize);

            string txt=JsonConvert.SerializeObject(model);

            return txt;
        }

        public ActionResult Upload(HttpPostedFileBase Filedata)
        {
            
            if (Filedata == null ||string.IsNullOrEmpty(Filedata.FileName) ||Filedata.ContentLength == 0)
            {
                return this.HttpNotFound();
            }
            
            string filename = System.IO.Path.GetFileName(Filedata.FileName);

            string virtualPath =string.Format("~/Images/gallery/{0}", filename);
            
            string path = this.Server.MapPath(virtualPath);

            if (!Directory.Exists(this.Server.MapPath("~/Images/gallery/")))
            {

                Directory.CreateDirectory(this.Server.MapPath("~/Images/gallery/"));
            }

            Filedata.SaveAs(path);

            _imageService.Insert(new Image() { ImageName = filename, ImageUrl = path, ImageDescription = imgdesc,IsSmall =Convert.ToBoolean(issmall)});

            return this.Json(new { });
        }

        [HttpPost]
        public ActionResult SetImageDesc(string desc,string small)
        {
            imgdesc = desc;
            issmall = small;
            return Json(new { Content = "设置成功" });
        }

    }
}
