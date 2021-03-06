﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automatic_telephone_exchange.ATE;
using Automatic_telephone_exchange.BS;

namespace Automatic_telephone_exchange
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IAutomaticTelephoneExchange ate = new AutomaticTelephoneExchange();
                IBillingSystem bs = new BillingSystem(ate);
                IContractManager cm = new ContractManager(ate);
                IContract c1 = cm.GetContract();
                IContract c2 = cm.GetContract();
                IContract c3 = cm.GetContract();
                c1.Client.AddMoney(10);
                Terminal t1 = c1.Terminal;
                Terminal t2 = c2.Terminal;
                Terminal t3 = c3.Terminal;
                t1.ConnectToPort();
                t2.ConnectToPort();
                t3.ConnectToPort();
                t1.Call(t2.Number);
                Thread.Sleep(4000);
                t2.EndCall();
                t2.Call(t3.Number);
                Thread.Sleep(3000);
                t3.EndCall();
                t3.Call(t1.Number);
                Thread.Sleep(1000);
                t1.EndCall();

                bs.ShowReport();
                bs.SortBy(x => x.Duration);
                bs.SortBy(x => x.CurrentNumber);
                bs.SortBy(x => x.Cost);
                bs.SortBy(x => x.CallStart.Date);
                bs.FilterBy(x => x.CurrentNumber == t1.Number);
                bs.FilterBy(x => x.Cost == 0);
                bs.FilterBy(x => x.CallStart.Date.ToShortDateString() == DateTime.Now.ToShortDateString());

                Console.WriteLine("Calls from previous month: ");
                bs.FilterBy(x => x.CallStart.Date.Month == DateTime.Now.Month - 1);

                t1.DisconnectFromPort();
                t2.DisconnectFromPort();
                t3.DisconnectFromPort();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
