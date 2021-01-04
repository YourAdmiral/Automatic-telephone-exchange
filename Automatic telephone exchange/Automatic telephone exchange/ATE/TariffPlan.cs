using System;
using System.Collections.Generic;
using System.Text;

namespace Automatic_telephone_exchange.ATE
{
    public class TariffPlan
    {
        public int MonthCost { get; private set; }
        public int PerMinuteCost { get; private set; }
        public TariffPlan()
        {
            MonthCost = 20;
            PerMinuteCost = 1;
        }
    }
}