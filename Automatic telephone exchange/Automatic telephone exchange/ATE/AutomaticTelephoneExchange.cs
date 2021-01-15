using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Automatic_telephone_exchange.BS;
using Automatic_telephone_exchange.EventArguments;
using Automatic_telephone_exchange.Enums;

namespace Automatic_telephone_exchange.ATE
{
    public class AutomaticTelephoneExchange : IAutomaticTelephoneExchange
    {
        private IDictionary<int, Tuple<Port, IContract>> _clientsInfo;
        private IList<CallInfo> _callList = new List<CallInfo>();
        public AutomaticTelephoneExchange()
        {
            _clientsInfo = new Dictionary<int, Tuple<Port, IContract>>();
        }
        public IContract GetContract()
        {
            var contract = new Contract(this);
            _clientsInfo.Add(contract.Terminal.Number, new Tuple<Port, IContract>(contract.Terminal.Port, contract));
            return contract;
        }
        public void CloseContract(IContract contract)
        {
            contract.Terminal.DisconnectFromPort();
        }
        public void CallTo(object sender, ICallEventArgs e)
        {
            if ((_clientsInfo.ContainsKey(e.TargetNumber) && e.TargetNumber != e.CurrentNumber) || e is CallEndEventArgs)
            {
                CallInfo info = null;
                Port currentPort = null;
                Port targetPort = null;
                int currentNumber = 0;
                int targetNumber = 0;
                CallEnd(e, ref currentPort, ref targetPort, ref currentNumber, ref targetNumber);
                if (targetPort.State == PortState.Connected && currentPort.State == PortState.Connected)
                {
                    var currentTuple = _clientsInfo[currentNumber];
                    var targetTuple = _clientsInfo[targetNumber];
                    CallStart(e, ref info, ref targetPort);
                    Call(e, ref currentTuple, ref info, ref targetPort);
                    CallEnding(e, ref info, ref currentTuple, ref targetTuple, ref targetPort);
                }
            }
            else if (!_clientsInfo.ContainsKey(e.TargetNumber))
            {
                Console.WriteLine("No such number exists!");
            }
            else
            {
                Console.WriteLine("You can't call yourself!");
            }
        }
        public IList<CallInfo> GetCallList()
        {
            return _callList;
        }
        private void CallEnd(ICallEventArgs e, ref Port currentPort, ref Port targetPort, ref int currentNumber, ref int targetNumber)
        {
            if (e is CallEndEventArgs)
            {
                var callListFirst = _callList.First(x => x.Id == e.Id);
                if (callListFirst.CurrentNumber == e.CurrentNumber)
                {
                    currentPort = _clientsInfo[callListFirst.CurrentNumber].Item1;
                    targetPort = _clientsInfo[callListFirst.TargetNumber].Item1;
                    currentNumber = callListFirst.CurrentNumber;
                    targetNumber = callListFirst.TargetNumber;
                }
                else
                {
                    currentPort = _clientsInfo[callListFirst.TargetNumber].Item1;
                    targetPort = _clientsInfo[callListFirst.CurrentNumber].Item1;
                    currentNumber = callListFirst.TargetNumber;
                    targetNumber = callListFirst.CurrentNumber;
                }
            }
            else
            {
                currentPort = _clientsInfo[e.CurrentNumber].Item1;
                targetPort = _clientsInfo[e.TargetNumber].Item1;
                currentNumber = e.CurrentNumber;
                targetNumber = e.TargetNumber;
            }
        }
        private void CallStart(ICallEventArgs e, ref CallInfo info, ref Port targetPort)
        {
            if (e is CallStartEventArgs)
            {
                CallStartEventArgs callStartArgs = (CallStartEventArgs)e;
                if (!callStartArgs.Id.Equals(0) && _callList.Any(x => x.Id.Equals(callStartArgs.Id)))
                {
                    info = _callList.First(x => x.Id.Equals(callStartArgs.Id));
                }
                if (info != null)
                {
                    targetPort.AnswerCall(callStartArgs.CurrentNumber, callStartArgs.TargetNumber, callStartArgs.CallState, info.Id);
                }
                else
                {
                    targetPort.AnswerCall(callStartArgs.CurrentNumber, callStartArgs.TargetNumber, callStartArgs.CallState);
                }
            }
        }
        private void Call(ICallEventArgs e, ref Tuple<Port, IContract> currentTuple, ref CallInfo info, ref Port targetPort)
        {
            if (e is CallEventArgs)
            {
                if (currentTuple.Item2.Client.Money > currentTuple.Item2.Tariff.PerMinuteCost)
                {
                    var callArgs = (CallEventArgs)e;
                    if (callArgs.Id.Equals(0))
                    {
                        info = new CallInfo(
                            callArgs.CurrentNumber,
                            callArgs.TargetNumber,
                            DateTime.Now);
                        _callList.Add(info);
                    }
                    if (!callArgs.Id.Equals(0) && _callList.Any(x => x.Id.Equals(callArgs.Id)))
                    {
                        info = _callList.First(x => x.Id.Equals(callArgs.Id));
                    }
                    if (info != null)
                    {
                        targetPort.IncomingCall(callArgs.CurrentNumber, callArgs.TargetNumber, info.Id);
                    }
                    else
                    {
                        targetPort.IncomingCall(callArgs.CurrentNumber, callArgs.TargetNumber);
                    }
                }
                else
                {
                    Console.WriteLine($"Terminal {e.CurrentNumber} does not have enough money!");
                }
            }
        }
        private void CallEnding(ICallEventArgs e, ref CallInfo info, ref Tuple<Port, IContract> currentTuple, ref Tuple<Port, IContract> targetTuple, ref Port targetPort)
        {
            if (e is CallEndEventArgs)
            {
                var args = (CallEndEventArgs)e;
                double sum;
                info = _callList.Last();
                info.CallEnd = DateTime.Now;
                info.Duration = (int)TimeSpan.FromTicks((info.CallEnd - info.CallStart).Ticks).TotalSeconds;
                sum = currentTuple.Item2.Tariff.PerMinuteCost * TimeSpan.FromTicks((info.CallEnd - info.CallStart).Ticks).TotalMinutes;
                info.Cost = (int)sum;
                targetTuple.Item2.Client.RemoveMoney(info.Cost);
                targetPort.AnswerCall(args.CurrentNumber, args.TargetNumber, CallState.Rejected, info.Id);
            }
        }
    }
}