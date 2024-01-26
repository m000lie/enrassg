// ID: S10255981, S10257966
// Name: Rainnen, Ethan
using assignment;
using System;
using System.IO;


// helper method
// read flavour data from flavours.csv

Dictionary<string, double> flavourDict = new Dictionary<string, double>();
string path = "flavours.csv";
void ReadFlavourData()
{
	using (StreamReader sr = new StreamReader(path))
	{
		string? s = sr.ReadLine(); // read the heading
								   // save headers
		if (s != null)
		{
			string[] heading = s.Split(',');
		}

		while ((s = sr.ReadLine()) != null)
		{
			string[] marks = s.Split(',');
			flavourDict.Add(marks[0], Convert.ToDouble(marks[1]));
		}
	}
}

bool CheckPremium(Dictionary<string, double> flavourDict, string flavour)
{
	if (flavourDict[flavour] == 0)
	{
		return false;
	}
	else
	{

		return true;
	}
}


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
							   // read headers
	if (s != null)
	{
		string[] heading = s.Split(',');

	}

	while ((s = sr.ReadLine()) != null)
	{
		string[] marks = s.Split(',');
		// check if order is already present
		foreach (Order order in orderList)
		{
			if (order.Id == Convert.ToInt32(marks[1]))
			{
				switch (marks[4])
				{
					case "Cone":
						order.AddIceCream(new Cone(marks[4], Convert.ToInt32(marks[5]), new List<Flavour>().AddRange(new Flavour(marks[8], CheckPremium(flavourDict, marks[8]), 1), marks[9], marks[10]), new List<Topping>().AddRange());
						break;
					case "Cup":
						order.AddIceCream(new Cup(marks[4], Convert.ToInt32(marks[5])));
						break;
					case "Waffle":
						order.AddIceCream(new Waffle(marks[4], Convert.ToInt32(marks[5])));
						break;
				}
				order.AddIceCream(new IceCream(marks[4], Convert.ToInt32(marks[5])));
			}
		}
		Order o = new Order(Convert.ToInt32(marks[1]), Convert.ToDateTime(marks[2]), Convert.ToDateTime(marks[3], );
		orderList.Add(o);

	}
}

foreach (Order order in orderList)
{
	Console.WriteLine(order.ToString());
}
