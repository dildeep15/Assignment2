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
        public List<Customer> GetAllCustomer(int? OffsetLimit, int? NumberofRows);
        /// <summary>
        /// <c>GetCustomerById</c> return a specific customer by customerId.
        /// </summary>
        /// <returns>Customer</returns>
        public Customer GetCustomerById(int id);
        /// <summary>
        /// <c>GetCustomerByName</c> return a specific customer by name.
        /// </summary>
        /// <returns></returns>
        public Customer GetCustomerByName(string name);
        /// <summary>
        /// <c>AddNewCustomer</c> add a new customer to database.
        /// </summary>
        /// <returns>Boolean true or false</returns>
        public bool AddNewCustomer(Customer customer);
        /// <summary>
        /// <c>UpdateCustomer</c> update a customer in database.
        /// </summary>
        /// <returns>Boolean true or false</returns>
        public bool UpdateCustomer(Customer customer);
        /// <summary>
        /// <c>GetCustomerByCountry</c> return customers group by country code.
        /// </summary>
        /// <returns>List of customers</returns>
        public List<Customer> GetNumberOfCustomerByCountry();
        /// <summary>
        /// <c>HighestSpenderCustomer</c> return highest spender customers in descending order.
        /// </summary>
        /// <returns>List of customer</returns>
        public List<Customer> HighestSpenderCustomer();
        /// <summary>
        /// <c>PopularGenreOfCustomer</c> return most popular genre for a customer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Genre> PopularGenreOfCustomer(int id);
    }
}
