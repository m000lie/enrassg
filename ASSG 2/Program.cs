//==========================================================
// Student Number : S10257966
// Student Name : Ethan Chan
// Partner Name : Rainnen Wong Shin Zer
//==========================================================

using assignment;

string File = "customer.csv";

using (StreamReader sr = new StreamReader(File))
{
    string? s = sr.ReadLine();
    //Display Heading?
    if (s != null)
    {
        string[] heading = s.Split(',');
        Console.WriteLine("{0,10}  {1,10}  {2,10}  {3,10}  {4,10}  {5,10}",
            heading[0], heading[1], heading[2], heading[3], heading[4],
            heading[5]);
    }

    while (s != null)
    {
        string[] info = s.Split(",");
        Console.WriteLine(info.ToString());
    }
}




Console.Write("Enter customer name: ");
string name = Console.ReadLine();

Console.Write("Enter member ID: ");
string memberID = Console.ReadLine();

// Validate ID number format (if applicable)

Console.Write("Enter date of birth (YYYY-MM-DD): ");
DateTime dob;
if (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
{
    Console.WriteLine("Invalid date format. Please enter YYYY-MM-DD.");
    return; // Exit if invalid date
}

// Create customer and pointcard objects
Customer customer = new Customer { Name = name, MemberId = memberID, Dob = dob };
customer.Rewards = new Rewards();

// Append customer information to CSV file
string csvPath = "customers.csv";
try
{
    using (StreamWriter writer = new StreamWriter(csvPath, true))
    {
        writer.WriteLine($"{customer.Name},{customer.MemberId},{customer.dob:yyyy-MM-dd}");
    }
    Console.WriteLine("Customer registered successfully!");
}
catch (IOException ex)
{
    Console.WriteLine("Error writing to file: {0}", ex.Message);
}


