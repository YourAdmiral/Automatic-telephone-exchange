using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.Enums;

namespace Automatic_telephone_exchange.EventArguments
{
    public class CallEventArgs : EventArgs, ICallEventArgs
    {
        public int Id { get; private set; }
        public int CurrentNumber { get; private set; }
        public int TargetNumber { get; private set; }
        public CallEventArgs(int currentNumber, int targetNumber)
        {
            CurrentNumber = currentNumber;
            TargetNumber = targetNumber;
        }
        public CallEventArgs(int currentNumber, int targetNumber, int id)
        {
            CurrentNumber = currentNumber;
            TargetNumber = targetNumber;
            Id = id;
        }
    }
}
