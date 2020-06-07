using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickel.Interface
{
    public interface IProductRepo : ICRUD<ProductDTO>, IDisposable
    {
        ProductDTO Add(ProductDTO productDTO);

        IEnumerable<ProductDTO> FindSame(int id);

        IEnumerable<ProductDTO> FindTop(int max = 4);

        void Set(int id, ProductDTO productDTO);

        void Remove(int id);
    }
}
