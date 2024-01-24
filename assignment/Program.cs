// ID: S10255981, S10257966
// Name: Rainnen, Ethan
using assignment;

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
    }
}