using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.ATE;

namespace Automatic_telephone_exchange.BS
{
    public class ContractManager : IContractManager
    {
        public delegate void MessageHandler(string message);
        public event MessageHandler MessageEvent;
        private IAutomaticTelephoneExchange _ate;
        public ContractManager(IAutomaticTelephoneExchange ate)
        {
            _ate = ate;
            MessageEvent += DisplayMessage;
        }
        public IContract GetContract()
        {
            var contract = new Contract(_ate);
            _ate.AddClientsInfo(contract);
            MessageEvent("Contract signed!");
            return contract;
        }
        public void CloseContract(IContract contract)
        {
            contract.Terminal.DisconnectFromPort();
            MessageEvent("Contract closed!");
        }
        public void UnsubscribeFromEvent()
        {
            MessageEvent -= DisplayMessage;
        }
        private static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
