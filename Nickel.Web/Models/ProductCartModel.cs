using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nickel.Web.Models
{
    public class ProductCartModel
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
        public double Amount { get => (Product?.Price ?? 0) * Quantity; }
        public string AmountWithCurrency { get => "XAF " + string.Format("{0:N}", Amount); }

        public ProductCartModel() { }
        public ProductCartModel(ProductModel product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        /*
        public int Quantity
        {
            get
            {
                return Quantity;
            }

            set
            {
                Quantity = value < 1 ? 1 : value;
                Amount = Quantity * Product.Price;
            }
        }
        //public double Amount { get =>  => Product?.Price ?? 0 * Quantity; private set; }
        public double Amount { get; private set; }
        */
    }
}