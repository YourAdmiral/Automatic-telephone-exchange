using System;
using System.Collections.Generic;
using System.Text;

namespace Automatic_telephone_exchange.EventArguments
{
    public interface ICallEventArgs
    {
        int CurrentNumber { get; }
        int TargetNumber { get; }
    }
}
