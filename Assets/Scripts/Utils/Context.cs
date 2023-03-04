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
        public ModelLocator<BaseModel> ModelLocator { get { return _modelLocator; } }
        
        private readonly CommandManager _commandManager;
        private readonly ModelLocator<BaseModel> _modelLocator;

        public Context() : base()
        {
            _modelLocator = new ModelLocator<BaseModel>();
            _commandManager = new CommandManager(this);
        }
    }
}
