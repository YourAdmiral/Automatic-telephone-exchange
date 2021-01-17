using System;
using System.Collections.Generic;
using System.Text;

namespace Automatic_telephone_exchange.BS
{
    interface IContractManager
    {
        public IContract GetContract();
        public void CloseContract(IContract contract);
    }
}
