using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.ATE;

namespace Automatic_telephone_exchange.BS
{
    public interface IContract
    {
        public Client Client { get; }
        public Terminal Terminal { get; }
        public TariffPlan Tariff { get; }
    }
}