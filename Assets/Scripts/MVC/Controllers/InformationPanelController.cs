using StrategyGame.MVC.Models;
using StrategyGame.MVC.Views;
using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.MVC.Controllers
{
    public class InformationPanelController : BaseController<InformationPanelModel, InformationPanelView>
    {
        public InformationPanelModel model { get; private set; }
        public InformationPanelView view { get; private set; }
        public InformationPanelController(InformationPanelModel model, InformationPanelView view) : base(model, view)
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
