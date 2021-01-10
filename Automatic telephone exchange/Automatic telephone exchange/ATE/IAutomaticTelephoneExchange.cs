using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.BS;
using Automatic_telephone_exchange.EventArguments;

namespace Automatic_telephone_exchange.ATE
{
    public interface IAutomaticTelephoneExchange
    {
        public IContract GetContract();
        public void CallTo(object sender, ICallEventArgs e);
    }
}