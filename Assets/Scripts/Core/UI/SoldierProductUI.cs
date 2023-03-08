using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame.Core.UI
{
    public class SoldierProductUI : MonoBehaviour
    {
        [SerializeField] private string _soldierType;
        [SerializeField] private Button myButton;
        private void Start()
        {
            myButton.onClick.AddListener(CallFactory);
        }
        private void CallFactory()
        {
            //GameObject createdBuilding = BuildingFactory.Instance.GetBuilding(_buildingType);
        }

    }
}
