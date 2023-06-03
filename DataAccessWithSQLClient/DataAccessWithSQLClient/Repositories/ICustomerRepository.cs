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
        /// <summary>
        /// <c>GetAllCustomer</c> method return all customers.
        /// </summary>
        /// <returns>List of customers</returns>
        public List<Customer> GetAllCustomer();
        /// <summary>
        /// <c>GetCustomerById</c> return a specific customer by customerId.
        /// </summary>
        /// <returns>Customer</returns>
        public Customer GetCustomerById();
        /// <summary>
        /// <c>GetCustomerByName</c> return a specific customer by name.
        /// </summary>
        /// <returns></returns>
        public Customer GetCustomerByName();
        /// <summary>
        /// <c>AddNewCustomer</c> add a new customer to database.
        /// </summary>
        /// <returns>Boolean true or false</returns>
        public bool AddNewCustomer();
        /// <summary>
        /// <c>UpdateCustomer</c> update a customer in database.
        /// </summary>
        /// <returns>Boolean true or false</returns>
        public bool UpdateCustomer();
        /// <summary>
        /// <c>GetCustomerByCountry</c> return customers group by country code.
        /// </summary>
        /// <returns>List of customers</returns>
        public List<Customer> GetCustomerByCountry();        
    }
}
