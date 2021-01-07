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
        public Contract(Client client)
        {
            Client = client;
            Terminal = new Terminal(new Random().Next(0, 10000), 
                new Port());
            Tariff = new TariffPlan();
        }
    }
}