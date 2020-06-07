using Nickel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickel.BLL
{
    public class ProductLogic : IProductRepo
    {
        private NickelDBEntities db;
        public ProductLogic()
        {
            db = new NickelDBEntities();
        }

        public ProductDTO Add(ProductDTO productDTO)
        {
            if (productDTO != null)
            {
                Product product = new Product(
                        productDTO.Id, 
                        productDTO.Name, 
                        productDTO.Description, 
                        productDTO.Price, 
                        productDTO.PhotoByte, 
                        productDTO.PhotoMime
                    );
                product = db.Products.Add(product);
                db.SaveChanges();

                return new ProductDTO(
                        product.Id, 
                        product.Name, 
                        product.Price,
                        product.Description,
                        null, 
                        product.Photo, 
                        product.PhotoMime
                    );
            }

            return null;
        }

        public void Dispose()
        {
            db?.Dispose();
        }

        private IEnumerable<ProductDTO> GetProductDTOs(IEnumerable<Product> products)
        {
            return products.Select(
                    x => new ProductDTO(
                        x.Id,
                        x.Name,
                        x.Price,
                        x.Description,
                        x.PhotoMime, 
                        x.Photo,
                        x.PhotoMime
                    )
                ).ToArray();
        }

        public IEnumerable<ProductDTO> Find(string value)
        {
            value = value ?? "";
            var products = db.Products.Where(
                    x => x.Name.Contains(value) || (x.Description != null && x.Description.Contains(value))
                );

            return GetProductDTOs(products);
        }

        public IEnumerable<ProductDTO> FindSame(int id)
        {
            var products = db.Products.Where(
                    x => x.Id % 2 == id % 2
                );

            return GetProductDTOs(products);
        }

        public IEnumerable<ProductDTO> FindTop(int max = 4)
        {
            var products = db.Products.AsEnumerable().OrderBy(
                    x => Guid.NewGuid()
                ).Take(max);

            return GetProductDTOs(products);
        }

        public ProductDTO Get(int id)
        {
            var product = db.Products.Find(id);

            if (product != null)
            {
                return new ProductDTO(
                        product.Id,
                        product.Name,
                        product.Price,
                        product.Description,
                        product.PhotoMime, 
                        product.Photo,
                        product.PhotoMime
                    );
            }

            return null;
        }

        public void Remove(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }

        public void Set(int id, ProductDTO productDTO)
        {
            //var product = db.Products.Find(id);
            var product = db.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                var newProduct = new Product(
                        productDTO.Id, 
                        productDTO.Name, 
                        productDTO.Description, 
                        productDTO.Price, 
                        productDTO.PhotoByte ?? product.Photo, 
                        productDTO.PhotoMime ?? product.PhotoMime
                    );
                db.Entry(newProduct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
