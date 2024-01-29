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

bool KeepRunning = true;

while (KeepRunning)
{
    DisplayMenu();
    Console.Write("Enter a option: ");
    string option = Console.ReadLine();
    int number;

    bool isParsed = Int32.TryParse(option, out number);

    if (isParsed)
    {
        Console.WriteLine();
        switch (number)
        {
            case 0:
                Console.WriteLine("Goodbye! Have A Nice Day");
                KeepRunning = false;
                break;

            case 1:
                List<Customer> customerList = new List<Customer>();
                using (StreamReader sr = new StreamReader("customers.csv"))
                {
                    string? s = sr.ReadLine(); // read the heading
                                               // display the heading
                    if (s != null)
                    {
                        string[] heading = s.Split(',');
                        Console.WriteLine("{0,-10}  {1,-10}   {2,-10}  {3,-10}  {4,-10}  {5,-10}",
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
                        customerList.Add(c);
                        // print details of customer
                        Console.WriteLine("{0,-10}  {1,-10}   {2,-10}  {3,-16}  {4,-16}  {5,-10}",
                            marks[0], marks[1], date, marks[3], marks[4], marks[5]);
                    }
                }
                break;

            case 2:
                break;

            case 3:
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
                string customerInfo = $"{customer.Name},{customer.MemberId},{customer.Dob.ToString("dd-MM-yyyy")},{customer.Rewards}";
                using (StreamWriter sw = new StreamWriter("customers.csv", true))
                {
                    sw.WriteLine(customerInfo);
                }

                // Display a message to indicate registration status
                Console.WriteLine("Customer registration successful!");
                break;

            case 4:
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
                    Order order = new Order(selectedCustomer);

                    // Now you can add items to the order...
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
                break;

            case 5:
                break;

            case 6:
                break;

            default:
                Console.WriteLine("Invalid Option! Try Again");
                break;
        }
    }
    else
    {
        Console.WriteLine("Invalid Option! Try Again");
    }

    Console.WriteLine();
}