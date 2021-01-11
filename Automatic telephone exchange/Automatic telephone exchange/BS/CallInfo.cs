using System;
using System.Collections.Generic;
using System.Text;

namespace Automatic_telephone_exchange.BS
{
    public class CallInfo
    {
        public int Id { get; set; }
        public int CurrentNumber { get; set; }
        public int TargetNumber { get; set; }
        public double Duration { get; set; }
        public DateTime CallStart { get; set; }
        public DateTime CallEnd { get; set; }
        public double Cost { get; set; }
        public CallInfo(int currentNumber, int targetNumber, DateTime callStart)
        {
            Id = new Random().Next(0, 10000);
            CurrentNumber = currentNumber;
            TargetNumber = targetNumber;
            CallStart = callStart;
        }
    }
}