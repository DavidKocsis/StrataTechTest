using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrataServices.Contracts;

namespace StrateTest
{
    public static class Discounts
    {
        public static Dictionary<CustomerType, int> DiscountAmounts => new Dictionary<CustomerType, int>
        {
            { CustomerType.Gold, 3},
            { CustomerType.Silver, 2},
            { CustomerType.Standard, 0},
        };
    }
}
