using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Model;
using Service;
using Webdiyer.WebControls.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
       
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service)
        {
            this._categoryService = service;
        }

        [Authorize]
        public ActionResult Index()
        {
           
            return View(_categoryService.Entities.Distinct());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Insert(category);
            }
            return RedirectToAction("index");
        }

        public ActionResult Details(int id)
        {
            return View(_categoryService.Entities.Single(c => c.CategoryId == id));
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                Category _category = new Category();

                _category.CategoryId = category.CategoryId;
                _category.CategoryName = category.CategoryName;
                _category.CategorySlug = category.CategorySlug;

                _categoryService.Update(_category);

                return RedirectToAction("index");
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(_categoryService.Entities.Single(c => c.CategoryId == id));  
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(_categoryService.Entities.Single(c => c.CategoryId == id));
        }

        [HttpPost]
        public ActionResult Delete(int id,bool issave=true)
        {
            Category category=_categoryService.Entities.Single(c => c.CategoryId ==id );
            
            _categoryService.Delete(category);
           return RedirectToAction("index");
        }
    }
}
