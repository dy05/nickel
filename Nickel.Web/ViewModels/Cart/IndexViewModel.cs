using Nickel.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nickel.Web.ViewModels.Cart
{
    public class IndexViewModel
    {
        public IEnumerable<ProductCartModel> ProductsCart { get; set; }
        public IndexViewModel(IEnumerable<ProductCartModel> productsCart)
        {
            ProductsCart = productsCart;
        }

        public double TotalQuantity { get => ProductsCart?.Sum(x => x.Quantity) ?? 0; }
        public double TotalPrice { get => ProductsCart?.Sum(x => x.Product.Price) ?? 0; }
        public double TotalAmount { get => ProductsCart?.Sum(x => x.Amount) ?? 0; }
        public string TotalAmountWithCurrency { get => "XAF " + string.Format("{0:N}", TotalAmount); }
        public string TotalQuantityFormat { get => string.Format("{0:N}", TotalQuantity); }
        public string TotalPriceWithCurrency { get => string.Format("{0:N}", TotalPrice); }
    }
}