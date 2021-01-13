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
        public Port Port { get; private set; }
        public event EventHandler<CallStartEventArgs> CallStartEvent;
        public event EventHandler<CallEndEventArgs> CallEndEvent;
        public event EventHandler<CallEventArgs> CallEvent;
        private int _id;
        public Terminal(Port port)
        {
            Number = new Random().Next(0,10000);
            Port = port;
        }
        ~Terminal()
        {
            DisconnectFromPort();
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
            if (Port.Connect(this))
            {
                Port.PortCallEvent += TakeIncomingCall;
                Port.PortAnswerEvent += TakeAnswer;
            }
        }
        public void DisconnectFromPort()
        {
            if (Port.Disconnect())
            {
                Port.PortCallEvent -= TakeIncomingCall;
                Port.PortAnswerEvent -= TakeAnswer;
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
                Console.WriteLine($"Terminal {e.CurrentNumber} rejected call.");
        }
        public void TakeIncomingCall(object sender, CallEventArgs e)
        {
            ConsoleKey choose = default;
            _id = e.Id;
            bool check = true;
            Console.WriteLine($"Incoming call from {e.CurrentNumber} to {e.TargetNumber}");
            while (check)
            {
                Console.WriteLine("Accept or reject?\n1 - Accept\n2 - Reject");
                choose = Console.ReadKey(true).Key;
                switch (choose)
                {
                    case ConsoleKey.D1:
                        AnswerToCall(e.CurrentNumber, CallState.Answered, e.Id);
                        check = false;
                        break;
                    case ConsoleKey.D2:
                        EndCall();
                        check = false;
                        break;
                }
            }
        }
    }
}