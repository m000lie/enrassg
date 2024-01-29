// ID: S10255981, S10257966
// Name: Rainnen, Ethan

using assignment;
using System.Globalization;


void DisplayMenu()
{
    Console.WriteLine("==========================================");
    Console.WriteLine("[1] List All Customers");
    Console.WriteLine("[2] List All Current Orders");
    Console.WriteLine("[3] Register A New Customer");
    Console.WriteLine("[4] Create A Customers Order");
    Console.WriteLine("[5] Display Order Details Of A Customer");
    Console.WriteLine("[6] Modify Order Details");
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

        // repeat until end of file
        while ((s = sr.ReadLine()) != null)
        {
            string[] marks = s.Split(',');
            PointCard pc = new PointCard(Convert.ToInt32(marks[4]), Convert.ToInt32(marks[5]), marks[3]);
            DateTime date = DateTime.ParseExact(marks[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //Customer c = new Customer(marks[0], Convert.ToInt32(marks[1]), Convert.ToDateTime(marks[2]), null, null, pc);
            Customer c = new Customer(marks[0], Convert.ToInt32(marks[1]), date, new Order(), new List<Order>(), pc);
            customerList.Add(c);
        }
    }

    return heading;
}

List<string> customerHeaders = ReadCustomers("customers.csv");

// -------------------------------------------------
// list of flavours to check if it is a premium flavour or not in ReadOrderHistory()
List<string> flavours = new List<string> { "vanilla", "chocolate", "strawberry", "sea salt", "ube", "durian" };


List<string> orderListInterim = new List<string>();
Dictionary<int, string> dippedCone = new Dictionary<int, string>();
Dictionary<int, string> waffleFlavour = new Dictionary<int, string>();

// reads every record in orders.csv and stores each record in a list
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


// write order history to customer object 
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


        // handle multiple orders
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

                else
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

List<string> orderHeaders = ReadOrderHistory("orders.csv");
WriteOrderHistory();

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


            break;

        case "3":
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            Console.Write("Enter customer member ID: ");
            int memberID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter customer date of birth (dd-mm-yyyy): ");
            DateTime Dob = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

            Console.WriteLine("Enter customer membership tier: ");
            string Tier = Console.ReadLine();

            Console.WriteLine("Enter customer membership points: ");
            int Points = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter customer punchcard:");
            int PunchCard = Convert.ToInt32(Console.ReadLine());

            // Create a new customer object with the given information
            Customer customer = new Customer
            {
                Name = name,
                MemberId = memberID,
                Dob = Dob
            };

            // Create a new PointCard object
            PointCard pointCard = new PointCard
            {
                Tier = Tier,
                Points = Points,
                PunchCard = PunchCard,
            };

            // Assign the PointCard object to the customer
            customer.Rewards = pointCard;

            // Append the customer information to the customers.csv file
            string customerInfo =
                $"{customer.Name},{customer.MemberId},{customer.Dob.ToString("dd-MM-yyyy")},{customer.Rewards}";
            using (StreamWriter sw = new StreamWriter("customers.csv", true))
            {
                sw.WriteLine(customerInfo);
            }

            // Display a message to indicate registration status
            Console.WriteLine("Customer registration successful!");
            break;

        case "4": // NOT FINISHED
            int i = 1;
            List<Customer> customerList2 = new List<Customer>();
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                string? s = sr.ReadLine(); // read the heading
                // display the heading
                if (s != null)
                {
                    string[] heading = s.Split(',');
                    Console.WriteLine("    {0,-10}  {1,-10}   {2,-10}  {3,-10}  {4,-10}  {5,-10}",
                        heading[0], heading[1], heading[2], heading[3], heading[4], heading[5]);
                    // repeat until end of file
                }

                while ((s = sr.ReadLine()) != null)
                {
                    string[] marks = s.Split(',');
                    PointCard pc = new PointCard(Convert.ToInt32(marks[4]), Convert.ToInt32(marks[5]), marks[3]);
                    DateTime date = DateTime.ParseExact(marks[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //Customer c = new Customer(marks[0], Convert.ToInt32(marks[1]), Convert.ToDateTime(marks[2]), null, null, pc);
                    Customer c = new Customer(marks[0], Convert.ToInt32(marks[1]), date, null, null, pc);
                    customerList2.Add(c);
                    // print details of customer
                    Console.WriteLine("{0,0}. {1,-10}  {2,-10}   {3,-10}  {4,-16}  {5,-16}  {6,-10}",
                        i, marks[0], marks[1], date, marks[3], marks[4], marks[5]);
                    i++;
                }
            }

            Console.Write("Please select a customer:");
            Console.WriteLine();

            string customerId = Console.ReadLine();

            // Find the selected customer
            Customer selectedCustomer = customerList2.Find(c => c.MemberId == Convert.ToInt32(customerId));

            if (selectedCustomer != null)
            {
                Console.WriteLine($"Selected customer: {selectedCustomer.Name}");

                // Create a new order for the selected customer
                // Order order = new Order(selectedCustomer); <-- ethan there is something wrong with this line 

                // Now you can add items to the order...
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }

            break;

        case "5": // NOT DONE


            break;

        case "6": // NOT DONE
            // list the customer naems
            foreach (Customer c in customerList)
            {
                Console.WriteLine(c.Name);
            }

            // prompt user to select a customer and retrieve the selected customer’s current order
            Console.Write("Enter a customer name: ");
            string name2 = Console.ReadLine();
            Customer customer2 = customerList.Find(c => c.Name == name2);
            Order currentOrder = customer2.CurrentOrder;
            // retrieve all the ice cream objects of the order
            foreach (IceCream ic in currentOrder.IceCreamList)
            {
                Console.WriteLine(ic.ToString());
            }

            break;

        default:
            Console.WriteLine("Invalid option! Try again!");
            break;
    }
}