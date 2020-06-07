using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nickel.Web.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Photo { get; set; }

        [Required(ErrorMessage = "Le champ Name requis")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Le champ prix requis")]
        public double Price { get; set; }

        public string ShortDescription
        {
            get => !string.IsNullOrEmpty(Description) && Description.Length > 25 
                ? Description.Substring(0, 25) + "..." : Description;
        }

        public string PriceWithCurrency { get => "XAF " + string.Format("{0:N}", Price); }

        public HttpPostedFileBase Picture { get; set; }

        public ProductModel() {}

        public ProductModel(int id, string name, double price, string description, string photo)
        {
            Id = id;
            Photo = photo;
            Name = name;
            Description = description;
            Price = price;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}