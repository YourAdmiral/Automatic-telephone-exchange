using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.Enums;
using Automatic_telephone_exchange.EventArguments;

namespace Automatic_telephone_exchange.ATE
{
    public class Port
    {
        public PortState State;
        public delegate void MessageHandler(string message);
        public event EventHandler<CallStartEventArgs> PortAnswerEvent;
        public event EventHandler<CallEventArgs> PortCallEvent;
        public event EventHandler<CallStartEventArgs> CallStartEvent;
        public event EventHandler<CallEndEventArgs> EndCallEvent;
        public event EventHandler<CallEventArgs> CallEvent;
        public event MessageHandler ConnectEvent;
        public event MessageHandler DisconnectEvent;
        private IAutomaticTelephoneExchange _ate;
        private Terminal _terminal;
        public Port(IAutomaticTelephoneExchange ate)
        {
            _ate = ate;
            State = PortState.Disconnected;
            ConnectEvent += DisplayMessage;
            DisconnectEvent += DisplayMessage;
            CallStartEvent += ate.CallTo;
            CallEvent += ate.CallTo;
            EndCallEvent += ate.CallTo;
        }
        ~Port()
        {
            Disconnect();
            ConnectEvent -= DisplayMessage;
            DisconnectEvent -= DisplayMessage;
            CallStartEvent -= _ate.CallTo;
            CallEvent -= _ate.CallTo;
            EndCallEvent -= _ate.CallTo;
        }
        public bool Connect(Terminal terminal)
        {
            if (State == PortState.Disconnected)
            {
                State = PortState.Connected;
                terminal.CallStartEvent += AnswerTo;
                terminal.CallEndEvent += EndCall;
                terminal.CallEvent += CallTo;
                ConnectEvent?.Invoke($"Terminal {terminal.Number} connected to port!");
                _terminal = terminal;
                return true;
            }
            else
            {
                ConnectEvent?.Invoke($"Port engaged by Terminal {_terminal.Number}!");
                return false;
            }
        }
        public bool Disconnect()
        {
            if (State == PortState.Connected)
            {
                State = PortState.Disconnected;
                _terminal.CallStartEvent -= AnswerTo;
                _terminal.CallEndEvent -= EndCall;
                _terminal.CallEvent -= CallTo;
                DisconnectEvent?.Invoke($"Terminal {_terminal.Number} disconnected from port!");
                _terminal = null;
                return true;
            }
            else
            {
                ConnectEvent?.Invoke($"Port already disconnected!");
                return false;
            }
        }
        public void AnswerCall(int number, int targetNumber, CallState state)
        {
            PortAnswerEvent?.Invoke(this, new CallStartEventArgs(number, targetNumber, state));
        }
        public void AnswerCall(int number, int targetNumber, CallState state, int id)
        {
            PortAnswerEvent?.Invoke(this, new CallStartEventArgs(number, targetNumber, state, id));
        }
        public void IncomingCall(int number, int targetNumber)
        {
            PortCallEvent?.Invoke(this, new CallEventArgs(number, targetNumber));
        }
        public void IncomingCall(int number, int targetNumber, int id)
        {
            PortCallEvent?.Invoke(this, new CallEventArgs(number, targetNumber, id));
        }
        private static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        private void CallTo(object sender, CallEventArgs e)
        {
            CallEvent?.Invoke(this, new CallEventArgs(e.CurrentNumber, e.TargetNumber));
        }
        private void AnswerTo(object sender, CallStartEventArgs e)
        {
            CallStartEvent?.Invoke(this, new CallStartEventArgs(
                e.CurrentNumber,
                e.TargetNumber,
                e.CallState,
                e.Id));
        }
        private void EndCall(object sender, CallEndEventArgs e)
        {
            EndCallEvent?.Invoke(this, new CallEndEventArgs(e.Id, e.CurrentNumber));
        }
    }
}