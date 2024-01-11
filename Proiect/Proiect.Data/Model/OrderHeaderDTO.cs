using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Data.Model
{
    public class OrderHeaderDTO
    {
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public string Adress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public DateTime CheckoutDate { get; set; }
    }
}
