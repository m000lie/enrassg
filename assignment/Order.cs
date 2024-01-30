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

    public void ModifyIceCream(int choice)
    {
        switch (choice)
        {
            case 1:
                Console.Write("Enter the index of the ice cream you want to modify: ");

                int index = Convert.ToInt32(Console.ReadLine());
                try
                {
                    IceCream tempHolder = iceCreamList[index];


                    // input option
                    Console.Write("Enter new option: ");
                    string newOption = Console.ReadLine();
                    if (newOption.ToLower() == "cup" || newOption.ToLower() == "cone" ||
                        newOption.ToLower() == "waffle")
                    {
                        tempHolder.Option = newOption;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option");
                    }

                    // input scoops
                    Console.Write("Enter number of scoops: ");
                    int newScoops = Convert.ToInt32(Console.ReadLine());

                    // input flavour
                    Console.Write("Enter new flavours (max: 3): ");
                    string[] flavours = new string[newScoops]; // array size = scoop because each scoop has a flavour
                    flavours = Console.ReadLine().Split(','); // add user input to flavours array
                    List<Flavour> newFlavours = new List<Flavour>();
                    foreach (string f in flavours)
                    {
                        newFlavours.Add(new Flavour(f, flavourClass[f], 1));
                    }

                    // input toppings
                    Console.Write("Enter new toppings (max: 4): ");
                    string[] toppings = new string[4];
                    toppings = Console.ReadLine().Split(','); // add user input to toppings array
                    List<Topping> newToppings = new List<Topping>();
                    foreach (string t in toppings)
                    {
                        newToppings.Add(new Topping(t));
                    }

                    if (newOption.ToLower() == "cone")
                    {
                        // prompt user for dipped cone or not
                        Console.Write("Dipped cone? (y/n): ");
                        string newDipped = Console.ReadLine();

                        if (newDipped.ToLower() == "y")
                        {
                            iceCreamList[index] = new Cone(newOption, newScoops, newFlavours, newToppings, true);
                        }
                        else if (newDipped.ToLower() == "n")
                        {
                            iceCreamList[index] = new Cone(newOption, newScoops, newFlavours, newToppings, false);
                        }
                        else
                        {
                            Console.WriteLine("invalid option!!! ");
                        }
                    }

                    else if (newOption.ToLower() == "cup")
                    {
                        iceCreamList[index] = new Cup(newOption, newScoops, newFlavours, newToppings);
                    }

                    else if (newOption.ToLower() == "waffle")
                    {
                        Console.Write("Enter new waffle flavour: ");
                        string newWaffleFlavour = Console.ReadLine();
                        iceCreamList[index] =
                            new Waffle(newOption, newScoops, newFlavours, newToppings, newWaffleFlavour);
                    }
                    else
                    {
                        Console.WriteLine("Invalid option");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid index");
                }

                break;

            case 2:
                // input option
                Console.Write("Cup, Cone, or Waffle: ");
                string option = Console.ReadLine();

                // input scoops
                Console.Write("Enter number of scoops (max: 3): ");
                int scoops = Convert.ToInt32(Console.ReadLine());

                // input flavour
                Console.Write("Enter new flavours (max: 3): ");
                string[] flavours2 = new string[scoops]; // array size = scoop because each scoop has a flavour
                flavours2 = Console.ReadLine().Split(','); // add user input to flavours array
                List<Flavour> newFlavours2 = new List<Flavour>();
                foreach (string f in flavours2)
                {
                    newFlavours2.Add(new Flavour(f, flavourClass[f], 1));
                }

                // input toppings
                Console.Write("Enter new toppings (max: 4): ");
                string[] toppings2 = new string[4];
                toppings2 = Console.ReadLine().Split(','); // add user input to toppings array
                List<Topping> newToppings2 = new List<Topping>();
                foreach (string t in toppings2)
                {
                    newToppings2.Add(new Topping(t));
                }

                if (option.ToLower() == "cone")
                {
                    // prompt about dipped cone
                    Console.Write("Dipped cone? (y/n): ");
                    string newDipped = Console.ReadLine();
                    if (newDipped.ToLower() == "y")
                    {
                        iceCreamList.Add(new Cone(option, scoops, newFlavours2, newToppings2, true));
                    }
                    else if (newDipped.ToLower() == "n")
                    {
                        iceCreamList.Add(new Cone(option, scoops, newFlavours2, newToppings2, false));
                    }
                    else
                    {
                        Console.WriteLine("invalid option!!! ");
                    }
                }

                else if (option.ToLower() == "waffle")
                {
                    // prompt for waffle flavour
                    Console.Write("Enter new waffle flavour: ");
                    string newWaffleFlavour = Console.ReadLine();
                    iceCreamList.Add(new Waffle(option, scoops, newFlavours2, newToppings2, newWaffleFlavour));
                }
                else
                {
                    iceCreamList.Add(new Cup(option, scoops, newFlavours2, newToppings2));
                }

                break;

            case 3:
                Console.Write("Enter the ice cream index you want to delete: ");
                int deleteIndex = Convert.ToInt32(Console.ReadLine());

                try
                {
                    if (iceCreamList.Count != 1)
                    {
                        iceCreamList.RemoveAt(deleteIndex);
                    }
                    else
                    {
                        Console.WriteLine("Cannot have zero items in an order!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid index");
                }

                break;
        }
    }
    // to implement
    // public void ModifyIceCream(int index)
    // {
    //
    // 	Console.WriteLine("What would you like to modify?");
    // 	string choiceMenu = "1. Option\n2. Scoops\n3. Flavours\n4. Toppings\n5. Dipped Cone\n6. Waffle Flavour\n7. Exit";
    // 	Console.WriteLine(choiceMenu);
    //
    // 	Console.Write("Enter your choice: ");
    // 	int choice = Convert.ToInt32(Console.ReadLine());
    // 	switch (choice)
    // 	{
    // 		case 1:
    // 			Console.WriteLine("Enter new option: ");
    // 			string option = Console.ReadLine();
    // 			iceCreamList[index].Option = option;
    // 			break;
    // 		case 2:
    // 			Console.WriteLine("Enter new number of scoops: ");
    // 			int scoops = Convert.ToInt32(Console.ReadLine());
    // 			iceCreamList[index].Scoops = scoops;
    // 			break;
    // 		case 3:
    // 			Console.WriteLine("Enter new flavours split by a ',' : ");
    // 			string[] flavours = new string[iceCreamList[index].Scoops];
    // 			flavours = Console.ReadLine().Split(',');
    // 			iceCreamList[index].Flavours.Clear();
    // 			foreach (string flavour in flavours)
    // 			{
    // 				iceCreamList[index].Flavours.Add(new Flavour(flavour, flavourClass[flavour], 1));
    // 			}
    //
    // 			break;
    // 		case 4:
    // 			Console.WriteLine("Enter new toppings: ");
    // 			string[] toppings = new string[4];
    // 			toppings = Console.ReadLine().Split(',');
    // 			iceCreamList[index].Toppings.Clear();
    // 			foreach (string topping in toppings)
    // 			{
    // 				iceCreamList[index].Toppings.Add(new Topping(topping));
    // 			}
    // 			break;
    // 		case 5:
    // 			Console.WriteLine("Enter new dipped cone: ");
    // 			
    // 			if (iceCreamList[index] is Cone cone)
    // 			{
    // 				cone.Dipped = true;
    // 			}
    // 			else
    // 			{
    // 				Console.WriteLine("Invalid choice");
    // 			}
    // 			break;
    // 		case 6:
    // 			Console.WriteLine("Enter new waffle flavour: ");
    // 			if (iceCreamList[index] is Waffle waffle)
    // 			{
    // 				waffle.WaffleFlavour = Console.ReadLine();
    // 			}
    // 			else
    // 			{
    // 				Console.WriteLine("Invalid choice");
    // 			}
    // 			
    // 			break;
    // 		case 7:
    // 			break;
    // 		default:
    // 			Console.WriteLine("Invalid choice");
    // 			break;
    // 	}
    // 	
    // }

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