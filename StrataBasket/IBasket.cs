using StrataServices.Contracts;

namespace StrataBasket
{
    public interface IBasket
    {
        bool AuthenticateUser(string userName, string password);
        void Add(ShoppingCartItem request);
        Order ProcessOrder(string userName);
        Order GenerateOrder(string userName);
    }
}