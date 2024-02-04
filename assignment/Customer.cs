using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
	internal class Customer
	{
		private string name;
		private int memberId;
		private DateTime dob;
		private Order currentOrder;
		private List<Order> orderHistory = new List<Order>();
		private PointCard rewards;
        private string v;

        public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public int MemberId
		{
			get { return memberId; }
			set { memberId = value; }
		}
		public DateTime Dob
		{
			get { return dob; }
			set { dob = value; }
		}
		public Order CurrentOrder
		{
			get { return currentOrder; }
			set { currentOrder = value; }
		}
		public List<Order> OrderHistory
		{
			get { return orderHistory; }
			set { orderHistory = value; }
		}
		public PointCard Rewards
		{
			get { return rewards; }
			set { rewards = value; }
		}
		public Customer(string name, int memberId, DateTime dob, Order currentOrder, List<Order> orderHistory, PointCard rewards)
		{
			Name = name;
			MemberId = memberId;
			Dob = dob;
			CurrentOrder = currentOrder;
			OrderHistory = orderHistory;
			Rewards = rewards;
		}

		

        public Customer()
        {
        }

		// create a blank order
		// the actual order creation method is in Order.cs(newIceCream()). 
		// we felt that it was more appropriate to put it there as it's functionality is utilized by many of it's methods. 
		// if we implement it here, we would to duplicate our code and that would violate the principal of DRY (Don't Repeat Yourself)
        public Order MakeOrder()
		{
			Random rnd = new Random();
			Order order = new Order(rnd.Next(), DateTime.Now, null, new List<IceCream>());
			return order;
		}


		public bool IsBirthday()
		{
			if (DateTime.Now.Day == Dob.Day && DateTime.Now.Month == Dob.Month)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public override string ToString()
		{
			return $"Name: {name}, MemberID: {memberId}, dob: {dob}, MembershipStatus: {rewards.Tier}, MembershipPoints: {rewards.Points}, PunchCard: {rewards.PunchCard}";
		}

	}
}
