using StrategyGame.MVC.Models;
using StrategyGame.MVC.Views;
using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.MVC.Controllers
{
    public class ProductionMenuController : BaseController <ProductionMenuModel, ProductionMenuView>
    {
        public ProductionMenuModel model { get; private set; }
        public ProductionMenuView view { get; private set; }
        public ProductionMenuController(ProductionMenuModel model, ProductionMenuView view) : base(model, view)
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
