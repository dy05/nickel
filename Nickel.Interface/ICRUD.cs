using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickel.Interface
{
    public interface ICRUD<T>
    {
        IEnumerable<T> Find(string value);

        T Get(int id);
    }
}
