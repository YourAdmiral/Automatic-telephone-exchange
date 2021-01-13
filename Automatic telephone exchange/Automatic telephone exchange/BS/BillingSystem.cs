using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.Enums;
using Automatic_telephone_exchange.ATE;
using System.Linq;

namespace Automatic_telephone_exchange.BS
{
    public class BillingSystem : IBillingSystem
    {
        IAutomaticTelephoneExchange _ate;
        IList<CallInfo> _calls;
        public BillingSystem(IAutomaticTelephoneExchange ate)
        {
            _ate = ate;
            _calls = _ate.GetCallList();
        }
        public void ShowReport()
        {
            foreach (var call in _calls)
            {
                Console.WriteLine($"Call From: {call.CurrentNumber}\n" +
                    $"Call To: {call.TargetNumber}\n" +
                    $"Duration: {call.Duration} sec.\n" +
                    $"Call start: {call.CallStart}\n" +
                    $"Call end: {call.CallEnd}\n" +
                    $"Call cost: {call.Cost}\n");
                Console.WriteLine("---------------");
            }
        }
        public void SortBy(Func<CallInfo, object> func)
        {
            _calls = _ate.GetCallList().OrderBy(func).ToList();
        }
        public IList<CallInfo> FilterBy(Func<CallInfo, bool> func)
        {
            return _calls.Where(func).ToList();
        }
    }
}