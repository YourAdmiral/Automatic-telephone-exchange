using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.ATE;

namespace Automatic_telephone_exchange.BS
{
    public interface IContract
    {
        int Number { get; }
        Client Client { get; }
        TariffPlan Tariff { get; }
    }
}