using DataAccessWithSQLClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessWithSQLClient.Repositories
{
    internal interface ICustomerRepository
    {
        // Declaration of function to be implemented for customer 
        public List<Customer> GetAllCustomer();
        public Customer GetCustomerById();
        public Customer GetCustomerByName();
        public bool AddNewCustomer();
        public bool UpdateCustomer();
        public List<Customer> GetCustomerByCountry();        
    }
}
