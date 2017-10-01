using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StrataServices.Contracts;

namespace StrataServices.JsonRepos
{
    public class BasketJsonRepository :IBasketRepository
    {
        private readonly string _jsonFileLocation;

        public BasketJsonRepository(string jsonFileLocation)
        {
            _jsonFileLocation = jsonFileLocation;
        }
        public void Add(ShoppingCartItem request)
        {
            var basketitems = GetShoppingCartItems();
            basketitems.Add(request);
            File.WriteAllText(_jsonFileLocation, JsonConvert.SerializeObject(basketitems));
        }

        public List<ShoppingCartItem> Get(string userName)
        {
            var basketItems = GetShoppingCartItems();
            return basketItems.Where(item => item.Customer == userName).ToList();
        }

        private List<ShoppingCartItem> GetShoppingCartItems()
        {
            return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(File.ReadAllText(_jsonFileLocation));
        }
    }
}
