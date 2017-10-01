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
    public class CustomerDetailsJsonRepository : ICustomerDetailsRepository
    {
        private string _jsonLocation;

        public CustomerDetailsJsonRepository(string jsonLocation)
        {
            _jsonLocation = jsonLocation;
        }
        public Customer GetDetails(string customerName)
        {
            var customers = GetCustomers();
            return customers.FirstOrDefault(c => c.Name == customerName);
        }

        public void Update(Customer customer)
        {
            var customers = GetCustomers();
            var index = customers.FindIndex(c => c.Name == customer.Name);
            customers[index] = customer;
            File.WriteAllText(_jsonLocation, JsonConvert.SerializeObject(customers));

        }

        private List<Customer> GetCustomers()
        {
           return JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(_jsonLocation));
        }
    }
}
