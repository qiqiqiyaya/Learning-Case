using Stateless.Graph;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stateless.Reflection;

namespace StatelessTest
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum State
    {
        Open,
        Assigned,
        Deferred,
        Closed
    }

    /// <summary>
    /// 触发器
    /// </summary>
    public enum Trigger
    {
        Assign,
        Defer,
        Close
    }

    public class Bug
    {
        State _state = State.Open;
        StateMachine<State, Trigger> _machine;
        StateMachine<State, Trigger>.TriggerWithParameters<string> _assignTrigger;

        string _title;
        string _assignee;

        public Bug(string title)
        {
            _title = title;

            //_machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);
            _machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);

            _assignTrigger = _machine.SetTriggerParameters<string>(Trigger.Assign);

            _machine.Configure(State.Open).Permit(Trigger.Assign, State.Assigned);
            _machine.Configure(State.Assigned)
                .OnEntryFrom(_assignTrigger, assignee => _assignee = assignee)
                .SubstateOf(State.Open)
                .PermitReentry(Trigger.Assign)
                .Permit(Trigger.Close, State.Closed)
                .Permit(Trigger.Defer, State.Deferred);

            _machine.Configure(State.Deferred)
                .OnEntry(() => _assignee = null)
                .Permit(Trigger.Assign, State.Assigned);

        }

        public string CurrentState
        {
            get
            {
                return _machine.State.ToString();
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        public string Assignee
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_assignee))
                {
                    return "Not Assigned";
                }

                return _assignee;
            }
        }

        public void Assign(string assignee)
        {
            _machine.Fire(_assignTrigger, assignee);
        }

        public void Defer()
        {
            _machine.Fire(Trigger.Defer);
        }

        public void Close()
        {
            _machine.Fire(Trigger.Close);
        }

        public StateMachineInfo GetInfo()
        {
            return _machine.GetInfo();
        }
    }
}
