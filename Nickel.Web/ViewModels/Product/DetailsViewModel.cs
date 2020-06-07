using Nickel.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nickel.Web.ViewModels.Product
{
    public class DetailsViewModel
    {
        public IEnumerable<ProductModel> SameProducts { get; set; }

        public ProductModel Product { get; set; }

        public DetailsViewModel(ProductModel product, IEnumerable<ProductModel> sameProducts)
        {
            Product = product;
            SameProducts = sameProducts;
        }
    }
}