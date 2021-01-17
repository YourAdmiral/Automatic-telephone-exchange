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
            bool isDouble = false;
            double money = default;
            while (!isDouble)
            {
                string str = Console.ReadLine();
                isDouble = double.TryParse(str, out money);
                if (!isDouble)
                {
                    Console.WriteLine("Incorrect number entered!");
                }
            }
            Client = new Client(
                firstName,
                lastName,
                money);
            Port port = new Port(ate);
            Terminal = new Terminal(port);
            Tariff = new TariffPlan();
        }
    }
}