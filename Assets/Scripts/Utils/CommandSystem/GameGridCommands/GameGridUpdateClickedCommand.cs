using StrategyGame.MVC.Models;
using StrategyGame.Utils;
using StrategyGame.Utils.CommandSystem;
using System;
using UnityEngine;

public class GameGridUpdateClickedCommand : ICommand
{
    public GameGridUpdateClickedCommand()
    {

    }
    public bool Execute(Context context)
    {
        return true;
    }
}
