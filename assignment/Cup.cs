namespace assignment;
// ID: S10255981, S10257966
// Name: Rainnen, Ethan
internal class Cup : IceCream
{
    public Cup()
    {
    }

    public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
    {
        Option = option;
        Scoops = scoops;
        Flavours = flavours;
        Toppings = toppings;
    }

    public override double CalculatePrice()
    {
        double sum = 0;
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

        foreach (Flavour flavour in Flavours)
        {
            if (flavour.Premium)
            {
                sum += 2;
            }
        }
        
        // toppings 
        sum += 1 * Toppings.Count;

        return sum;
    }
}