// ID: S10255981, S10257966
// Name: Rainnen, Ethan

using assignment;
using System.Globalization;

string CUSTOMERS_PATH = "customers.csv";
string ORDERS_PATH = "orders.csv";
void DisplayMenu()
{
    Console.WriteLine("==========================================");
    Console.WriteLine("[1] List All Customers");
    Console.WriteLine("[2] List All Current Orders");
    Console.WriteLine("[3] Register A New Customer");
    Console.WriteLine("[4] Create A Customers Order");
    Console.WriteLine("[5] Display Order Details Of A Customer");
    Console.WriteLine("[6] Modify Order Details");
    Console.WriteLine("[7] Process Order");
    Console.WriteLine("[8] Display charges breakdown");
    Console.WriteLine("==========================================");
}

List<Customer> customerList = new List<Customer>();

List<string> ReadCustomers(string file)
{
    List<string> heading = new List<string>();
    using (StreamReader sr = new StreamReader(file))
    {
        string? s = sr.ReadLine(); // read the heading

        if (s != null)
        {
            heading.AddRange(s.Split(','));
        }

        try
        {
            // repeat until end of file
            while ((s = sr.ReadLine()) != null)
            {
                string[] marks = s.Split(',');
                PointCard pc = new PointCard(Convert.ToInt32(marks[4]), Convert.ToInt32(marks[5]), marks[3]);
                DateTime date = DateTime.ParseExact(marks[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime TimeReceived =
                    DateTime.ParseExact("27/10/2023 13:23", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                Customer c = new Customer(marks[0], Convert.ToInt32(marks[1]), date, new Order(123, TimeReceived, null,
                    new List<IceCream>()
                    {
                        new Cone("Cone", 2, new List<Flavour>() { new Flavour("Vanilla", false, 1) },
                            new List<Topping>() { new Topping("Sprinkles") }, false)
                    }), new List<Order>(), pc);
                // Customer c = new Customer(marks[0], Convert.ToInt32(marks[1]), date, new Order(), new List<Order>(), pc);
                customerList.Add(c);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    return heading;
}

List<string> customerHeaders = ReadCustomers(CUSTOMERS_PATH);

// -------------------------------------------------
// list of flavours to check if it is a premium flavour or not in ReadOrderHistory()
List<string> flavours = new List<string> { "vanilla", "chocolate", "strawberry", "sea salt", "ube", "durian" };
List<string> toppings = new List<string> { "sprinkles", "mochi", "sago", "oreos" };


List<string> orderListInterim = new List<string>();
Dictionary<int, string> dippedCone = new Dictionary<int, string>();
Dictionary<int, string> waffleFlavour = new Dictionary<int, string>();

// helper method that reads every record in orders.csv and stores each record in a list
List<string> ReadOrderHistory(string file)
{
    List<string> heading = new List<string>();
    using (StreamReader sr = new StreamReader(file))
    {
        string? s = sr.ReadLine(); // read the heading

        if (s != null)
        {
            heading.AddRange(s.Split(','));
        }

        int indexCount = 0;
        // repeat until end of file
        while ((s = sr.ReadLine()) != null)
        {
            string[] marks = s.Split(',');

            // each string here represents a record in the csv file
            // orderListInterim.Add(
            // $"{marks[0]},{marks[1]},{marks[2]},{marks[3]},{marks[4]},{marks[5]},{marks[7]},{marks[8]},{marks[9]},{marks[10]},{marks[11]},{marks[12]},{marks[13]}");
            orderListInterim.Add(s);
            dippedCone[indexCount] = marks[6];
            waffleFlavour[indexCount] = marks[7];

            indexCount++;
        }
    }

    return heading;
}


// helper method to write order history to customer object 
List<Order> WriteOrderHistory()
{
    List<Order> tempOrderList = new List<Order>();
    for (int j = 0; j < orderListInterim.Count; j++)
    {
        List<IceCream> tempIceCreamList = new List<IceCream>();
        // multiple list to store orders with more than one icecream object
        List<string> multiple = new List<string>();
        multiple.Add(orderListInterim[j]);
        // check if there are other orders matching the same memberid and time received
        for (int i = 0; i < orderListInterim.Count; i++)
        {
            if (i == j)
            {
                continue;
            }

            if (orderListInterim[i].Split(",")[1] == orderListInterim[j].Split(",")[1] &&
                orderListInterim[i].Split(",")[2] == orderListInterim[j].Split(",")[2])
            {
                multiple.Add(orderListInterim[i]);
                orderListInterim.Remove(orderListInterim[i]);
            }
        }


        // loop through multiple orders made by same customer
        foreach (string n in multiple)
        {
            List<Flavour> tempFlavourList = new List<Flavour>();
            List<Topping> tempToppingList = new List<Topping>();
            string[] premiumFlavours = new string[] { "durian", "ube", "sea salt" };
            string[] orderDetails = n.Split(",");

            // add flavours/toppings
            foreach (string detail in orderDetails)
            {
                if (flavours.Contains(detail.ToLower()))
                {
                    // check if it is a premium flavour or not
                    if (premiumFlavours.Contains(detail.ToLower()))
                    {
                        tempFlavourList.Add(new Flavour(detail, true, 1));
                    }
                    else
                    {
                        tempFlavourList.Add(new Flavour(detail, false, 1));
                    }
                }

                else if (toppings.Contains(detail.ToLower()))
                {
                    tempToppingList.Add(new Topping(detail));
                }
            }

            switch (orderDetails[4])
            {
                case "Waffle":
                    tempIceCreamList.Add(new Waffle(orderDetails[4], Convert.ToInt32(orderDetails[5]), tempFlavourList,
                        tempToppingList, waffleFlavour[j]));
                    break;
                case "Cone":
                    tempIceCreamList.Add(new Cone(orderDetails[4], Convert.ToInt32(orderDetails[5]), tempFlavourList,
                        tempToppingList, dippedCone[j] == "TRUE" ? true : false));
                    break;
                case "Cup":
                    tempIceCreamList.Add(new Cup(orderDetails[4], Convert.ToInt32(orderDetails[5]), tempFlavourList,
                        tempToppingList));
                    break;
            }
        }

        string[] orderDetails2 = orderListInterim[j].Split(",");


        // loop through customer object to find a match for each order and add it to the customer's order history
        foreach (Customer c in customerList)
        {
            if (c.MemberId == Convert.ToInt32(orderDetails2[1]))
            {
                DateTime TimeReceived =
                    DateTime.ParseExact(orderDetails2[2], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                DateTime TimeFulfilled =
                    DateTime.ParseExact(orderDetails2[3], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                c.OrderHistory.Add(new Order(Convert.ToInt32(orderDetails2[0]), TimeReceived, TimeFulfilled,
                    tempIceCreamList));
            }
        }
    }

    return tempOrderList;
}

List<string> orderHeaders = ReadOrderHistory(ORDERS_PATH);
WriteOrderHistory();

// helper method to display current order
void DisplayCurrentOrder(Order co)
{
    Console.WriteLine($"{co.Id}, {co.TimeReceived}, {co.TimeFulfilled}");
    Console.WriteLine(co.ToString());
}

// create gold and regular queues
Queue<Order> goldQueue = new Queue<Order>();
Queue<Order> regularQueue = new Queue<Order>();


// process order from queue
void ProcessOrder(Queue<Order> orderQueue)
{
    // dequeue and assign to order object
    Order order = orderQueue.Dequeue();
    Console.WriteLine($"Order ID: {order.Id}");

    double totalOrderSum = 0;
    double mostExpensiveIC = 0;
    double firstOrderSum = order.IceCreamList[0].CalculatePrice();

    foreach (IceCream ic in order.IceCreamList)
    {
        Console.WriteLine(ic.ToString());
        totalOrderSum += ic.CalculatePrice();
        if (ic.CalculatePrice() > mostExpensiveIC)
        {
            mostExpensiveIC = ic.CalculatePrice();
        }
    }

    Console.WriteLine($"Total: ${totalOrderSum}");

    // display membership status & points of the customer
    Customer customerCheckOut = customerList.Find(c => c.CurrentOrder.Id == order.Id);
    Console.WriteLine($"Customer: {customerCheckOut.Name}");
    Console.WriteLine($"Membership Tier: {customerCheckOut.Rewards.Tier}");
    Console.WriteLine($"Membership Points: {customerCheckOut.Rewards.Points}");

    if (customerCheckOut.IsBirthday())
    {
        totalOrderSum -= mostExpensiveIC;
    }

    // check if customer has completed punch card = 10
    if (customerCheckOut.Rewards.PunchCard == 10)
    {
        totalOrderSum -= firstOrderSum;
        customerCheckOut.Rewards.PunchCard = 0;
    }

    Console.WriteLine("======================");
    Console.WriteLine(order.ToString());

    // check if customer is allowed to redeem points 
    if (customerCheckOut.Rewards.Tier != "Ordinary")
    {
        Console.Write("How many points would you like to redeem?: ");
        int redeem = Convert.ToInt32(Console.ReadLine());
        if (redeem <= customerCheckOut.Rewards.Points)
        {
            customerCheckOut.Rewards.Points -= redeem;
            totalOrderSum -= (0.02 * redeem);
        }
        else
        {
            Console.WriteLine("Not enough points!");
        }
    }

    Console.WriteLine($"FINAL BILL: {totalOrderSum:F2}");
    Console.WriteLine("(Press any key to make payment)");
    Console.ReadKey();
    // 1 ice cream = 1 order
    customerCheckOut.Rewards.PunchCard += order.IceCreamList.Count;

    if (customerCheckOut.Rewards.PunchCard % 10 != 0 && customerCheckOut.Rewards.PunchCard > 10)
    {
        // get decimal portion of the number and set it as the punchcard value
        // e.g. 25 -> 25/10 = 2.5 -> (2.5 % 1) * 10 = 5 -> 5 is the punchcard value
        // e.g. 38 -> 38/10 = 3.8 -> (3.8 % 1) * 10 = 8 -> 8 is the punchcard value
        // do this because everytime we hit 10 we reset
        customerCheckOut.Rewards.PunchCard = ((customerCheckOut.Rewards.PunchCard / 10) % 1) * 10;
    }

    // get number of points earned and cast it to int in order to round down before adding
    customerCheckOut.Rewards.Points += (int)(totalOrderSum * .72);
    // check if customer is eligible for upgrade
    if (customerCheckOut.Rewards.Points >= 50 && customerCheckOut.Rewards.Tier != "Gold")
    {
        customerCheckOut.Rewards.Tier = "Silver";
    }
    else if (customerCheckOut.Rewards.Points >= 100)
    {
        customerCheckOut.Rewards.Tier = "Gold";
    }

    Console.WriteLine($"Order {order.Id} has been processed!");
    order.TimeFulfilled = DateTime.Now;
    customerCheckOut.OrderHistory.Add(order);
}

while (true)
{
    DisplayMenu();
    Console.Write("Enter a option: ");
    string option = Console.ReadLine();


    switch (option)
    {
        case "0":
            Console.WriteLine("Goodbye! Have A Nice Day");
            break;

        case "1":
            // print headers
            Console.WriteLine("{0,-10}  {1,-10}   {2,-10}  {3,-10}  {4,-10}  {5,-10}",
                customerHeaders[0], customerHeaders[1], customerHeaders[2], customerHeaders[3], customerHeaders[4],
                customerHeaders[5]);

            // list all customers
            foreach (Customer c in customerList)
            {
                Console.WriteLine("{0,-10}  {1,-10}   {2,-10}  {3,-16}  {4,-16}  {5,-10}",
                    c.Name, c.MemberId, c.Dob, c.Rewards.Tier, c.Rewards.Points, c.Rewards.PunchCard);
            }

            break;

        case "2":
            foreach (Order o in regularQueue)
            {
                Console.WriteLine(o.ToString());
            }

            foreach (Order o in goldQueue)
            {
                Console.WriteLine(o.ToString());
            }

            break;

        case "3":
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            Console.Write("Enter customer member ID: ");
            int memberID = Convert.ToInt32(Console.ReadLine());
            DateTime Dob;
            try
            {
                Console.Write("Enter customer date of birth (dd/mm/yyyy): ");
                Dob = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid date format!");
                break;
            }

            string tier;
            try
            {
                Console.Write("Enter customer membership tier: ");
                tier  = Console.ReadLine();
                if (tier != "Gold" && tier != "Silver" && tier != "Ordinary")
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid tier!");
                break;
            }

            Console.Write("Enter customer membership points: ");
            int points = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter customer punchcard:");
            int punchCard = Convert.ToInt32(Console.ReadLine());


            // Create a new PointCard object
            PointCard pointCard = new PointCard
            {
                Tier = tier,
                Points = points,
                PunchCard = punchCard,
            };
            // Create a new customer object with the given information
            Customer customer = new Customer(name, memberID, Dob, null, new List<Order>(), pointCard);


            // Append the customer information to the customers.csv file
            string customerInfo =
                $"{customer.Name},{customer.MemberId},{customer.Dob.ToString("dd/MM/yyyy")},{customer.Rewards}\n";

            // append to customer list
            customerList.Add(customer);
            
            File.AppendAllText(CUSTOMERS_PATH, customerInfo);
            
            // using (StreamWriter sw = new StreamWriter("customers.csv", true))
            //     
            // {
            //     var writer = new csvwriter(sw);
            //     Console.WriteLine(customerInfo);
            //     sw.Write(customerInfo);
            //     sw.Close();
            // }

            // Display a message to indicate registration status
            Console.WriteLine("Customer registration successful!");
            break;

        case "4":
            // errorStatus of new ice cream creation
            bool errorStatus = false;
            // list customer
            foreach (Customer c in customerList)
            {
                Console.WriteLine(c.ToString());
            }

            // prompt user to select a customer
            Console.Write("Enter a customer name: ");
            string chosenCustomer4 = Console.ReadLine();

            try
            {
                Customer customer_inputted = customerList.Find(c => c.Name == chosenCustomer4);
                // check if customer is valid
                // assigning name property to variable makes a call, if it is null, it will throw an error
                string checkValidity = customer_inputted.Name;
                List<IceCream> newOrderICList = new List<IceCream>();
                // create order object
                Order newOrder = new Order(new Random().Next(), DateTime.Now, null, newOrderICList);
                // user option controls if the user would like to make a new order or not
                bool userOption = false;
                // prompt user to enter new ice cream order
                do
                {
                    // always reset userOption to false so that if the user just presses enter below, it will default to no
                    userOption = false;
                    try
                    {
                        IceCream userIceCream = newOrder.newIceCream();
                        newOrderICList.Add(userIceCream);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid option! Try again!");
                        errorStatus = true;
                        break;
                    }


                    Console.Write("Would you like to add another ice cream? (y/N): ");
                    string anotherIC = Console.ReadLine();
                    if (anotherIC == "y")
                    {
                        userOption = true;
                    }
                } while (userOption);

                if (errorStatus)
                {
                    // restart order creation
                    Console.WriteLine("Error has occured. Please restart order creation.");
                    break;
                }

                newOrder.IceCreamList = newOrderICList;
                customer_inputted.CurrentOrder = newOrder;

                if (customer_inputted.Rewards.Tier == "Gold")
                {
                    goldQueue.Enqueue(newOrder);
                    Console.WriteLine("Order has been made successfully!");
                }
                else
                {
                    regularQueue.Enqueue(newOrder);
                    Console.WriteLine("Order has been made successfully!");
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Customer not found!");
            }

            break;

        case "5":
            // list customer
            foreach (Customer c in customerList)
            {
                Console.WriteLine(c.ToString());
            }


            // prompt user to select a customer
            Console.Write("Enter a customer name: ");
            string name1 = Console.ReadLine();
            try
            {
                Customer customer_inputted = customerList.Find(c => c.Name == name1);
                // retrieve all the orders of the customer
                List<Order> orderList = customer_inputted.OrderHistory;
                // check if the current order is null
                if (customer_inputted.CurrentOrder.TimeReceived.Year != 0001)
                {
                    orderList.Add(customer_inputted.CurrentOrder);
                }

                // display the order details
                foreach (Order o in orderList)
                {
                    Console.WriteLine($"{o.Id}, {o.TimeReceived}, {o.TimeFulfilled}");
                    Console.WriteLine(o.ToString());
                }

                if (orderList.Count == 0)
                {
                    Console.WriteLine("No orders found!");
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Customer not found!");
            }


            break;

        case "6":
            // list the customer names
            foreach (Customer c in customerList)
            {
                Console.WriteLine(c.ToString());
            }

            // prompt user to select a customer and retrieve the selected customer’s current order
            Console.Write("Enter a customer name: ");
            string name2 = Console.ReadLine();
            try
            {
                Customer customer2 = customerList.Find(c => c.Name == name2);
                Order currentOrder = customer2.CurrentOrder;
                // retrieve all the ice cream objects of the order
                for (int j = 0; j < currentOrder.IceCreamList.Count; j++)
                {
                    Console.WriteLine($"{j}. {currentOrder.IceCreamList[j].ToString()}");
                }


                // prompt user to make a choice
                Console.Write(
                    "[1] choose an existing ice cream object to modify\n[2] add an entirely new ice cream object to the order\n[3] choose an existing ice cream object to delete from the order\nWhat option?: ");
                try
                {
                    int option2 = Convert.ToInt32(Console.ReadLine());
                    switch (option2)
                    {
                        case 1:
                            // choose an existing ice cream object to modify
                            currentOrder.ModifyIceCream(1);
                            DisplayCurrentOrder(currentOrder);
                            break;
                        case 2:
                            currentOrder.ModifyIceCream(2);
                            DisplayCurrentOrder(currentOrder);
                            break;
                        case 3:
                            currentOrder.ModifyIceCream(3);
                            DisplayCurrentOrder(currentOrder);
                            break;
                        default:
                            Console.WriteLine("Invalid option! Try again!");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid option! Try again!");
                    break;
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Customer not found!");
                break;
            }

            break;

        case "7":
            Console.WriteLine("Process Order");
            Console.WriteLine("==============");
            // check if gold queue is empty, if it is, then process regular queue
            if (goldQueue.Count == 0)
            {
                // check if regular queue is empty, if it is, then print no orders
                if (regularQueue.Count == 0)
                {
                    Console.WriteLine("No orders to process!");
                    break;
                }

                // process order
                ProcessOrder(regularQueue);
            }
            else
            {
                ProcessOrder(goldQueue);
            }

            break;
        case "8":
            void DisplayMonthlyChargedAmounts(int year)
            {
                Dictionary<int, double> monthlyAmounts = new Dictionary<int, double>();

                // Loop through fulfilled orders for the inputted year
                foreach (Customer customer in customerList)
                {
                    foreach (Order order in customer.OrderHistory)
                    {
                        if (order.TimeReceived.Year == year)
                        {
                            int month = order.TimeReceived.Month;
                            double orderAmount = 0;

                            // Calculate the total amount for the order
                            foreach (IceCream iceCream in order.IceCreamList)
                            {
                                orderAmount += iceCream.CalculatePrice();
                            }

                            // Add the order amount to the monthly total
                            if (monthlyAmounts.ContainsKey(month))
                            {
                                monthlyAmounts[month] += orderAmount;
                            }
                            else
                            {
                                monthlyAmounts.Add(month, orderAmount);
                            }
                        }
                    }
                }

                List<string> months = new List<string>{"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
                int i = 0;

                // Display the monthly charged amounts breakdown
                foreach (var kvp in monthlyAmounts)
                {
                    Console.WriteLine($"{months[kvp.Key]} {year}: ${kvp.Value}");
                    i++;
                }

                // Calculate and display the total charged amount for the year
                double totalAmount = monthlyAmounts.Values.Sum();
                Console.WriteLine("Total Charged Amount for Year {0}: ${1}", year, totalAmount);
            }

            // Prompt the user for the year
            Console.Write("Enter the year: ");
            int inputYear = Convert.ToInt32(Console.ReadLine());

            // Display monthly charged amounts breakdown
            DisplayMonthlyChargedAmounts(inputYear);

            break;
        default:
            Console.WriteLine("Invalid option! Try again!");
            break;
    }
}