using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeSystem.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int UserId { get; set; }   // matches user_id column
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
