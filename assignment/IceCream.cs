

abstract class IceCream
{
	private string option;
	private int scoops;
	private List<Flavour> flavours { get; set; } = new List<Flavour>();
	private List<Topping> toppings { get; set; } = new List<Topping>();

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
	public IceCream() { }
	public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
	{
		Option = option;
		Scoops = scoops;
		Flavours = flavours;
		Toppings = toppings;
	}

	public abstract double CalculatePrice();
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

class Cone : IceCream
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

class Waffle : IceCream
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