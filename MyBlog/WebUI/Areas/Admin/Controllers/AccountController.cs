using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Domain.Model;
using Service;

namespace WebUI.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService user)
        {
            _userService = user;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user =_userService.Entities.SingleOrDefault(u=>model.Password==u.Password && model.UserName==u.UserName);
                if (user != null)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        user.UserName,
                        DateTime.Now,
                        DateTime.Now.Add(FormsAuthentication.Timeout),
                            false,
                            "admin"                
                        );
                    HttpCookie cookie = new HttpCookie(
                        FormsAuthentication.FormsCookieName,
                        FormsAuthentication.Encrypt(ticket));
                    Response.Cookies.Add(cookie);

                    if (!String.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
                    else return RedirectToAction("Index", "Home");
                }
                //else if (model.UserName == "cjn")
                //{
                //    _userService.Insert(new User()
                //        {
                //            DisplayName = "小跳蚤",
                //            Password = "123456",
                //            UserEmail = "1278671543@qq.com",
                //            UserName = "cjn",
                //            LastLoginDate = DateTime.Now
                //        });
                //}
                else
                {
                    ModelState.AddModelError("", "用户名或密码不正确！"); 
                }
                    
            }
            return View(model);                      

        }


    }
}
