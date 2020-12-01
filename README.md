# alex-project0
functionality

    place orders to store locations for customers
    add a new customer
    search customers by name
    display details of an order
    display all order history of a store location
    display all order history of a customer
    input validation
    exception handling
    persistent data; no prices, customers, order history, etc. hardcoded in C#
    (optional: order history can be sorted by earliest, latest, cheapest, most expensive)
    (optional: get a suggested order for a customer based on his order history)
    (optional: display some statistics based on order history)
    (optional: asynchronous network & file I/O)
    (optional: logging of exceptions, EF SQL commands, and other events to a file) [new]
    (optional: deserialize data from disk) [now optional]
    (optional: serialize data to disk) [now optional]
    
design

    use EF Core (database-first approach) [new]
    use an Azure SQL DB in third normal form [new]
    include a .sql script that can create the database schema and initial data [new]
    don't use public fields
    define and use at least one interface
    core / domain / business logic
    class library
    contains all business logic
    contains domain classes (customer, order, store, product, etc.)
    documentation with <summary> XML comments on all public types and members (optional: <params> and <return>)
    (recommended: has no dependency on UI, data access, or any input/output considerations)
    
customer

    has first name, last name, etc.
    (optional: has a default store location to order from)
    order
    has a store location
    has a customer
    has an order time (when the order was placed)
    can contain multiple kinds of product in the same order
    rejects orders with unreasonably high product quantities
    (optional: some additional business rules, like special deals)
    
location

    has an inventory
    inventory decreases when orders are accepted
    rejects orders that cannot be fulfilled with remaining inventory
    (optional: for at least one product, more than one inventory item decrements when ordering that product)
    product (etc.)
    
user interface

    interactive console application
    has only display- and input-related code
    low-priority component, will be replaced when we move to project 1
    data access [new]
    
class library

    separate project for data access code using EF Core
    contains data access logic but no business logic
    use repository pattern for separation of concerns
    
test

    at least 10 test methods
    focus on unit testing business logic; testing the console app is very low priority
    do proper TDD for at least one class in the app
