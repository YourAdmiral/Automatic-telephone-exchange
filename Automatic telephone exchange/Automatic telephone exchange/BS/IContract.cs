using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.ATE;

namespace Automatic_telephone_exchange.BS
{
    public interface IContract
    {
        Client Client { get; }
        Terminal Terminal { get; }
        TariffPlan Tariff { get; }
    }
}