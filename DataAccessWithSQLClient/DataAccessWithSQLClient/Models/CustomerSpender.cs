using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessWithSQLClient.Models
{
    internal class CustomerSpender
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double InvoiceTotal { get; set; }
    }
}
