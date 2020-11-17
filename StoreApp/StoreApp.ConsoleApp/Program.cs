using System;
using System.Collections.Generic;
using StoreApp.Library;
using System.Linq;

namespace StoreApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StoreRepository stores = new StoreRepository();
            CustomerRepository customers = new CustomerRepository();
            OrderRepository orders = new OrderRepository();
            ProductRepository products = new ProductRepository();

            populateCustomers(customers);
            populateProductsStoresAndOrders(products, stores, orders);


            List<char> options = new List<char> { '1', '2', '3', '4', '5', '6' };

            Console.WriteLine(" \t \t \t Welcome to Alex's Order Application\n");

            char option = DisplayMainMenu();


            while(option != '6')
            {
                while (!options.Contains(option))
                {
                    Console.WriteLine("\nInvalid input, please type the number corresponding to desired option\n");
                    option = DisplayMainMenu();
                }

                switch (option)
                {
                    case '1':
                        Console.WriteLine("\n \t \t \t Register New Customer\n");
                        RegisterNewCustomer(customers);
                        break;
                    case '2':
                        Console.WriteLine("\n \t \t \t Display Customer Info\n");
                        DisplayCustomerDetails(customers);
                        break;
                    case '3':
                        Console.WriteLine("\n \t \t \t Display Customer Orders\n");
                        DisplayCustomerOrders(customers, orders, stores, products);
                        break;
                    case '4':
                        Console.WriteLine("\n \t \t \t Display Store Orders\n");
                        DisplayStoreOrders(customers, orders, stores, products);
                        break;
                    case '5':
                        Console.WriteLine("\n \t \t \t Place an Order\n");
                        PlaceOrder(customers, orders, stores, products);
                        break;
                }
                option = DisplayMainMenu();
            }
        }

        public static char DisplayMainMenu()
        {
            Console.WriteLine("Select an option from the menu below:");
            Console.WriteLine("1) Register New Customer");
            Console.WriteLine("2) Display Customer Details");
            Console.WriteLine("3) Display Customer Order History");
            Console.WriteLine("4) Display Store Order History");
            Console.WriteLine("5) Place an Order");
            Console.WriteLine("6) Quit Aplication \n");


            char c = Console.ReadKey().KeyChar;

            return c;
        }

        public static void RegisterNewCustomer(CustomerRepository customers)
        {
            Console.WriteLine("Enter Your First Name:");
            String firstName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("\n Invalid input, please provide a real first name");
                Console.WriteLine("Enter Your First Name:");

                firstName = Console.ReadLine();

            }
            Console.WriteLine("\nEnter Your Last Name:");
            String lastName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("\n Invalid input, please provide a real last name");
                Console.WriteLine("Enter Your Last Name:");

                lastName = Console.ReadLine();
            }

            Console.WriteLine("\nEnter Your Phone Number: '(xxx)xxx-xxx'");
            String phone = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("\n Invalid input, please provide a real phone number");
                Console.WriteLine("Enter Your Phone Number: '(xxx)xxx-xxx'");
                phone = Console.ReadLine();
            }

            Customer newCustomer = new Customer(firstName, lastName, phone);
            bool success = false;

            try
            {
                success = customers.AddNewCustomer(newCustomer);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("\nError adding customer. Returning to main Menu...\n");
                return;
            };
            

            if (!success)
            {
                Console.WriteLine("\nCustomer Already Exists. Returning to main Menu...\n");
            }
            else
            {
                Console.WriteLine("\nCustomer Succesfully Added. Returning to main Menu...\n");
            }
        }

        public static void DisplayCustomerDetails(CustomerRepository customers)
        {
            Console.WriteLine("Enter Your First Name:");
            String firstName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("\nInvalid input, please provide a real first name");
                Console.WriteLine("Enter Your First Name:");

                firstName = Console.ReadLine();

            }

            Console.WriteLine("\nEnter Your Last Name:");
            String lastName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("\nInvalid input, please provide a real last name");
                Console.WriteLine("Enter Your Last Name:");

                lastName = Console.ReadLine();
            }

            Customer customer = customers.GetCustomerByFirstAndLastName(firstName, lastName);
            if (customer != null)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Name: {customer.CustomerFullName}");
                Console.WriteLine($"Phone: {customer.PhoneNumber}\n");
            }
            else
            {
                Console.WriteLine("\nCustomer not found. Returning to main menu...\n");

            }

        }

        public static void DisplayCustomerOrders(CustomerRepository customers, OrderRepository orders, StoreRepository stores, ProductRepository products)
        {
            Console.WriteLine("Enter Your First Name:");
            String firstName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("\nInvalid input, please provide a real first name");
                Console.WriteLine("Enter Your First Name:");

                firstName = Console.ReadLine();

            }

            Console.WriteLine("\nEnter Your Last Name:");
            String lastName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("\nInvalid input, please provide a real last name");
                Console.WriteLine("Enter Your Last Name:");

                lastName = Console.ReadLine();
            }

            Customer customer = customers.GetCustomerByFirstAndLastName(firstName, lastName);

            if (customer != null)
            {
                List<Order> customerOrders = orders.GetOrdersCustomerId(customer.CustomerId);

                if (customerOrders.Any())
                {
                    foreach(var order in customerOrders)
                    {
                        Store store = stores.GetStoreById(order.GetStoreId);

                        
                        Console.WriteLine($"\nOrder Id: {order.OrderId}");
                        Console.WriteLine($"Date and Time: {order.GetTime}");
                        Console.WriteLine($"Store: {store.StoreName}");
                        Console.WriteLine($"Products:");

                        foreach(var item in order.GetOrderItems)
                        {
                            IProduct product = products.GetProductById(item.Key);
                            Console.WriteLine($"\t{product.ProductName}: {item.Value}");
                        }
                        Console.WriteLine($"Total: ${order.GetTotal}");
                    }
                    Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine($"\n{customer.CustomerFullName} has placed no orders yet. Returning to main menu...\n");
                }
            }
            else
            {
                Console.WriteLine("\nCustomer not found. Returning to main menu...\n");

            }

        }

        public static void DisplayStoreOrders(CustomerRepository customers, OrderRepository orders, StoreRepository stores, ProductRepository products)
        {
            Console.WriteLine("Enter Store Name:");
            String storeName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(storeName))
            {
                Console.WriteLine("\n Invalid input, please provide a real first name");
                Console.WriteLine("Enter Your First Name:");

                storeName = Console.ReadLine();

            }

            Store store = stores.GetStoreByName(storeName);

            if (store != null)
            {
                List<Order> storeOrders = orders.GetOrdersByStoreId(store.StoreId);

                if (storeOrders.Any())
                {
                    foreach (var order in storeOrders)
                    {
                        Customer customer = customers.GetCustomerById(order.GetCustomerId);


                        Console.WriteLine($"\nOrder Id: {order.OrderId}");
                        Console.WriteLine($"Date and Time: {order.GetTime}");
                        Console.WriteLine($"Customer: {customer.CustomerFullName}");
                        Console.WriteLine($"Products:");

                        foreach (var item in order.GetOrderItems)
                        {
                            IProduct product = products.GetProductById(item.Key);
                            Console.WriteLine($"\t{product.ProductName}: {item.Value}");
                        }
                        Console.WriteLine($"Total: ${order.GetTotal}");
                    }
                    Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine($"\n{store.StoreName} has no orders yet. Returning to main menu...\n");
                }
            }
            else
            {
                Console.WriteLine("\nStore not found. Returning to main menu...\n");

            }
        }

        public static void PlaceOrder(CustomerRepository customers, OrderRepository orders, StoreRepository stores, ProductRepository products)
        {
            Console.WriteLine("Enter Your First Name:");
            String firstName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("\nInvalid input, please provide a real first name");
                Console.WriteLine("Enter Your First Name:");

                firstName = Console.ReadLine();

            }

            Console.WriteLine("\nEnter Your Last Name:");
            String lastName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("\nInvalid input, please provide a real last name");
                Console.WriteLine("Enter Your Last Name:");

                lastName = Console.ReadLine();
            }

            Customer customer = customers.GetCustomerByFirstAndLastName(firstName, lastName);

            if (customer != null)
            {
                List<Store> allStores = stores.GetAllStores();

                if (allStores.Any())
                {
                    Console.WriteLine("\nStores available:");
                    for(int i = 0; i < allStores.Count(); i++)
                    {
                        Console.WriteLine($"{i}) {allStores[i].StoreName}");
                    }

                    Console.WriteLine("\nType number corresponding to store you want to place order at:");

                    int storeChoice = (int)char.GetNumericValue(Console.ReadKey().KeyChar);
                    

                    while (storeChoice < 0 || storeChoice > allStores.Count()-1)
                    {
                        Console.WriteLine("\nInvalid option, please try again");
                        Console.WriteLine("\nType number corresponding to store you want to place order at:");

                        storeChoice = (int)char.GetNumericValue(Console.ReadKey().KeyChar);
                    }

                    Console.WriteLine("\n");


                    Store store = allStores[storeChoice];

                    Console.WriteLine("\nAvailable Products:");

                    if (store.StoreInventory.Any())
                    {
                        foreach (var item in store.StoreInventory)
                        {
                            IProduct product = products.GetProductById(item.Key);
                            Console.WriteLine($"\tName:{product.ProductName} \t\t\t\tAvailable:{item.Value}");
                        }
                        Console.WriteLine("\n");
                    }
                    else
                    {
                        Console.WriteLine("\nNo products available for purchase. Returning to main menu...\n");
                        return;
                    }

                    Console.WriteLine("Enter name of Product you want to add to cart or type 'Done' if done ordering: ");

                    String productName = Console.ReadLine();
                    IProduct product1 = null;
                    Dictionary<int, int> cart = new Dictionary<int, int>();
                    double total = 0.0;
                    while (productName != "Done"){

                        while (string.IsNullOrWhiteSpace(productName))
                        {
                            Console.WriteLine("\nInvalid input. Product does not exist, please provide product name from the list exactly as it appears");
                            Console.WriteLine("Enter name of Product you want to add to cart or type 'Done' if done ordering: ");
                            productName = Console.ReadLine();

                            if(productName == "Done")
                            {
                                break;
                            }
                        }

                        if(productName == "Done")
                        {
                            break;
                        }

                        product1 = products.GetProductByName(productName);
                        if (product1 == null || !store.StoreInventory.ContainsKey(product1.ProductId))
                        {
                            Console.WriteLine("\nInvalid input. Product does not exist, please provide product name from the list exactly as it appears");
                            Console.WriteLine("Enter name of Product you want to add to cart or type 'Done' if done ordering: ");
                            productName = Console.ReadLine();
                            continue;
                        }

                        int amount = 0;

                        Console.WriteLine("\nHow many would you like to add to cart? ");
                        try
                        {
                            amount = Int32.Parse(Console.ReadLine());
                        }
                        catch(FormatException)
                        {
                            amount = -1;
                        }
                        catch(ArgumentNullException)
                        {
                            amount = -1;
                        }
                        catch(OverflowException)
                        {
                            amount = -1;
                        }
                        

                        while(amount < 0 || amount > store.StoreInventory[product1.ProductId])
                        {
                            Console.WriteLine($"\nInvalid amount. Make sure amount is a valid positive integer, inlcudes no white spaces and its not higher than current available stock ({store.StoreInventory[product1.ProductId]})");
                            Console.WriteLine("\nHow many would you like to add to cart? ");
                            try
                            {
                                amount = Int32.Parse(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                amount = -1;
                            }
                            catch (ArgumentNullException)
                            {
                                amount = -1;
                            }
                            catch (OverflowException)
                            {
                                amount = -1;
                            }
                        }

                        cart.Add(product1.ProductId, amount);
                        total += product1.Price * amount;
                        Console.WriteLine("\nItem added to cart succesfully\n");
                        Console.WriteLine("Enter name of Product you want to add to cart or type 'Done' if done ordering: ");
                        productName = Console.ReadLine();
                        product1 = null;


                    }


                    if (cart.Any())
                    {
                        Console.WriteLine("\nDo you wish to place order Y/N?");
                        char YesOrNo = Console.ReadKey().KeyChar;
                        Console.WriteLine("\n");

                        while (YesOrNo != 'Y' && YesOrNo != 'N')
                        {
                            Console.WriteLine("\nInvalid input please type 'Y' for Yes and 'N' for No");
                            YesOrNo = Console.ReadKey().KeyChar;
                            Console.WriteLine("\n");

                        }

                        if (YesOrNo == 'Y')
                        {
                            Order order = new Order(store.StoreId, customer.CustomerId, DateTime.Now, total, cart);
                            stores.PlaceOrder(store.StoreId, cart);
                            orders.AddNewOrder(order);
                            Console.WriteLine("\nOrder placed succesfully\n");

                        }
                        else
                        {
                            Console.WriteLine("\nOrder Canceled. Returning to main menu...\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYour cart is empty. Returning to main menu...\n");
                    }
                    
                }
                else
                {
                    Console.WriteLine("\nNo Stores in database yet, sorry :(\n");
                }
            }
            else
            {
                Console.WriteLine("\nCustomer not found. Please register first to place an order. Returning to main menu...\n");
            }
        }

        public static void populateCustomers(CustomerRepository customers)
        {

            Customer alex = new Customer(1, "Alex", "Soto", "(619)730-8241");
            Customer yesenia = new Customer(2, "Yesenia", "Cisneros", "(626)482-7104");
            Customer chupis = new Customer(3, "Armando", "Soto", "(619)547-2327");

            customers.AddNewCustomer(alex);
            customers.AddNewCustomer(yesenia);
            customers.AddNewCustomer(chupis);

        }
        
        public static void populateProductsStoresAndOrders(ProductRepository products, StoreRepository stores, OrderRepository orders)
        {
            IProduct toiletPaper = new Product(1, "Toilet Paper", 3.00);
            IProduct apple = new Product(2, "Apple", 1.00);
            IProduct ps5 = new Product(3, "Play Station 5", 500.00);
            IProduct burger = new Product(4, "Cheese Burger", 2.00);
            IProduct chancla = new Product(5, "Chancla", 5.00);
            IProduct picture = new Product(6, "Picture", 4.00);
            IProduct makeup = new Product(7, "Makeup", 25.00);
            IProduct fountaindrink = new Product(8, "Fountain Drink", 2.00);
            IProduct fries = new Product(9, "Fries", 3.00);
            IProduct salad = new Product(10, "Salad", 7.00);
            IProduct book = new Product(11, "Book", 6.00);
            IProduct computer = new Product(12, "Computer", 600.00);
            IProduct chair = new Product(13, "Chair", 100.00);
            IProduct mirror = new Product(14, "Mirror", 50.00);
            IProduct pizza = new Product(15, "Pizza", 13.00);

            products.AddNewProduct(toiletPaper);
            products.AddNewProduct(apple);
            products.AddNewProduct(ps5);
            products.AddNewProduct(burger);
            products.AddNewProduct(chancla);
            products.AddNewProduct(picture);
            products.AddNewProduct(makeup);
            products.AddNewProduct(fountaindrink);
            products.AddNewProduct(fries);
            products.AddNewProduct(salad);
            products.AddNewProduct(book);
            products.AddNewProduct(computer);
            products.AddNewProduct(chair);
            products.AddNewProduct(mirror);
            products.AddNewProduct(pizza);

            Store walmart = new Store(1, "Walmart");
            Store staples = new Store(2, "Staples");
            Store fastFoodStore = new Store(3, "Fast Food Stop");



            walmart.AddToInventory(book, 30);
            walmart.AddToInventory(computer, 10);
            walmart.AddToInventory(toiletPaper, 100);
            walmart.AddToInventory(mirror, 20);
            walmart.AddToInventory(apple, 150);
            walmart.AddToInventory(makeup, 40);
            walmart.AddToInventory(picture, 50);
            walmart.AddToInventory(chair, 25);

            staples.AddToInventory(computer, 50);
            staples.AddToInventory(chair, 70);
            staples.AddToInventory(book, 200);

            fastFoodStore.AddToInventory(burger, 200);
            fastFoodStore.AddToInventory(fountaindrink, 300);
            fastFoodStore.AddToInventory(fries, 200);
            fastFoodStore.AddToInventory(pizza, 100);
            fastFoodStore.AddToInventory(salad, 50);

            stores.AddNewStore(walmart);
            stores.AddNewStore(staples);
            stores.AddNewStore(fastFoodStore);


            Dictionary<int, int> orderProducts = new Dictionary<int, int> { { 11, 2 }, { 12, 1 }, {1,5} };
            Dictionary<int, int> orderProducts2 = new Dictionary<int, int> { { 4, 2 }, { 9, 2 }, { 8, 2 } };


            Order order1 = new Order(1, 1, 1, new DateTime(2020, 11, 16, 7, 0, 0),627.00,orderProducts);
            Order order2 = new Order(2, 3, 1, new DateTime(2019, 05, 09, 9, 15, 0), 14.0, orderProducts2);

            stores.PlaceOrder(order1.GetStoreId, order1.GetOrderItems);
            stores.PlaceOrder(order2.GetStoreId, order2.GetOrderItems);

            orders.AddNewOrder(order1);
            orders.AddNewOrder(order2);

        }

        
            

        
    }
}
