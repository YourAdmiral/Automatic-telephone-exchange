using System;
using System.Collections.Generic;
using System.Text;

namespace Automatic_telephone_exchange.EventArguments
{
    public class CallEventArgs : EventArgs, ICallEventArgs
    {
        public int CurrentNumber { get; private set; }
        public int TargetNumber { get; private set; }
        public CallEventArgs(int current, int target)
        {
            CurrentNumber = current;
            TargetNumber = target;
        }
    }
}
