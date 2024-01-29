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

List<string> flavours = new List<string> { "vanilla", "chocolate", "strawberry", "sea salt", "ube", "durian" };
List<string> toppings = new List<string> { "sprinkles", "mochi", "sago", "oreos" };


List<string> orderListInterim = new List<string>();
Dictionary<int, string> dippedCone = new Dictionary<int, string>();
Dictionary<int, string> waffleFlavour = new Dictionary<int, string>();

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
List<Order> IceCreams()
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
IceCreams();

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
            foreach (var kvp in dippedCone)
            {
                Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
            }

            break;

        case "4":
            foreach (string n in orderListInterim)
            {
                Console.WriteLine(n);
            }

            break;

        case "5":
            // list all customers
            // foreach (Customer c in customerList)
            // {
            //     Console.WriteLine(c.Name);
            // }
            //
            // // prompt user to select a customer and retrieve the selected customer
            //
            // Console.Write("Enter a customer name: ");
            // string name = Console.ReadLine();
            // Customer customer = customerList.Find(c => c.Name == name);
            // Console.WriteLine(customer.ToString());
            // // retrieve all the order objects of the customer, past and current
            // List<Order> customerOrderList = customer.OrderHistory;
            // orderList.Add(customer.CurrentOrder);
            // // for each order, display all the details of the order including datetime received, datetime
            // // fulfilled (if applicable) and all ice cream details associated with the order
            // foreach (Order o in orderList)
            // {
            //     Console.WriteLine(o.ToString());
            // }

            break;

        case "6":
            // list the customer naems
            foreach (Customer c in customerList)
            {
                Console.WriteLine(c.Name);
            }

            // prompt user to select a customer and retrieve the selected customer’s current order
            Console.Write("Enter a customer name: ");
            string name2 = Console.ReadLine();
            Customer customer2 = customerList.Find(c => c.Name == name2);
            Order order = customer2.CurrentOrder;
            // retrieve all the ice cream objects of the order
            foreach (IceCream i in order.IceCreamList)
            {
                Console.WriteLine(i.ToString());
            }

            break;

        default:
            Console.WriteLine("Invalid option! Try again!");
            break;
    }
}