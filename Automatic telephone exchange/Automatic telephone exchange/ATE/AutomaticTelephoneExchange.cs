using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Automatic_telephone_exchange.BS;
using Automatic_telephone_exchange.EventArguments;
using Automatic_telephone_exchange.Enums;

namespace Automatic_telephone_exchange.ATE
{
    public class AutomaticTelephoneExchange : IAutomaticTelephoneExchange
    {
        private IDictionary<int, Tuple<Port, IContract>> _clientsInfo;
        public AutomaticTelephoneExchange()
        {
            _clientsInfo = new Dictionary<int, Tuple<Port, IContract>>();
        }
        public IContract GetContract()
        {
            var contract = new Contract(this);
            _clientsInfo.Add(contract.Terminal.Number, new Tuple<Port, IContract>(contract.Terminal.Port, contract));
            return contract;
        }
        public void CallTo(object sender, ICallEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}