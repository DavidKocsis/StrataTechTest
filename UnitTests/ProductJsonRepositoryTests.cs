using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrataServices.JsonRepos;
using Xunit;

namespace UnitTests
{
    public class ProductJsonRepositoryTests
    {
        [Fact]
        public void ProductJsonRepoReturnsCorrectData()
        {
            var sut = new ProductJsonRepository("./SampleData/Product.json");

            var result = sut.Get("product1");

            Assert.Equal(result.Code, "product1");
            Assert.Equal(result.Description, "Test Product 1");
            Assert.Equal(result.UnitPrice, 25);
        }
    }
}
