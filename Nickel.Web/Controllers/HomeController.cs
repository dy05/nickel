using Nickel.BLL;
using Nickel.Interface;
using Nickel.Web.Models;
using Nickel.Web.Repository;
using Nickel.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nickel.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductRepo productRepo;

        public HomeController()
        {
            productRepo = new ProductLogic();
        }

        // GET: Home
        public ActionResult Index()
        {
            var products = productRepo?.FindTop();
            foreach (var p in products)
            {
                p.Photo = Url.Action("GetPhoto", "Product", new { id = p.Id }, Request.Url.Scheme);
                p.PhotoByte = null;
                p.PhotoMime = null;
            }
            if (Request.AcceptTypes.Contains("application/json"))
                return Json(products, JsonRequestBehavior.AllowGet);

            return View(
                new IndexViewModel(
                    products.Select(
                        x => new ProductModel
                        (
                            x.Id,
                            x.Name,
                            x.Price,
                            x.Description,
                            Url.Action("GetPhoto", "Product", new { id = x.Id })
                            //$"~/images/Photos/{x.Photo}"
                        )
                    )
                )
            );
        }
    }
}