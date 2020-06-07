using System;
using System.Collections.Generic;
using System.Text;

namespace Nickel.Mobile.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        public string ShortDescription
        {
            get => !string.IsNullOrEmpty(Description) && Description.Length > 25
                ? Description.Substring(0, 25) + "..." : Description;
        }

        public string PriceWithCurrency { get => "XAF " + string.Format("{0:N}", Price); }

        public ProductModel() { }

        public ProductModel(int id, string name, double price, string description, string photo)
        {
            Id = id;
            Photo = photo;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
