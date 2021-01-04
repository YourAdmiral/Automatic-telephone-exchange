using System;
using System.Collections.Generic;
using System.Text;

namespace Automatic_telephone_exchange.BS
{
    public class Client
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public double Money { get; private set; }
        public Client(string firstName, string lastName, double money)
        {
            FirstName = firstName;
            LastName = lastName;
            Money = money;
        }
        public void AddMoney(double money)
        {
            Money += money;
        }
        public void RemoveMoney(double money)
        {
            Money -= money;
        }
    }
}