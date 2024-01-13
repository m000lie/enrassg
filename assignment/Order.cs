namespace assignment;

internal class Order
{
    private int id;
    private DateTime timeReceived;
    private DateTime timeFulfilled;

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

    public DateTime TimeFulfilled
    {
        get { return timeFulfilled; }
        set { timeFulfilled = value; }
    }

    public Order()
    {
    }

    public Order(int id, DateTime timeReceived)
    {
        Id = id;
        TimeReceived = timeReceived;
    }

    // to implement
    public void ModifyIceCream(int index)
    {
    }

    // to implement
    public void AddIceCream()
    {
    }

    public void DeleteIceCream(int index)
    {
        // to implement
    }

    public double CalculateTotal()
    {
        // to implement
        return 0;
    }
}