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
    public class CustomerDetailsJsonRepoTests
    {
        [Fact]
        public void CustomerDetailsRetrievedCorrectlyFromJson()
        {
            var sut = new CustomerDetailsJsonRepository("./SampleData/Customers.json");
            var result = sut.GetDetails("test1");

            Assert.Equal("test1",result.Name);
            Assert.Equal("testAddress1", result.Address);
            Assert.Equal("test@test.com", result.Email);
            Assert.Equal("password1", result.Password);
            Assert.Equal(CustomerType.Standard, result.Type);
            Assert.Equal(100, result.TwelveMonthTotal);
        }

        [Fact]
        public void CustomerDetailsAreUpdatedCorrectlyToJson()
        {
            var sut = new CustomerDetailsJsonRepository("./SampleData/Customers.json");
            var customer = sut.GetDetails("test1");

            customer.TwelveMonthTotal = 250;
            sut.Update(customer);
            var result = sut.GetDetails("test1");
            Assert.Equal(250,result.TwelveMonthTotal);
        }
    }
}
