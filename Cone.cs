namespace assignment;
// ID: S10255981, S10257966
// Name: Rainnen, Ethan
internal class Cone : IceCream
{
    private bool dipped;
    public bool Dipped { get; set; }

    public Cone()
    {
    }

    public Cone(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped)
    {
        Option = option;
        Scoops = scoops;
        Flavours = flavours;
        Toppings = toppings;
        Dipped = dipped;
    }
    
    public override double CalculatePrice()
    {
        double sum = 0;
        
        // scoops
        switch (Scoops)
        {
            case 1:
                sum += 4;
                break;
            case 2:
                sum += 5.5;
                break;
            case 3:
                sum += 6.5;
                break;
        }
        // flavours
        foreach (Flavour flavour in Flavours)
        {
            if (flavour.Premium)
            {
                sum += 0.5;
            }
        }
        
        // toppings 
        sum += 1 * Toppings.Count;
        
        // dipped
        if (Dipped)
        {
            sum += 2;
        }

        return sum;
    }
}