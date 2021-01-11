using System;
using System.Collections.Generic;
using System.Text;

namespace Automatic_telephone_exchange.EventArguments
{
    public interface ICallEventArgs
    {
        public int Id { get; }
        public int CurrentNumber { get; }
        public int TargetNumber { get; }
    }
}
