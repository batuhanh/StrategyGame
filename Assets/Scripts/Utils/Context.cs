using StrategyGame.MVC.Models;
using StrategyGame.Utils.CommandSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace StrategyGame.Utils
{
    public class Context 
    {
        public CommandManager CommandManager { get { return _commandManager; } }
        
        private readonly CommandManager _commandManager;

        public Context() : base()
        {
            _commandManager = new CommandManager(this);
        }
    }
}
