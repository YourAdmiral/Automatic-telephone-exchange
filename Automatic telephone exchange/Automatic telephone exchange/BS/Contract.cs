using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.ATE;

namespace Automatic_telephone_exchange.BS
{
    public class Contract : IContract
    {
        public int Number { get; private set; }
        public Client Client { get; private set; }
        public TariffPlan Tariff { get; private set; }
        public Contract(Client client)
        {
            Number = new Random().Next(0, 10000);
            Client = client;
            Tariff = new TariffPlan();
        }
    }
}