using DataAccessWithSQLClient.Models;
using DataAccessWithSQLClient.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlTypes;

namespace DataAccessWithSQLClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create object of CustomerRepository class
            ICustomerRepository repository = new CustomerRepository();

            // Display all customers
            GetAllCustomers(repository);

            // Get Customer by Id 
            int customerId = 1;
            GetCustomerById(repository, customerId);

            // Get Customer By Name
            string name = "Helena";
            GetCustomerByName(repository, name);

            // Get Customer with offset & number of rows
            int offset = 10;
            int numberOfRows = 10;
            GetPageOfCustomer(repository, offset, numberOfRows);

            // Add a new Customer
            Customer customer = new Customer()
            {
                FirstName = "UpdatedJohn",
                LastName  = "Cena",
                Country = "US",
                PostalCode = "343 34",
                Phone = "+23-3434343",
                Email = "johnsmith@gmail.com"
            };
            AddCustomer(repository, customer);

            //Update an existing Customer
            Customer update = new Customer()
            {
                CustomerId = 60,
                FirstName = "UpdatedJohn",
                LastName = "Cena",
                Country = "US",
                PostalCode = "343 34",
                Phone = "+23-3434343",
                Email = "johnsmith@gmail.com"
            };
            UpdateCustomer(repository, update);

            // Get numbers of customer per Country
            GetCustomersPerCountry(repository);

            // Get customer with highest invoice amount
            GetHighestSpender(repository);

            // Get popular genre of customer depending on invoiced tracks 
            customerId = 12;
            GetCustomerPopularGenre(repository, customerId);
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
        /// <c>GetCustomersInCountry</c> method get numbers of customer per country
        /// </summary>
        /// <param name="repository"></param>
        private static void GetCustomersPerCountry(ICustomerRepository repository)
        {
            PrintCustomerCountry(repository.GetNumberOfCustomerByCountry());
        }

        /// <summary>
        /// <c>PrintCustomerCountry</c> method print IEnumerable list of customers per country
        /// </summary>
        /// <param name="customerCountries"></param>
        private static void PrintCustomerCountry(IEnumerable<CustomerCountry> customerCountries)
        {
            if (customerCountries != null && customerCountries.Any())
            {
                // Print column name for data
                Console.WriteLine("Country -- Number Of Customers");
                // Print column for customer table
                foreach (CustomerCountry cc in customerCountries)
                {
                    PrintLine(30);
                    Console.WriteLine($"{cc.Country} -- {cc.NumberOfCustomers}");
                }           
            }
            else
            {
                Console.WriteLine("Unable to get number of customer per country record from database");
            }
        }

        /// <summary>
        /// <c>GetHighestSpender</c> method get a list of customer with invoice total in descending order.
        /// </summary>
        /// <param name="repository"></param>
        private static void GetHighestSpender(ICustomerRepository repository)
        {
            PrintHighestSpender(repository.GetHighestSpenderCustomers());
        }

        /// <summary>
        /// <c>PrintHighestSpender</c> method prints a list of customers with invoice total in descending order to console.
        /// </summary>
        /// <param name="customerSpenders"></param>
        private static void PrintHighestSpender(List<CustomerSpender> customerSpenders)
        {
            if (customerSpenders != null && customerSpenders.Any())
            {
                // Print column name for data
                Console.WriteLine("Customer ID -- Invoice Total");
                // Print column for customer table
                foreach (CustomerSpender cs in customerSpenders)
                {
                    PrintLine(30);
                    Console.WriteLine($"\t{cs.CustomerId} \t  {cs.InvoiceTotal}");
                }
            }
            else
            {
                Console.WriteLine("Unable to get highest spender customers from database");
            }
        }


        /// <summary>
        /// <c>GetCustomerPopularGenre</c> method get customer popular genre & print it to console.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="customerId"></param>
        private static void GetCustomerPopularGenre(ICustomerRepository repository, int customerId)
        {
            PrintCustomerGenre(repository.PopularGenreOfCustomer(customerId));
        }

        /// <summary>
        /// <c>PrintCustomerGenre</c> method print customer popular genre to console.
        /// </summary>
        /// <param name="customerGenres"></param>
        private static void PrintCustomerGenre(List<CustomerGenre> customerGenres)
        {
            if (customerGenres != null && customerGenres.Any())
            {
                // Assign first Genre.
                string popularGenre = customerGenres[0].Name;
                for (int i = 1; i < customerGenres.Count(); i++)
                {
                    // Check if there is tie up in popular genre
                    if (customerGenres[i-1].totalTracks == customerGenres[i].totalTracks)
                        popularGenre +=" & "+ customerGenres[i].Name;
                }
                Console.WriteLine("The popular genre of customer:"+ popularGenre);
            }
            else
            {
                Console.WriteLine("The Customer has not buyed any tracks");
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
