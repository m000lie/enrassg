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
	private DateTime? timeFulfilled;
	private List<IceCream> iceCreamList = new List<IceCream>();
	private Dictionary<string, bool> flavourClass = new Dictionary<string, bool>();



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

	public DateTime? TimeFulfilled
	{
		get { return timeFulfilled; }
		set { timeFulfilled = value; }
	}

	public List<IceCream> IceCreamList
	{
		get { return iceCreamList; }
		set { iceCreamList = value; }
	}
	
	public void AddIceCream(IceCream iceCream)
	{
		iceCreamList.Add(iceCream);
	}

	public void DeleteIceCream(int index)
	{
		iceCreamList.RemoveAt(index);
	}

	private void initFlavourClass()
	{
		flavourClass.Add("Vanilla", false);
		flavourClass.Add("Chocolate", false);
		flavourClass.Add("Strawberry", false);
		flavourClass.Add("Durian", true);
		flavourClass.Add("Ube", true);
		flavourClass.Add("Sea salt", true);
	}
	public Order()
	{
		initFlavourClass();
	}

	public Order(int id, DateTime timeReceived, DateTime? timeFulfilled, List<IceCream> iceCreamList)
	{
		Id = id;
		TimeReceived = timeReceived;
		TimeFulfilled = timeFulfilled;
		IceCreamList = iceCreamList;
		initFlavourClass();
	}
	

	// to implement
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
	
	public override string ToString()
	{
		string s = "";
		foreach (IceCream iceCream in iceCreamList)
		{
			s += iceCream.ToString() + "\n";
		}
		return s;
	}
}