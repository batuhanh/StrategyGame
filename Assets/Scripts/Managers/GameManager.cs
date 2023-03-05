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
        [SerializeField] private ProductionMenuView _informationPanelView;
        public void Initialize()
        {
            ProductionMenu productionMenu = new ProductionMenu(_productionMenuView);
            productionMenu.Initialize();

            //TODO
           // InformationPanel.Instance.InformationPanelView = _informationPanelView;
        }
    }

}
