namespace assignment
{
    abstract class Flavour
    {
        private string type;
        private bool premium;
        private int quantity;
        public string Type { get; set; }
        public bool Premium { get; set; }
        public int Quantity { get; set; }

        public Flavour() { }
        public Flavour(string type, bool premium, int quantity)
        {
            Type = type;
            Premium = premium;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}