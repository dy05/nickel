using Nickel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nickel.Web.Repository
{
    public class ProductRepo : IProductRepo
    {
        private static int maxId = 0;

        public static List<ProductDTO> products = new List<ProductDTO> {
            new ProductDTO(1, "Road set", 1250.5,  "Coucher de soleil en route", "1.png"),
            new ProductDTO(3, "Sun", 3250.5, "Soleil a Manengouba", "2.png"),
        };

        public ProductRepo()
        {
            if (maxId == 0)
            {
                maxId = products.Max(x => x.Id);
            }
        }

        public IEnumerable<ProductDTO> Find(string value)
        {
            return products;
        }

        public IEnumerable<ProductDTO> FindTop(int max = 4)
        {
            return products.OrderBy(x => Guid.NewGuid()).Take(max);
        }

        public IEnumerable<ProductDTO> FindSame(int id)
        {
            return products.Where(x => x.Id % 2 == (id % 2)).ToArray();
        }

        public ProductDTO Get(int id)
        {
            return products.FirstOrDefault(x => x.Id == id);
        }

        public ProductDTO Add(ProductDTO productDTO)
        {
            maxId++;
            productDTO.Id = maxId;
            products.Add(productDTO);

            return productDTO;
        }

        public bool Set(int id, ProductDTO productDTO)
        {
            return false;
        }

        public bool Remove(int? id)
        {
            if (id == null)
            {
                products = new List<ProductDTO>() { };
                return true;
            }
            //if (products.ElementAt(id))
            //{
            //    products.RemoveAt(id);
            //    return true;
            //}
            return false;
        }

        void IProductRepo.Set(int id, ProductDTO productDTO)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}