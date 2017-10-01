using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using StrataBasket;
using StrataServices;
using StrataServices.Contracts;
using Xunit;

namespace UnitTests
{
    public class BasketOperationTests
    {
        private const string Name = "TestUser";
        private const string Password = "testpass";
        private readonly IBasketRepository _basketRepository = A.Fake<IBasketRepository>();
        private IPaymentService _paymentServie = A.Fake<IPaymentService>();
        private IMessager _messager = A.Fake<IMessager>();

        private static readonly Customer Customer = new Customer
        {
            Address = "testAddress",
            Email = "test@test.com",
            Name = Name,
            Password = Password,
            Type = CustomerType.Standard,
            TwelveMonthTotal = 480
        };

        [Fact]
        public void UserAuthenticatedCorrectlyIfPasswordIsCorrect()
        {
            var sut = new Basket(CreateMockCustomerRepo(), _basketRepository, _paymentServie, _messager);
            var result = sut.AuthenticateUser(Name, Password);

            Assert.True(result);
        }

        [Fact]
        public void UserNotAuthenticatedIfPasswordIsIncorrect()
        {
            var sut = new Basket(CreateMockCustomerRepo(), _basketRepository, _paymentServie, _messager);
            var result = sut.AuthenticateUser(Name, "wrongPassword");

            Assert.False(result);
        }

        [Fact]
        public void ItemIsAddedToBasket()
        {
            var request = new ShoppingCartItem
            {
                Customer = Name,
                ProductId = "testProduct",
                Quantity = 2,
                UnitPrice = 1.50m
            };


            var sut = new Basket(CreateMockCustomerRepo(), _basketRepository, _paymentServie, _messager);
            sut.Add(request);
            A.CallTo(() => _basketRepository.Add(request)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void OrderIsGeneratedCorrectly()
        {
            var expected = new Order
            {
                Amount = 45.50m,
                Customer = Name,
                Date = DateTime.Now,
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { ProductCode = "test1",Quantity = 1, UnitPrice = 25.50m},
                    new OrderLine { ProductCode = "test2",Quantity = 2, UnitPrice = 10.00m}
                }
            };
            A.CallTo(() => _basketRepository.Get(Name)).Returns(new List<ShoppingCartItem>
            {
                new ShoppingCartItem{Customer = Name,ProductId = "test1",Quantity = 1,UnitPrice = 25.50m},
                new ShoppingCartItem{Customer = Name, ProductId = "test2",Quantity = 2, UnitPrice = 10.00m}
            });

            var sut = new Basket(CreateMockCustomerRepo(), _basketRepository, _paymentServie, _messager);
            var result = sut.GenerateOrder(Name);

            Assert.Equal(expected.Amount,result.Amount);
            Assert.Equal(expected.Customer, result.Customer);
            Assert.Equal(expected.OrderLines[0].Quantity, result.OrderLines[0].Quantity);
            Assert.Equal(expected.OrderLines[0].ProductCode, result.OrderLines[0].ProductCode);
            Assert.Equal(expected.OrderLines[0].UnitPrice, result.OrderLines[0].UnitPrice);

            Assert.Equal(expected.OrderLines[1].Quantity, result.OrderLines[1].Quantity);
            Assert.Equal(expected.OrderLines[1].ProductCode, result.OrderLines[1].ProductCode);
            Assert.Equal(expected.OrderLines[1].UnitPrice, result.OrderLines[1].UnitPrice);
        }

        [Fact]
        public void CustomerTypeIsUpdated()
        {
            var mockcustomerRepo = CreateMockCustomerRepo();
            A.CallTo(() => _basketRepository.Get(Name)).Returns(new List<ShoppingCartItem>
            {
                new ShoppingCartItem {Customer = Name, ProductId = "test1", Quantity = 1, UnitPrice = 25.50m}
            });

            var sut = new Basket(mockcustomerRepo, _basketRepository, _paymentServie, _messager);

            var result = sut.GenerateOrder(Name);
            Customer.Type = CustomerType.Silver;
            A.CallTo(() => mockcustomerRepo.Update(Customer)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void OrderIsNotConfirmedIfPaymentFails()
        {
            var sut = new Basket(CreateMockCustomerRepo(), _basketRepository, _paymentServie, _messager);

            A.CallTo(() => _paymentServie.Authorize()).Returns(false);

            sut.ProcessOrder(Name);

            A.CallTo(()=> _messager.SendConfirmationToCustomer(A<Order>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _messager.SendDetailsToCouriers(A<Order>.Ignored)).MustNotHaveHappened();
        }


        [Fact]
        public void OrderIsonfirmedIfPaymentSuceeds()
        {
            var sut = new Basket(CreateMockCustomerRepo(), _basketRepository, _paymentServie, _messager);

            A.CallTo(() => _paymentServie.Authorize()).Returns(true);
            A.CallTo(() => _basketRepository.Get(Name)).Returns(new List<ShoppingCartItem>
            {
                new ShoppingCartItem {Customer = Name, ProductId = "test1", Quantity = 1, UnitPrice = 25.50m}
            });

            sut.ProcessOrder(Name);

            A.CallTo(() => _messager.SendConfirmationToCustomer(A<Order>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _messager.SendDetailsToCouriers(A<Order>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        private static ICustomerDetailsRepository CreateMockCustomerRepo()
        {
            var customerRepository = A.Fake<ICustomerDetailsRepository>();
            A.CallTo(() => customerRepository.GetDetails(Name)).Returns(Customer);
            return customerRepository;
        }
    }
}