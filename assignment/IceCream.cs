

class IceCream
{
    private string option;
    private int scoops;

    public string Option { get; set; }
    public int Scoops { get; set; }
    public List<Flavour> Flavours { get; set; } = new List<Flavour>();
    public List<Topping> Toppings { get; set; } = new List<Topping>();

    public IceCream() { }
    public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
    {
        Option = option;
        Scoops = scoops;
        Flavours = flavours;
        Toppings = toppings;
    }

    public abstract double CalculatePrice() { };
}

class Cup : IceCream
{
    public Cup() { }
    public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
    {
        Option = option;
        Scoops = scoops;
        Flavours = flavours;
        Toppings = toppings;
    }

    public override double CalculatePrice() { };
}

class Cone: IceCream
{
    private bool dipped;
    public bool Dipped { get; set; }

    public Cone() { }
    public Cone(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped)
    {
        Option = option;
        Scoops = scoops;
        Flavours = flavours;
        Toppings = toppings;
        Dipped = dipped;
    }
}

class Waffle: IceCream
{
    private string waffleFlavour;
    public string WaffleFlavour { get; set; }
    public Waffle() { }
    public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleFlavour)
    {
        Option = option;
        Scoops = scoops;
        Flavours = flavours;
        Toppings = toppings;
        WaffleFlavour = waffleFlavour;
    }
}