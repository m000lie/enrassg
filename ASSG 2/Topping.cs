namespace assignment
{
    class Topping
    {
        private string type;
        public string Type { get; set; }
        public Topping() { }
        public Topping(string type)
        {
            Type = type;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
