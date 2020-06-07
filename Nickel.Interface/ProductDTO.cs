using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickel.Interface
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public byte[] PhotoByte { get; set; }
        public string PhotoMime { get; set; }

        public ProductDTO() { }

        public ProductDTO(int id, string name, double price, string description, string photo)
        {
            Id = id;
            Photo = photo;
            Name = name;
            Description = description;
            Price = price;
        }

        public ProductDTO(int id, string name, double price, string description, string photo, byte[] photoByte, string photoMime) : 
            this(id, name, price, description, photo)
        {
            PhotoByte = photoByte;
            PhotoMime = photoMime;
        }
    }
}
