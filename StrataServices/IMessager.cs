using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrataServices.Contracts;

namespace StrataServices
{
    public interface IMessager
    {
        void SendConfirmationToCustomer(Order order);

        void SendDetailsToCouriers(Order order);
    }
}
