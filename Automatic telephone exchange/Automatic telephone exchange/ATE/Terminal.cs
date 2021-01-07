using System;
using System.Collections.Generic;
using System.Text;
using Automatic_telephone_exchange.EventArguments;
using Automatic_telephone_exchange.Enums;

namespace Automatic_telephone_exchange.ATE
{
    public class Terminal
    {
        public int Number { get; private set; }
        public event EventHandler<CallStartEventArgs> CallStartEvent;
        public event EventHandler<CallEndEventArgs> CallEndEvent;
        public event EventHandler<CallEventArgs> CallEvent;
        private Port _port;
        private int _id;
        public Terminal(int number, Port port)
        {
            Number = number;
            _port = port;
        }
        public void Call(int targetNumber)
        {
            CallEvent?.Invoke(this, new CallEventArgs(Number, targetNumber));
        }
        public void EndCall()
        {
            CallEndEvent?.Invoke(this, new CallEndEventArgs(_id, Number));
        }
        public void ConnectToPort()
        {
            if (_port.Connect(this))
            {
                _port.PortCallEvent += TakeIncomingCall;
                _port.PortAnswerEvent += TakeAnswer;
            }
        }
        public void DisconnectFromPort()
        {
            if (_port.Disconnect(this))
            {
                _port.PortCallEvent -= TakeIncomingCall;
                _port.PortAnswerEvent -= TakeAnswer;
            }
        }
        public void AnswerToCall(int targetNumber, CallState state, int id)
        {
            CallStartEvent?.Invoke(this, new CallStartEventArgs(Number, targetNumber, state, id));
        }
        public void TakeAnswer(object sender, CallStartEventArgs e)
        {
            _id = e.Id;
            if (e.CallState == CallState.Answered)
                Console.WriteLine($"Terminal {e.CurrentNumber} answered call from {e.TargetNumber}.");
            else
                Console.WriteLine($"Terminal {e.CurrentNumber} rejected call from {e.TargetNumber}.");
        }
        public void TakeIncomingCall(object sender, CallEventArgs e)
        {
            ConsoleKey choose = default;
            _id = e.Id;
            Console.WriteLine($"Incoming call from {e.CurrentNumber} to {e.TargetNumber}");
            while (choose != ConsoleKey.D1 || choose != ConsoleKey.D2)
            {
                Console.WriteLine("Accept or reject?\n1 - Accept\n2 - Reject");
                choose = Console.ReadKey(true).Key;
                switch (choose)
                {
                    case ConsoleKey.D1:
                        AnswerToCall(e.CurrentNumber, CallState.Answered, e.Id);
                        break;
                    case ConsoleKey.D2:
                        EndCall();
                        break;
                }
            }
        }
    }
}