using System;
using System.Collections.Generic;
using System.Text;

namespace Automatic_telephone_exchange.ATE
{
    public class Terminal
    {
        public int Number { get; private set; }
        private Port _terminalPort;
        private int _id;
        public Terminal(int number, Port port)
        {
            Number = number;
            _terminalPort = port;
        }
    }
}