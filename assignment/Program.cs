// ID: S10255981, S10257966
// Name: Rainnen, Ethan
using assignment;

// feature 1
List<Customer> customerList = new List<Customer>();
string File = "customers.csv";
using (StreamReader sr = new StreamReader(File))
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
        Customer c = new Customer(marks[0], Convert.ToInt32(marks[1]), Convert.ToDateTime(marks[2]), null, null, pc);
        customerList.Add(c);
        // print details of customer
        Console.WriteLine("{0,-10}  {1,-10}   {2,-10}  {3,-10}  {4,-10}  {5,-10}",
            marks[0], marks[1], marks[2], marks[3], marks[4], marks[5]);
    }
}

// feature 2 - list all current orders
List<Order> orderList = new List<Order>();
string File2 = "orders.csv";
using (StreamReader sr = new StreamReader(File2))
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
        Order o = new Order(Convert.ToInt32(marks[1]), Convert.ToDateTime(marks[2]));
        orderList.Add(o);
        
    }
}

foreach (Order order in orderList)
{
    Console.WriteLine(order.ToString());
}
