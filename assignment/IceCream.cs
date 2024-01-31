// ID: S10255981, S10257966
// Name: Rainnen, Ethan
using System.Diagnostics;

namespace assignment 
{ 
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

        public override string ToString()
        {
            return $"Option: {Option}, Scoops: {Scoops}, Flavours: {flavours}, Toppings: {Toppings}";
        }
    }

}
