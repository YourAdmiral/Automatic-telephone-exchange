using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.ATE;

namespace Automatic_telephone_exchange.BS
{
    public class Contract : IContract
    {
        public Client Client { get; private set; }
        public Terminal Terminal { get; private set; }
        public TariffPlan Tariff { get; private set; }
        public Contract(IAutomaticTelephoneExchange ate)
        {
            Console.WriteLine("Enter information about client:");
            Console.WriteLine("First name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Last name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Amount of money: ");
            double money = Convert.ToDouble(Console.ReadLine());
            Client = new Client(
                firstName,
                lastName,
                money);
            Terminal = new Terminal(ate);
            Tariff = new TariffPlan();
        }
    }
}