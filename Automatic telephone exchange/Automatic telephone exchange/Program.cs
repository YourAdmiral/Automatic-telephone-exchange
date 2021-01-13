using System;
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
            IAutomaticTelephoneExchange ate = new AutomaticTelephoneExchange();
            IBillingSystem bs = new BillingSystem(ate);
            IContract c1 = ate.GetContract();
            IContract c2 = ate.GetContract();
            IContract c3 = ate.GetContract();
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
            Console.WriteLine("\nReport: ");
            bs.ShowReport();
            bs.SortBy(x=>x.Duration);
            Console.WriteLine("\n-----Sorted-----!");
            bs.ShowReport();
            Console.WriteLine(c1.Client.Money);

            bs.SortBy(x => x.CurrentNumber);
            Console.WriteLine("\n-----Sorted-----!");
            bs.ShowReport();

            bs.SortBy(x => x.Cost);
            Console.WriteLine("\n-----Sorted-----!");
            bs.ShowReport();

            bs.SortBy(x => x.CallStart.Date);
            Console.WriteLine("\n-----Sorted-----!");
            bs.ShowReport();

            t1.DisconnectFromPort();
            t2.DisconnectFromPort();
            t3.DisconnectFromPort();
        }
    }
}
