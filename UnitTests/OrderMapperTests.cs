using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrataServices.Contracts;
using StrataServices.Mappers;
using Xunit;

namespace UnitTests
{
    public class OrderMapperTests
    {
        private const string CustomerName = "testName";

        [Fact]
        public void ShoppingCartItemsAreMappedToOrder()
        {
            var shoppingCartItems = new List<ShoppingCartItem>
            {
                new ShoppingCartItem
                {
                  Customer = CustomerName,
                  ProductId = "test1",
                  Quantity = 1,
                  UnitPrice = 15.50m
                },
                new ShoppingCartItem()
                {
                    Customer = CustomerName,
                    ProductId = "test2",
                    Quantity = 2,
                    UnitPrice = 25
                }
            };
            var result = OrderMapper.Map(shoppingCartItems);
            Assert.Equal(result.Amount,65.50m);
            Assert.Equal(CustomerName,result.Customer);
            Assert.Equal(result.OrderLines.Count(),2);
            Assert.Equal(result.OrderId, result.OrderLines[0].OrderId);
            Assert.Equal(result.OrderId, result.OrderLines[1].OrderId);
        }

        [Fact]
        public void OrderMapperAppliesCorrectDiscount()
        {
            var shoppingcartItems = new List<ShoppingCartItem>
            {
                new ShoppingCartItem()
                {
                    Customer = CustomerName,
                    ProductId = "test2",
                    Quantity = 1,
                    UnitPrice = 50
                }
            };

            var result = OrderMapper.Map(shoppingcartItems, 50);

            Assert.Equal(result.Amount, 25);
        }
    }
}
