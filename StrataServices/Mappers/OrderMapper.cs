using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrataServices.Contracts;

namespace StrataServices.Mappers
{
    public static class OrderMapper
    {
        public static Order Map(List<ShoppingCartItem> shoppingCartItems, int discountPercentage = 0)
        {
            var guid = Guid.NewGuid();
            var orderlines = shoppingCartItems.Select(item => new OrderLine
                {
                    OrderId = guid,
                    ProductCode = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                })
                .ToList();

            var totalAmount = shoppingCartItems.Sum(item => item.UnitPrice * item.Quantity);
            var discountAmount = totalAmount * discountPercentage / 100;

            return new Order
            {
                Amount = totalAmount - discountAmount ,
                Customer = shoppingCartItems.FirstOrDefault().Customer,
                Date = DateTime.Now,
                OrderId = guid,
                OrderLines = orderlines
            };
        }
    }
}
