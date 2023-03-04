using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;

namespace StrategyGame.Utils.CommandSystem
{
    public class CommandManager 
    {
        public int InvokeDelegateLookupCount { get { return _invokeCommandDelegatesLookup.Count; } }

        private Context _context;

        public delegate void InvokeCommandDelegate<TCommand>(TCommand e) where TCommand : ICommand;
        private delegate void InvokeCommandDelegate(ICommand e);
        private Dictionary<System.Type, InvokeCommandDelegate> _invokeCommandDelegates = new Dictionary<System.Type, InvokeCommandDelegate>();
        private Dictionary<System.Delegate, InvokeCommandDelegate> _invokeCommandDelegatesLookup = new Dictionary<System.Delegate, InvokeCommandDelegate>();

        public CommandManager(Context context)
        {
            _context = context;
        }
        public void InvokeCommand(ICommand command)
        {
            InvokeCommandDelegate del;
            if (_invokeCommandDelegates.TryGetValue(command.GetType(), out del))
            {
                del.Invoke(command);
            }
        }

        public bool ExecuteCommand(ICommand command)
        {
            bool didExecute = command.Execute(_context);
            InvokeCommand(command);

            return didExecute;
        }

        public void AddCommandListener<TCommand>(CommandManager.InvokeCommandDelegate<TCommand> invokeDelegate) where TCommand : ICommand
        {
            AddCommandListenerImpl<TCommand>(invokeDelegate);
        }

        private void AddCommandListenerImpl<TCommand>(CommandManager.InvokeCommandDelegate<TCommand> del) where TCommand : ICommand
        {
            if (_invokeCommandDelegatesLookup.ContainsKey(del))
            {
                return;
            }

            InvokeCommandDelegate internalDelegate = (e) => del((TCommand)e);
            _invokeCommandDelegatesLookup[del] = internalDelegate;

            InvokeCommandDelegate tempDel;
            if (_invokeCommandDelegates.TryGetValue(typeof(TCommand), out tempDel))
            {
                _invokeCommandDelegates[typeof(TCommand)] = tempDel += internalDelegate;
            }
            else
            {
                _invokeCommandDelegates[typeof(TCommand)] = internalDelegate;
            }
        }

        public void RemoveCommandListener<TCommand>(CommandManager.InvokeCommandDelegate<TCommand> del) where TCommand : ICommand
        {
            InvokeCommandDelegate internalDelegate;

            if (_invokeCommandDelegatesLookup.TryGetValue(del, out internalDelegate))
            {
                InvokeCommandDelegate tempDel;
                if (_invokeCommandDelegates.TryGetValue(typeof(TCommand), out tempDel))
                {
                    tempDel -= internalDelegate;
                    if (tempDel == null)
                    {
                        _invokeCommandDelegates.Remove(typeof(TCommand));
                    }
                    else
                    {
                        _invokeCommandDelegates[typeof(TCommand)] = tempDel;
                    }
                }

                _invokeCommandDelegatesLookup.Remove(del);
            }
        }
    }
}
