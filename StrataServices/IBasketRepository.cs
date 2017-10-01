using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrataServices.Contracts;

namespace StrataServices
{
    public interface IBasketRepository
    {
        void Add(ShoppingCartItem request);

        List<ShoppingCartItem> Get(string userName);
    }
}
