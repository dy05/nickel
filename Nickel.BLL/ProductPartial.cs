using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickel.BLL
{
    public partial class Product
    {
        public Product() {}

        public Product(int id, string name, string description, double price, byte[] photo, string photoMime)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Photo = photo;
            PhotoMime = photoMime;
        }
    }
}
