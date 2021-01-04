using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.Enums;

namespace Automatic_telephone_exchange.EventArguments
{
    public class CallStartEventArgs : EventArgs, ICallEventArgs
    {
        public int CurrentNumber { get; private set; }
        public int TargetNumber { get; private set; }
        public CallState State;
        public CallStartEventArgs(int current, int target, CallState state)
        {
            CurrentNumber = current;
            TargetNumber = target;
            State = state;
        }
    }
}
