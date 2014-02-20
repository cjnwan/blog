using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Model;
using Service;

namespace WebUI.Controllers
{
    public class GalleryController : Controller
    {
        private IImageService _imageService;

        public GalleryController(IImageService image)
        {
            _imageService = image;
        }

        public ActionResult Index()     
        {
          
            return View(_imageService.Entities.Where(i => i.PostImageId == 0));
        }

    }
}
