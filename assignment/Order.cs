// ID: S10255981, S10257966
// Name: Rainnen, Ethan
using Microsoft.VisualBasic;
using static System.Formats.Asn1.AsnWriter;
using System.Collections.Generic;
using System;

namespace assignment;

internal class Order
{
	private int id;
	private DateTime timeReceived;
	private DateTime timeFulfilled;
	private List<IceCream> iceCreamList = new List<IceCream>();
	private Dictionary<string, double> flavourList = new Dictionary<string, double>();
	private string path = "flavours.csv";
	// read flavour data from flavours.csv
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
				flavourList.Add(marks[0], Convert.ToDouble(marks[1]));
			}
		}	
	}
	



	public int Id
	{
		get { return id; }
		set { id = value; }
	}

	public DateTime TimeReceived
	{
		get { return timeReceived; }
		set { timeReceived = value; }
	}

	public DateTime TimeFulfilled
	{
		get { return timeFulfilled; }
		set { timeFulfilled = value; }
	}

	public void AddIceCream(IceCream iceCream)
	{
		iceCreamList.Add(iceCream);
	}

	public void DeleteIceCream(int index)
	{
		iceCreamList.RemoveAt(index);
	}

	public Order()
	{
		
	}

	public Order(int id, DateTime timeReceived)
	{
		Id = id;
		TimeReceived = timeReceived;
		
	}


	public void ModifyIceCream(int index)
	{

		Console.WriteLine("What would you like to modify?");
		string choiceMenu = "1. Option\n2. Scoops\n3. Flavours\n4. Toppings\n5. Dipped Cone\n6. Waffle Flavour\n7. Exit";
		Console.WriteLine(choiceMenu);

		Console.Write("Enter your choice: ");
		int choice = Convert.ToInt32(Console.ReadLine());
		switch (choice)
		{
			case 1:
				Console.WriteLine("Enter new option: ");
				string option = Console.ReadLine();
				iceCreamList[index].Option = option;
				break;
			case 2:
				Console.WriteLine("Enter new number of scoops: ");
				int scoops = Convert.ToInt32(Console.ReadLine());
				iceCreamList[index].Scoops = scoops;
				break;
			case 3:
				Console.WriteLine("Enter new flavours split by a ',' : ");
				string[] flavours = new string[iceCreamList[index].Scoops];
				flavours = Console.ReadLine().Split(',');
				iceCreamList[index].Flavours.Clear();
				foreach (string flavour in flavours)
				{
					iceCreamList[index].Flavours.Add(new Flavour(flavour, flavourClass[flavour], 1));
				}

				break;
			case 4:
				Console.WriteLine("Enter new toppings: ");
				string[] toppings = new string[4];
				toppings = Console.ReadLine().Split(',');
				iceCreamList[index].Toppings.Clear();
				foreach (string topping in toppings)
				{
					iceCreamList[index].Toppings.Add(new Topping(topping));
				}
				break;
			case 5:
				Console.WriteLine("Enter new dipped cone: ");
				
				if (iceCreamList[index] is Cone cone)
				{
					cone.Dipped = true;
				}
				else
				{
					Console.WriteLine("Invalid choice");
				}
				break;
			case 6:
				Console.WriteLine("Enter new waffle flavour: ");
				if (iceCreamList[index] is Waffle waffle)
				{
					waffle.WaffleFlavour = Console.ReadLine();
				}
				else
				{
					Console.WriteLine("Invalid choice");
				}
				
				break;
			case 7:
				break;
			default:
				Console.WriteLine("Invalid choice");
				break;
		}
		
	}

	public double CalculateTotal()
	{
		// calculate the total price of the order
		double total = 0;
		foreach (IceCream iceCream in iceCreamList)
		{
			total += iceCream.CalculatePrice();
		}
		return total;
	}
}