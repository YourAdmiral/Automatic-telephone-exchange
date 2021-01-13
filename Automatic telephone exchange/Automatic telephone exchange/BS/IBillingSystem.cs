﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Automatic_telephone_exchange.BS
{
    public interface IBillingSystem
    {
        public void ShowReport();
        public void SortBy(Func<CallInfo, object> func);
        public IList<CallInfo> FilterBy(Func<CallInfo, bool> func);
    }
}