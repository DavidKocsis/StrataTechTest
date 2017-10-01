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
    public class ProductJsonRepository : IProductRepository
    {
        private readonly string _jsonLocation;

        public ProductJsonRepository(string jsonLocation)
        {
            _jsonLocation = jsonLocation;
        }


        public Product Get(string productId)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(_jsonLocation));
            return products.FirstOrDefault(c => c.Code == productId);
        }
    }
}
