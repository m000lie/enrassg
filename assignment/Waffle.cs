// ID: S10255981, S10257966
// Name: Rainnen, Ethan
namespace assignment;

internal class Waffle : IceCream
{
    private string waffleFlavour;
    public string WaffleFlavour { get; set; }

    public Waffle()
    {
    }

    public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleFlavour = "NA")
    {
        Option = option;
        Scoops = scoops;
        Flavours = flavours;
        Toppings = toppings;
        WaffleFlavour = waffleFlavour;
    }
    
    public override double CalculatePrice()
    {
        double sum = 0;
        
        // scoops
        switch (Scoops)
        {
            case 1:
                sum += 7;
                break;
            case 2:
                sum += 8.5;
                break;
            case 3:
                sum += 9.5;
                break;
        }
        // flavours
        foreach (Flavour flavour in Flavours)
        {
            if (flavour.Premium)
            {
                sum += 2;
            }
        }
        
        // toppings 
        sum += 1 * Toppings.Count;
        
        // waffle flavour
        if (WaffleFlavour != null)
        {
            sum += 3;
        }

        return sum;
    }
}