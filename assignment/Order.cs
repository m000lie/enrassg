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

    // added dictionary to check if flavour is premium or not
    private Dictionary<string, bool> flavourClass = new Dictionary<string, bool>();

    // added list to store valid available toppings
    private List<string> toppingList = new List<string>();

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

    // initialize flavourClass dictionary
    private void initFlavourClass()
    {
        flavourClass.Add("vanilla", false);
        flavourClass.Add("chocolate", false);
        flavourClass.Add("strawberry", false);
        flavourClass.Add("durian", true);
        flavourClass.Add("ube", true);
        flavourClass.Add("sea salt", true);
    }

    private void initToppingList()
    {
        toppingList.Add("sprinkles");
        toppingList.Add("mochi");
        toppingList.Add("sago");
        toppingList.Add("oreos");
    }

    public Order()
    {
        initFlavourClass();
        initToppingList();
    }

    public Order(int id, DateTime timeReceived, DateTime? timeFulfilled, List<IceCream> iceCreamList)
    {
        Id = id;
        TimeReceived = timeReceived;
        TimeFulfilled = timeFulfilled;
        IceCreamList = iceCreamList;
        initFlavourClass();
        initToppingList();
    }

    // made this method public and static so that it may be called outside the class without creating an instance of the class
    public IceCream newIceCream()
    {
        // input option
        Console.Write("Enter new option (cup, cone, waffle): ");
        string newOption = Console.ReadLine();
        try
        {
            if (newOption.ToLower() != "cup" && newOption.ToLower() != "cone" &&
                newOption.ToLower() != "waffle")
            {
                throw new Exception();
            }
        }
        catch (Exception e)
        {
            throw new Exception();
        }

        // input scoops
        Console.Write("Enter number of scoops (max: 3): ");
        int newScoops = Convert.ToInt32(Console.ReadLine());
        try
        {
            if (newScoops > 3)
            {
                Console.WriteLine("Invalid number of scoops. You may only have a maximum of 3 scoops.");
                throw new Exception();
            }
        }
        catch (Exception e)
        {
            throw new Exception();
        }

        // input flavour
        Console.Write($"Enter new flavours separated by ','  (required: {newScoops}): ");
        string[] flavours = new string[newScoops]; // array size = scoop because each scoop has a flavour
        flavours = Console.ReadLine().Split(','); // add user input to flavours array

        if (flavours.Length != newScoops)
        {
            Console.WriteLine("Invalid number of flavours");
            throw new Exception();
        }

        foreach (string flavour in flavours)
        {
            if (!flavourClass.ContainsKey(flavour.ToLower()))
            {
                throw new Exception();
            }
        }


        List<Flavour> newFlavours = new List<Flavour>();
        foreach (string f in flavours)
        {
            newFlavours.Add(new Flavour(f, flavourClass[f.ToLower()], 1));
        }

        // input toppings
        Console.Write("Enter new toppings (max: 4): ");
        string[] toppings = new string[4];
        toppings = Console.ReadLine().Split(','); // add user input to toppings array
        // check if they are valid toppings

        foreach (string topping in toppings)
        {
            if (!toppingList.Contains(topping.ToLower()))
            {
                throw new Exception();
            }
        }


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
                return new Cone(newOption, newScoops, newFlavours, newToppings, true);
            }
            // default to non dipped
            else
            {
                return new Cone(newOption, newScoops, newFlavours, newToppings, false);
            }
        }

        else if (newOption.ToLower() == "cup")
        {
            return new Cup(newOption, newScoops, newFlavours, newToppings);
        }

        else
        {
            Console.Write("Enter new waffle flavour (enter for default): ");
            string newWaffleFlavour = Console.ReadLine();
            if (newWaffleFlavour.ToLower() == "red velvet" || newWaffleFlavour.ToLower() == "charcoal" ||
                newWaffleFlavour.ToLower() == "pandan")
            {
                return new Waffle(newOption, newScoops, newFlavours, newToppings, newWaffleFlavour);
            }
            else
            {
                if (newWaffleFlavour != "")
                {
                    Console.WriteLine("Invalid waffle flavour. Defaulting to plain.");
                }

                return new Waffle(newOption, newScoops, newFlavours, newToppings, null);
            }
        }
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
                    iceCreamList[index] = newIceCream();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                break;

            case 2:
                try
                {
                    iceCreamList.Add(newIceCream());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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

        return $"ID: {Id}\n{s}";
    }
}