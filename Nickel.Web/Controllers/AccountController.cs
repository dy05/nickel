using Nickel.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nickel.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
 
        public ActionResult Edit(string id)
        {
            string name = Request.RequestContext.HttpContext.User?.Identity?.Name;
            var accountModel = new AccountModel();
            return View("Index", accountModel);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountModel accountModel)
        {
            // Pour faire passer la validation
            accountModel.Name = "x";
            accountModel.ConfirmPassword = accountModel.Password;
            if (ModelState.IsValid)
            {
                try
                {
//                    bool v = System.Web.Security.FormsAuthentication.Authenticate(accountModel.Email, accountModel.Password);
                }
                catch
                {
                    //
                }
            }
            return View("Index", accountModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AccountModel accountModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //
                }
                catch
                {
                    //
                }
            }
            return View("Index", accountModel);
        }

        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}