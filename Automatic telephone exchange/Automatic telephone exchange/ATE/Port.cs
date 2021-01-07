using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.Enums;
using Automatic_telephone_exchange.EventArguments;

namespace Automatic_telephone_exchange.ATE
{
    public class Port
    {
        public delegate void MessageHandler(string message);
        public event MessageHandler ConnectEvent;
        public event MessageHandler DisconnectEvent;
        public PortState State;
        public Port()
        {
            State = PortState.Disconnected;
            ConnectEvent += DisplayMessage;
            DisconnectEvent += DisplayMessage;
        }
        public bool Connect(Terminal terminal)
        {
            if (State == PortState.Disconnected)
            {
                State = PortState.Connected;
                ConnectEvent?.Invoke($"Terminal {terminal.Number} connected to port!");
            }
            return true;
        }
        public bool Disconnect(Terminal terminal)
        {
            if (State == PortState.Connected)
            {
                State = PortState.Disconnected;
                DisconnectEvent?.Invoke($"Terminal {terminal.Number} disconnected from port!");
            }
            return true;
        }
        private static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        private void CallTo(object sender, CallEventArgs e)
        {

        }
        private void AnswerTo(object sender, CallStartEventArgs e)
        {

        }
    }
}