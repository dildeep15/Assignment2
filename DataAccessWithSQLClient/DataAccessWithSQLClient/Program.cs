using DataAccessWithSQLClient.Models;
using DataAccessWithSQLClient.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace DataAccessWithSQLClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICustomerRepository repository = new CustomerRepository();
            // Display all customers
            //GetAllCustomers(repository);

            // Get Customer by Id
            // GetCustomerById(repository,1);

            //GetCustomerByName(repository, "Helena");

            //GetPageOfCustomer(repository, 10, 10);


            Customer customer = new Customer()
            {
                CustomerId = 60,
                FirstName = "UpdatedJohn",
                LastName  = "Cena",
                Country = "US",
                PostalCode = "343 34",
                Phone = "+23-3434343",
                Email = "johnsmith@gmail.com"
            };
            //AddCustomer(repository, customer);
            UpdateCustomer(repository, customer);

        }


        /// <summary>
        ///  <c>DisplayCustomers</c> method display all customers.
        /// </summary>
        /// <param name="repository"></param>
        private static void GetAllCustomers(ICustomerRepository repository)
        {
            PrintCustomers(repository.GetAllCustomer(null, null));
        }

        /// <summary>
        /// <c>DisplayCustomer</c> method returns a customer
        /// </summary>
        /// <param name="repository"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void GetCustomerById(ICustomerRepository repository, int customerId)
        {
            Customer customer = repository.GetCustomerById(customerId);
            if (customer != null)
                PrintCustomer(customer);
            else
                Console.WriteLine("No customer found with "+customerId+" Customer ID");
        }

        /// <summary>
        /// <c>GetCustomerByName</c> method get customer by name.
        /// </summary>
        /// <param name="repository">Repository</param>
        /// <param name="name">Name of customer</param>
        private static void GetCustomerByName(ICustomerRepository repository, string name)
        {
            Customer customer = repository.GetCustomerByName(name);
            if (customer != null)
                PrintCustomer(customer);
            else
                Console.WriteLine("No customer found with " + name + " Name");
        }

        /// <summary>
        /// <c>GetPageOfCustomer</c> method return rows of customer by defined offset.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="offset"></param>
        /// <param name="rows"></param>
        private static void GetPageOfCustomer(ICustomerRepository repository, int offset, int rows)
        {
            PrintCustomers(repository.GetAllCustomer(offset, rows));
        }

        /// <summary>
        /// <c>AddCustomer</c> method validate & insert a new customer record in database
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="customer"></param>
        private static void AddCustomer(ICustomerRepository repository, Customer customer)
        {
            // Validate customer for non-null fields;
            if(customer.FirstName.IsNullOrEmpty() || customer.LastName.IsNullOrEmpty()|| customer.Email.IsNullOrEmpty())
                Console.WriteLine("A Customer must have valid First Name, Last Name & Email!");

            else
            {
                // Continue with insertion
                if(repository.AddNewCustomer(customer))
                    Console.WriteLine($"Customer {customer.FirstName} {customer.LastName} added successfully!");
                else
                    Console.WriteLine("Unable to add customer to database");
            }
        }

        /// <summary>
        /// <c>UpdateCustomer</c> method update an existing customer in database.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="customer"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void UpdateCustomer(ICustomerRepository repository, Customer customer)
        {
            // Validate primary key for table
            if (customer.CustomerId == 0)
                Console.WriteLine("Can not update customer information without CustomerId");

            // Validate customer for non-null fields;
            else if (customer.FirstName.IsNullOrEmpty() || customer.LastName.IsNullOrEmpty() || customer.Email.IsNullOrEmpty())
                Console.WriteLine("A Customer must have valid First Name, Last Name & Email!");

            else
            {
                // Continue with insertion
                if (repository.UpdateCustomer(customer))
                    Console.WriteLine($"Customer {customer.FirstName} {customer.LastName} updated successfully!");
                else
                    Console.WriteLine("Unable to update customer in database");
            }
        }



        /// <summary>
        /// <c>PrintCustomers</c> method prints IEnumerable list of customers
        /// </summary>
        /// <param name="customers">IEnumerable customer list</param>
        private static void PrintCustomers(IEnumerable<Customer> customers)
        {
            if(customers != null && customers.Any())
            {
                // Print column for customer table
                foreach (Customer customer in customers)
                    PrintCustomer(customer);
            }
            else
            {
                Console.WriteLine("No Customer found!");
            }
        }

        /// <summary>
        /// <c>PrintCustomer</c> method print a customer details.
        /// </summary>
        /// <param name="customer">Customer</param>
        private static void PrintCustomer(Customer customer)
        {
            PrintLine( 100 );
            Console.WriteLine($"{customer.CustomerId} -- {customer.FirstName} -- {customer.LastName} -- {customer.Country} -- {customer.PostalCode} -- {customer.Phone} -- {customer.Email}");
        }

        /// <summary>
        /// <c>PrintLine</c> prints -  symbols count numbers of time. 
        /// </summary>
        /// <param name="count">number of times</param>
        private static void PrintLine(int count)
        {
            Console.WriteLine(new string( '-', count));
        }
    }
}