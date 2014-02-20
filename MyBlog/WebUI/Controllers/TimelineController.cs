using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;

namespace WebUI.Controllers
{
    public class TimelineController : Controller
    {
        private IPostService _postService;

        public TimelineController(IPostService post)
        {
            _postService = post;
        }

        public ActionResult Index()
        {
            return View(_postService.Entities);
        }

    }
}
