using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Model;
using Service;

namespace WebUI.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService service,IRoleService role)
        {
            _userService = service;
            _roleService = role;
        }

        [Authorize]
        public ActionResult Index()
        {

            return View(_userService.Entities.FirstOrDefault());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                User admin = new User();
                admin.UserName = user.UserName;
                admin.UserEmail = user.UserEmail;
                admin.LastLoginDate = DateTime.Now;
                admin.DisplayName = user.DisplayName;
                admin.Password = user.Password;

                admin.Role = _roleService.Entities.Single(r => r.RoleName == "admin").RoleId;
                _userService.Insert(admin);
            }
          return  RedirectToAction("Index");
        }
    }
}
