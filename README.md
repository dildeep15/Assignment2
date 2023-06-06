# Noroff  Assignment2
Sql database scripts &amp; a c# console application with data access using sqlclient

<div align="center">
    <h1>Assignment2</h1>
</div>

## Sql database scripts &amp; a c# console application with data access using sqlclient

This assignment is divided in two appendix
## Appendix A: Multiple database scripts in SQL Scripts folder
>01_dbCreate.sql: Create the SuperheroesDb database.
>02_tableCreate.sql: Create Superhero, Assistant and Power tables.
>03_relationshipSuperheroAssistant.sql: Alter & define relationship between Superhero & Assistant tables.
>04_relationshipSuperheroPower.sql: Alter & define relationship between Superhero & Power tables.
>05_insertSuperheroes.sql: Insert data into Superhero table.
>06_insertAssistants.sql: Insert data into Assitant table.
>07_powers.sql: Insert data into Power table.
>08_updateSuperhero.sql: Update Superhero table.
>09_deleteAssistant.sql: Delete data from Assistant table.

## Appendix B: A C# console application with SQL Client library to interact with the database(Chinook)

>The application allows you to create different types of Heroes. For example Mage, Ranger, Rogue or Warrior.
>A Hero can be created just with a name.
>Each hero can equip various armor and weapons and are displayed under hero equipments. Each hero has a valid armor types and valid armor types that it can equip.
>All character types have their own attributes, which is a combination of **stregnth, Dexterity  & intelligence**.
>Every hero start at level 1 and on level up attributes of hero gets different attributes gain.

## General info and usage
>The console application has 5 datamodels in Models folder: Customer, CustomerCountry, CustomerGenre, CustomerSpender and Invoice.
>The application has 3 repositories: ConnectionStringHelper, ICustomerRepository(Interface), CustomerRepository.
>Program class with main function.

## Usage:

```
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

## Technologies

Project is created with:
* .Net6
* C#
* Microsoft.Data.SqlClient(5.1.1)
