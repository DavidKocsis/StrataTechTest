using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrataServices.Contracts;

namespace StrataServices
{
    //This class was created to allow us to go through with using the api. in a real life scenarion this would have implmentation to talk to the third party apps handling messaging
    public class Messager : IMessager
    {
        public void SendConfirmationToCustomer(Order order)
        {
           
        }

        public void SendDetailsToCouriers(Order order)
        {
        }
    }
}
