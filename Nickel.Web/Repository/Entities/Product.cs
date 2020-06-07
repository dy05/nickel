using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nickel.Web.Repository.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Photo { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public Product()
        {
        }

        public Product(int id, string name, double price, string description, string photo)
        {
            Id = id;
            Photo = photo;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}