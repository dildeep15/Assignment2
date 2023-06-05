using Azure.Core;
using DataAccessWithSQLClient.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
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
            bool success = false;
            string sql = "INSERT INTO Customer(FirstName, LastName, Country, PostalCode, Phone, Email)" +
                " VALUES(@FirstName, @LastName, @Country, @PostalCode, @Phone, @Email)";
            try
            {
                using ( SqlConnection conn = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    conn.Open();
                    using ( SqlCommand cmd = new SqlCommand( sql, conn))
                    {
                        // Update values in query string
                        cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                        cmd.Parameters.AddWithValue("@Country", string.IsNullOrEmpty(customer.Country) ? DBNull.Value : customer.Country);
                        cmd.Parameters.AddWithValue("@PostalCode", string.IsNullOrEmpty(customer.PostalCode) ? DBNull.Value : customer.PostalCode);
                        cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty( customer.Phone) ? DBNull.Value : customer.Phone);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        // Execute non-query
                        success = cmd.ExecuteNonQuery() > 0 ? true : false;
                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }

        /// <summary>
        /// <c>GetAllCustomer</c> method get list of all customers from database;
        /// </summary>
        /// <returns>IEnumerable List of Customer</returns>
        public List<Customer> GetAllCustomer(int? OffsetLimit, int? NumberofRows)
        {
            List<Customer> customers = new List<Customer>();
            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer " +
                "ORDER BY CustomerId ";
            try
            {
                // Connect
                using (SqlConnection conn = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    conn.Open();
                    // Update query if values of Offfset & number of rows is provided
                    if (OffsetLimit.HasValue && NumberofRows.HasValue)
                        sql += $"OFFSET {OffsetLimit} ROWS FETCH NEXT {NumberofRows} ROWS ONLY";

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

        /// <summary>
        /// <c>GetCustomerByName</c> method return a customer matching name from database.
        /// </summary>
        /// <param name="name">Name of Customer</param>
        /// <returns>Customer</returns>
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

        public bool UpdateCustomer(Customer customer)
        {
            bool success = false;
            string sql = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Country = @Country, " +
                "PostalCode = @PostalCode, Phone = @Phone, Email = @Email" +
                " WHERE CustomerId = @CustomerId";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // Update values in query string
                        cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                        cmd.Parameters.AddWithValue("@Country", string.IsNullOrEmpty(customer.Country) ? DBNull.Value : customer.Country);
                        cmd.Parameters.AddWithValue("@PostalCode", string.IsNullOrEmpty(customer.PostalCode) ? DBNull.Value : customer.PostalCode);
                        cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(customer.Phone) ? DBNull.Value : customer.Phone);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                        // Execute non-query
                        success = cmd.ExecuteNonQuery() > 0 ? true : false;
                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }

        public List<CustomerCountry> GetNumberOfCustomerByCountry()
        {
            throw new NotImplementedException();
        }

        public List<CustomerSpender> HighestSpenderCustomer()
        {
            throw new NotImplementedException();
        }

        public List<CustomerGenre> PopularGenreOfCustomer(int id)
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
