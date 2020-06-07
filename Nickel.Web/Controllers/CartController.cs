using Nickel.BLL;
using Nickel.Interface;
using Nickel.Web.Models;
using Nickel.Web.Repository;
using Nickel.Web.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nickel.Web.Controllers
{
    public class CartController : Controller
    {

        private IProductRepo productRepo;

        public CartController()
        {
            productRepo = new ProductLogic();
        }

        // GET: Cart
        public ActionResult Index()
        {
            IEnumerable<ProductCartModel> productsCart = GetProductsCart();

            return View(new IndexViewModel(productsCart));
        }

        private List<ProductCartModel> GetProductsCart()
        {
            List<ProductCartModel> productsCart = new List<ProductCartModel>();
            if (Session["Cart"] != null)
                productsCart = Session["Cart"] as List<ProductCartModel>;
            return productsCart;
        }

        private List<ProductCartModel> AddProductsCart(ProductCartModel productCartModel)
        {
            List<ProductCartModel> productsCart = GetProductsCart();
            bool exist = false;

            for (int i = 0; i < productsCart.Count; i++)
            {
                if (productsCart[i].Product.Id == productCartModel.Product.Id)
                {
                    productsCart[i].Quantity += productCartModel.Quantity;
                    exist = true;
                    break;
                }
            }

            if (!exist)
            {
                productsCart.Add(productCartModel);
            }

            Session["Cart"] = productsCart;
            return productsCart;
        }

        /*
        private List<ProductCartModel> SetProductsCart(ProductCartModel productCartModel)
        {
            List<ProductCartModel> productsCart = GetProductsCart();
            for (int i = 0; i<productsCart.Count; i++)
            {
                if (productsCart[i].Product.Id == productCartModel.Product.Id)
                {
//                    productsCart[i] = productCartModel;
                    productsCart[i].Quantity += productCartModel.Quantity;
                    if (productsCart[i].Quantity < 1)
                    {
                        productsCart.RemoveAt(i);
                    }
                    break;
                }
            }

            Session["Cart"] = productsCart;
            return productsCart;
        }
        */

        private List<ProductCartModel> SetProductsCart(int id, int quantity)
        {
            List<ProductCartModel> productsCart = GetProductsCart();
            for (int i = 0; i < productsCart.Count; i++)
            {
                if (productsCart[i].Product.Id == id)
                {
                    productsCart[i].Quantity = quantity;
                    if (productsCart[i].Quantity < 1)
                    {
                        productsCart.RemoveAt(i);
                    }
                    break;
                }
            }

            Session["Cart"] = productsCart;
            return productsCart;
        }


        /*
        private List<ProductCartModel> RemoveProductsCart(ProductCartModel productCartModel)
        {
            List<ProductCartModel> productsCart = GetProductsCart();
            for (int i = 0; i<productsCart.Count; i++)
            {
                if (productsCart[i].Product.Id == productCartModel.Product.Id)
                {
                    productsCart[i] = productCartModel;
                    productsCart[i].Quantity -= productCartModel.Quantity;
                    if (productsCart[i].Quantity < 0)
                    {
                        productsCart.RemoveAt(i);
                    }
                }
            }

            Session["Cart"] = productsCart;
            return productsCart;
        }
        */

        private List<ProductCartModel> RemoveProductsCart(int id)
        {
            List<ProductCartModel> productsCart = GetProductsCart();

            var p = productsCart.FirstOrDefault(x => x.Product.Id == id);
            if (p != null)
            {
                productsCart.Remove(p);
            }
            /*
            for (int i = 0; i<productsCart.Count; i++)
            {
                if (productsCart[i].Product.Id == id)
                {
                    productsCart.RemoveAt(i);
                    break;
                }
            }
            */

            Session["Cart"] = productsCart;
            return productsCart;
        }


        public ActionResult Create(int id, int quantity, string returnUrl)
        {
            var productDTO = productRepo.Get(id);
            var productModel = new ProductModel(
                    productDTO.Id,
                    productDTO.Name,
                    productDTO.Price,
                    productDTO.Description, 
                    Url.Action("GetPhoto", "Product", new { id = productDTO.Id })
                    //$"~/images/Photos/{productDTO.Photo}"
                );


            AddProductsCart(new ProductCartModel(productModel, quantity));

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id, int quantity, string returnUrl)
        {
            SetProductsCart(id, quantity);
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id, string returnUrl)
        {
            RemoveProductsCart(id);
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index");
        }
    }
}
