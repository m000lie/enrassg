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

    // to implement
    public void ModifyIceCream(int index)
    {
        // implement this method to modify the ice cream at the given index: option, scoops, flavours, topppings, dipped cone(if possible)
        // do this now
        // implement this method to modify the ice cream at the given index: option, scoops, flavours, topppings, dipped cone(if possible)
        // do this now
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
				Console.WriteLine("Enter new flavours: ");
				string flavours = Console.ReadLine();
                iceCreamList[index].Flavours = flavours;
				break;
			case 4:
				Console.WriteLine("Enter new toppings: ");
				string toppings = Console.ReadLine();
                iceCreamList[index].Toppings = toppings;
				break;
			case 5:
				Console.WriteLine("Enter new dipped cone: ");
				bool dipped = Convert.ToBoolean(Console.ReadLine()); 
                iceCreamList[index].Dipped = dipped;
				break;
			case 6:
				Console.WriteLine("Enter new waffle flavour: ");
				string waffleFlavour = Console.ReadLine();
                iceCreamList[index].WaffleFlavour = waffleFlavour;
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