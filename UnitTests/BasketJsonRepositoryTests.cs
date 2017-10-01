using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrataServices.Contracts;
using StrataServices.JsonRepos;
using Xunit;

namespace UnitTests
{
    public class BasketJsonRepositoryTests
    {
        [Fact]
        public void BasketJsonRepositoryReturnsItemsForCustomer()
        {
            var sut = new BasketJsonRepository("./SampleData/Basket.json");

            var result = sut.Get("testCustomer1");

            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.FirstOrDefault(r => r.ProductId == "product1").Quantity);
            Assert.Equal(3, result.FirstOrDefault(r => r.ProductId == "product2").Quantity);
            Assert.Equal(25, result.FirstOrDefault(r => r.ProductId == "product1").UnitPrice);
            Assert.Equal(60, result.FirstOrDefault(r => r.ProductId == "product2").UnitPrice);
        }

        [Fact]
        public void BasketJsonRepositoryAddItemToBasket()
        {
            var sut = new BasketJsonRepository("./SampleData/Basket.json");
            const string customerName = "testCustomer3";

            var shoppingCartItem = new ShoppingCartItem
            {
                Customer = customerName,
                ProductId = "product3",
                Quantity = 2,
                UnitPrice = 43
            };

            sut.Add(shoppingCartItem);

            var result = sut.Get(customerName);

            Assert.Equal(1, result.Count());
            Assert.Equal(2, result.FirstOrDefault(r => r.ProductId == "product3").Quantity);
            Assert.Equal(43, result.FirstOrDefault(r => r.ProductId == "product3").UnitPrice);
        }
    }
}
