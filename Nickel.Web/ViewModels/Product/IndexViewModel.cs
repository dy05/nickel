using Nickel.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nickel.Web.ViewModels.Product
{
    public class IndexViewModel
    {
        public IEnumerable<ProductModel> Products { get; set; }

        public IndexViewModel(IEnumerable<ProductModel> products)
        {
            Products = products;
        }
    }
}