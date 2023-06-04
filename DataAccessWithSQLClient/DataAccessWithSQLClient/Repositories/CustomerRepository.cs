using Azure.Core;
using DataAccessWithSQLClient.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessWithSQLClient.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        public bool AddNewCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <c>GetAllCustomer</c> method get list of all customers from database;
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetAllCustomer()
        {
            List<Customer> customers = new List<Customer>();
            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer";
            try
            {
                // Connect
                using (SqlConnection conn = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // Execute a command
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Reader
                            while (reader.Read())
                            {
                                // Handle result
                                Customer temp = new Customer();
                                temp.CustomerId = reader.GetInt32(0);
                                temp.FirstName = reader.GetString(1);
                                temp.LastName = reader.GetString(2);
                                temp.Country = CheckNullValue(reader, 3);
                                temp.PostalCode = CheckNullValue(reader, 4);
                                temp.Phone = CheckNullValue(reader, 5);
                                temp.Email = reader.GetString(6);
                                customers.Add(temp);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customers;
        }

        /// <summary>
        /// <c>GetCustomerById</c> method get a customer specified by customer id.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>Customer</returns>
        /// <exception cref="Exception"></exception>
        public Customer GetCustomerById(int id)
        {
            Customer customer = new Customer();
            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer" +
                " Where CustomerId = @CustomerId";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand( sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId" , (SqlDbType)id);
                        using(SqlDataReader reader =  cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                customer.CustomerId = reader.GetInt32(0);
                                customer.FirstName = reader.GetString(1);
                                customer.LastName = reader.GetString(2);
                                customer.Country = CheckNullValue(reader, 3);
                                customer.PostalCode = CheckNullValue(reader, 4);
                                customer.Phone = CheckNullValue(reader, 5);
                                customer.Email = reader.GetString(6);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customer;
        }

        public Customer GetCustomerByName(string name)
        {
            Customer customer = new Customer();
            string sql = "SELECT TOP 1 CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer" +
                " Where FirstName LIKE @Name OR LastName Like @Name";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name+"%");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customer.CustomerId = reader.GetInt32(0);
                                customer.FirstName = reader.GetString(1);
                                customer.LastName = reader.GetString(2);
                                customer.Country = CheckNullValue(reader, 3);
                                customer.PostalCode = CheckNullValue(reader, 4);
                                customer.Phone = CheckNullValue(reader, 5);
                                customer.Email = reader.GetString(6);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customer;
        }

        public List<Customer> GetNumberOfCustomerByCountry()
        {
            throw new NotImplementedException();
        }

        public List<Customer> HighestSpenderCustomer()
        {
            throw new NotImplementedException();
        }

        public List<Genre> PopularGenreOfCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <c>CheckNullValue</c> method check null values in database result
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="colIndex">Column Index</param>
        /// <returns></returns>
        private static string CheckNullValue(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    }
}
