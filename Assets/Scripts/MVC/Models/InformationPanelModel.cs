using StrategyGame.MVC.Models;
using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.MVC.Models
{
    public class InformationPanelModel : BaseModel
    {
        public IShowable ClickedObject { get { return _clickedObject; } set { _clickedObject = value; } }
        private IShowable _clickedObject;
        public override void Initialize(Context context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

            }
        }
    }
}
