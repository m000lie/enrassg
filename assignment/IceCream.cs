using System.Diagnostics;

abstract class IceCream
{
    private string option;
    private int scoops;
    private List<Flavour> flavours = new List<Flavour>();
    private List<Topping> toppings = new List<Topping>();

    public string Option
    {
        get { return option; }
        set { option = value; }
    }

    public int Scoops
    {
        get { return scoops; }
        set { scoops = value; }
    }


    public List<Flavour> Flavours
    {
        get { return flavours; }
        set { flavours = value; }
    }

    public List<Topping> Toppings
    {
        get { return toppings; }
        set { toppings = value; }
    }

    public IceCream()
    {
    }

    public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
    {
        Option = option;
        Scoops = scoops;
        Flavours = flavours;
        Toppings = toppings;
    }

    public abstract double CalculatePrice();
}





