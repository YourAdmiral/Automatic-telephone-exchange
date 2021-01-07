using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.Enums;

namespace Automatic_telephone_exchange.EventArguments
{
    public class CallStartEventArgs: EventArgs, ICallEventArgs
    {
        public int Id { get; private set; }
        public int CurrentNumber { get; private set; }
        public int TargetNumber { get; private set; }
        public CallState CallState;
        public CallStartEventArgs(int currentNumber, int targetNumber, CallState callState)
        {
            CurrentNumber = currentNumber;
            TargetNumber = targetNumber;
            CallState = callState;
        }
        public CallStartEventArgs(int currentNumber, int targetNumber, CallState state, int id)
        {
            CurrentNumber = currentNumber;
            TargetNumber = targetNumber;
            CallState = state;
            Id = id;
        }
    }
}
