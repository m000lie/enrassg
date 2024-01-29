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



while (true)
{
    DisplayMenu();
    Console.Write("Enter a option: ");
    int option = Convert.ToInt32(Console.ReadLine());
    
    switch (option)
    {
        case 0:
            Console.WriteLine("Goodbye! Have A Nice Day");
            break;

        case 1:
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
            break;

        case 4:
            break;

        case 5:
            break;

        case 6:
            break;
            break;
    }

}