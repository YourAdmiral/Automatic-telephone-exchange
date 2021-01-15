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
        private IAutomaticTelephoneExchange _ate;
        public delegate void MessageHandler(string message);
        public event MessageHandler MessageEvent;
        public BillingSystem(IAutomaticTelephoneExchange ate)
        {
            _ate = ate;
            MessageEvent += DisplayMessage;
        }
        public void ShowReport()
        {
            MessageEvent?.Invoke("Report: ");
            foreach (var call in _ate.GetCallList())
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
            MessageEvent?.Invoke("Sorted!");
            ShowReport(_ate.GetCallList().OrderBy(func).ToList());
        }
        public void FilterBy(Func<CallInfo, bool> func)
        {
            MessageEvent?.Invoke("Filtered!");
            ShowReport(_ate.GetCallList().Where(func).ToList());
        }
        public void UnsubscribeFromEvent()
        {
            MessageEvent -= DisplayMessage;
        }
        private static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        private void ShowReport(IList<CallInfo> calls)
        {
            MessageEvent?.Invoke("Report: ");
            foreach (var call in calls)
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
    }
}