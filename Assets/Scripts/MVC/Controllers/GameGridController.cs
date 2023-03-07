using StrategyGame.MVC.Models;
using StrategyGame.MVC.Views;
using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.MVC.Controllers
{
    public class GameGridController : BaseController<GameGridModel, GameGridView>
    {
        public GameGridModel model { get; private set; }
        public GameGridView view { get; private set; }
        public GameGridController(GameGridModel model, GameGridView view) : base(model, view)
        {

        }
        public override void Initialize(Context context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

            }
        }
    }
}
