using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Model;
using Service;

namespace WebUI.Areas.Admin.Controllers
{
    public class TagController : Controller
    {
        

        private readonly ITagService _tagService;

        public TagController(ITagService service)
        {
            _tagService = service;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(_tagService.Entities);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tag tag)
        {
           if (ModelState.IsValid)
           {
               _tagService.Insert(tag);
           }
            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            return View(_tagService.Entities.Single(t => t.TagId==id));
        }

        [HttpPost]
        public ActionResult Delete(int id, bool issave = true)
        {
            Tag tag = _tagService.Entities.Single(t=>t.TagId == id);

            _tagService.Delete(tag);
            return RedirectToAction("index");
        }

        public ActionResult Details(int id)
        {
            return View(_tagService.Entities.Single(t => t.TagId == id));
        }

        public ActionResult Edit(int id)
        {
            return View(_tagService.Entities.Single(t => t.TagId == id));
        }

        [HttpPost]
        public ActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                Tag _tag = new Tag();
                _tag.TagId = tag.TagId;
                _tag.TagName = tag.TagName;
                _tag.TagSlug = tag.TagSlug;
                _tagService.Update(_tag);
                return RedirectToAction("index");
            }
            return View(tag);
        }
    }
}
