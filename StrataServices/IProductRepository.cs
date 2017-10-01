using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrataServices.Contracts;

namespace StrataServices
{
    interface IProductRepository
    {
        Product Get(string productId);
    }
}
