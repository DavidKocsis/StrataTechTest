using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrataServices;
using StrataServices.Contracts;
using StrataServices.Mappers;
using StrateTest;

namespace StrataBasket
{
    public class Basket
    {
        private readonly ICustomerDetailsRepository _customerDetailsRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IPaymentService _paymentService;
        private readonly IMessager _messager;


        public Basket(ICustomerDetailsRepository customerDetailsRepository, 
            IBasketRepository basketRepository,
            IPaymentService paymentService,
            IMessager messager)
        {
            _customerDetailsRepository = customerDetailsRepository;
            _basketRepository = basketRepository;
            _paymentService = paymentService;
            _messager = messager;
        }

        public bool AuthenticateUser(string userName, string password)
        {
            var customer = _customerDetailsRepository.GetDetails(userName);

            return customer.Password == password;
        }

        public void Add(ShoppingCartItem request)
        {
            _basketRepository.Add(request);
        }

        public void ProcessOrder(string userName)
        {
            //chek payment
            if(_paymentService.Authorize())
            {
               var order =  GenerateOrder(userName);
                _messager.SendConfirmationToCustomer(order);
                _messager.SendDetailsToCouriers(order);
            }
            //generate order
            //send confimation
            //send courier details
        }

        public Order GenerateOrder(string userName)
        {
            var cartitems = _basketRepository.Get(userName);
            var customer = _customerDetailsRepository.GetDetails(userName);
            var order =  OrderMapper.Map(cartitems,Discounts.DiscountAmounts[customer.Type]);
            if (customer.TwelveMonthTotal + order.Amount > 800)
            {
                customer.Type = CustomerType.Gold;
                _customerDetailsRepository.Update(customer);
            }
            else if (customer.TwelveMonthTotal + order.Amount > 500)
            {
                customer.Type = CustomerType.Silver;
                _customerDetailsRepository.Update(customer);
            }
            return order;
        }

    }
}
