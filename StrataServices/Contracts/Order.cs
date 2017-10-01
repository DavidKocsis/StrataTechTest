using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrataServices.Contracts
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public string Customer { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public List<OrderLine> OrderLines { get; set; }
    }

    public class OrderLine
    {
     public Guid OrderId { get; set; }
        
     public string ProductCode { get; set; }

     public int Quantity { get; set; }

     public decimal UnitPrice { get; set; }
    }
}
