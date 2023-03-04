using StrategyGame.MVC.Views;
using StrategyGame.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Managers
{
    public class GameManager : MonoBehaviour
    {
      
        [SerializeField] private ProductionMenuView _productionMenuView;
        public void Initialize()
        {
            ProductionMenu rollABallMini = new ProductionMenu(_productionMenuView);

            rollABallMini.Initialize();
        }
    }

}
