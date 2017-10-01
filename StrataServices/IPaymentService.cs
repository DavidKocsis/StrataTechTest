using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrataServices
{
   public interface IPaymentService
    {
        bool Authorize();
    }
}
