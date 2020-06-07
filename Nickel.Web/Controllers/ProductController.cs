using Nickel.BLL;
using Nickel.Interface;
using Nickel.Web.Models;
using Nickel.Web.Repository;
using Nickel.Web.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nickel.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductRepo productRepo;

        public ProductController()
        {
            productRepo = new ProductLogic();
        }

        // GET: Product
        public ActionResult Index()
        {
            //List<ProductDTO> products = new List<ProductDTO>
            //{
            //    new ProductDTO(1, "Product 5", 5290.5, "Hmmm", "lllll")
            //};
            //return Json(products, JsonRequestBehavior.AllowGet);

            var products = productRepo?.Find(null);
            foreach (var p in products)
            {
                p.Photo = Url.Action("GetPhoto", "Product", new { id = p.Id }, Request.Url.Scheme);
                p.PhotoByte = null;
                p.PhotoMime = null;
            }

            if (Request.AcceptTypes.Contains("application/json"))
                return Json(products, JsonRequestBehavior.AllowGet);

            return View(new IndexViewModel(
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

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            ProductDTO productDTO = productRepo.Get(id ?? 0);
            if (productDTO == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ProductModel productModel = new ProductModel(
                    productDTO.Id,
                    productDTO.Name,
                    productDTO.Price,
                    productDTO.Description,
                    Url.Action("GetPhoto", "Product", new { id = productDTO.Id })
                    //$"~/images/Photos/{productDTO.Photo}"
                );
            var sameProducts = productRepo.FindSame(productModel.Id).Select(
                x => new ProductModel
                (
                    x.Id,
                    x.Name,
                    x.Price,
                    x.Description,
                    Url.Action("GetPhoto", "Product", new { id = x.Id })
                    //$"~/images/Photos/{x.Photo}"
                )
            );


            if (Request.AcceptTypes.Contains("application/json"))
                return Json(productModel, JsonRequestBehavior.AllowGet);

            return View(new DetailsViewModel(productModel, sameProducts));
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    byte[] photo = null;
                    string mime = null;
                    if (productModel.Picture != null && productModel.Picture.ContentLength > 0)
                    {
                        photo = new byte[productModel.Picture.ContentLength];
                        //var filename = Guid.NewGuid().ToString() + Path.GetExtension(productModel.Picture.FileName);
                        //productModel.Picture.SaveAs(Server.MapPath("~/images/Photos/" + filename));
                        //productModel.Photo = filename;
                        productModel.Picture.InputStream.Read(photo, 0, photo.Length);
                        mime = productModel.Picture.ContentType;
                    }

                    var productDTO = new ProductDTO(
                        productModel.Id,
                        productModel.Name,
                        productModel.Price,
                        productModel.Description,
                        productModel.Photo, 
                        photo, 
                        mime
                    );
                    productRepo.Add(productDTO);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(productModel);
                }
            }
            return View();
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            var productDTO = productRepo.Get(id ?? 0);
            if (productDTO == null)
            {
                return RedirectToAction("Index");
            }
            var productModel = new ProductModel(
                productDTO.Id,
                productDTO.Name,
                productDTO.Price,
                productDTO.Description,
                Url.Action("GetPhoto", "Product", new { id = productDTO.Id })
                //!string.IsNullOrEmpty(productDTO.Photo) ? $"~/images/Photos/{productDTO.Photo}" : null
            );

            return View(productModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    byte[] photo = null;
                    string mime = null;

                    if (productModel.Picture != null && productModel.Picture.ContentLength > 0)
                    {
                        //if (!string.IsNullOrEmpty(productModel.Photo))
                        //{
                        //    var oldPhoto = Server.MapPath("~/images/Photos/" + productModel.Photo);
                        //    if (System.IO.File.Exists(oldPhoto))
                        //    {
                        //        System.IO.File.Delete(oldPhoto);
                        //    }
                        //}
                        //var filename = Guid.NewGuid().ToString() + Path.GetExtension(productModel.Picture.FileName);
                        //productModel.Picture.SaveAs(Server.MapPath("~/images/Photos/" + filename));
                        //productModel.Photo = filename;
                        photo = new byte[productModel.Picture.ContentLength];
                        productModel.Picture.InputStream.Read(photo, 0, photo.Length);
                        mime = productModel.Picture.ContentType;
                    }


                    var productDTO = new ProductDTO(
                        productModel.Id,
                        productModel.Name,
                        productModel.Price,
                        productModel.Description,
                        productModel.Photo, 
                        photo, 
                        mime
//                        !string.IsNullOrEmpty(productDTO.Photo) ? $"~/images/Photos/{productDTO.Photo}" : null
                    );
                    productRepo.Set(id, productDTO);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(productModel);
                }
            }
            return View(productModel);
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                var productDTO = productRepo.Get(id ?? 0);
                if (productDTO == null)
                {
                    return RedirectToAction("Index");
                }

                //if (!string.IsNullOrEmpty(productDTO.Photo))
                //{
                //    var oldPhoto = Server.MapPath(productDTO.Photo);
                //    if (System.IO.File.Exists(oldPhoto))
                //    {
                //        System.IO.File.Delete(oldPhoto);
                //    }
                //}

                productRepo.Remove(productDTO.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            productRepo?.Dispose();
            base.Dispose(disposing);
        }

        public FileContentResult GetPhoto (int id)
        {
            var productDTO = productRepo.Get(id);
            if (productDTO != null && productDTO.PhotoByte != null)
            {
                return File(productDTO.PhotoByte, productDTO.PhotoMime);
            }

            return null;
        }
    }
}
