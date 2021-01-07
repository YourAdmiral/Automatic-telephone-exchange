using System;
using System.Collections.Generic;
using System.Text;

namespace Automatic_telephone_exchange.EventArguments
{
    public class CallEndEventArgs: EventArgs, ICallEventArgs
    {
        public int Id { get; private set; }
        public int CurrentNumber { get; private set; }
        public int TargetNumber { get; private set; }
        public CallEndEventArgs(int id, int number)
        {
            Id = id;
            CurrentNumber = number;
        }
    }
}
