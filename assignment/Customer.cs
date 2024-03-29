﻿using System;
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


		// public Order MakeOrder()
		// {
		// 	Random rnd = new Random();
		// 	Order order = new Order(rnd.Next(), DateTime.Now);
		// 	return order;
		// }

		public Customer()
		{
		}


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
			return $"Name: {name, -10} MemberID: {memberId, -10} Dob: {dob, -15:dd/MM/yyyy}" +
				$"MembershipStatus: {rewards.Tier, -10} MembershipPoints: {rewards.Points, -10} PunchCard: {rewards.PunchCard, -5}";
		}

	}

}