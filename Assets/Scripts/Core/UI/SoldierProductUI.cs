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
        public Barrack Barrack { get { return _barrack; } set { _barrack = value; } }

        [SerializeField] private string _soldierType;
        [SerializeField] private Barrack _barrack;
        [SerializeField] private Button myButton;
        private void Start()
        {
            myButton.onClick.AddListener(CallFactory);
        }
        private void CallFactory()
        {
            GameObject createdSoldier = SoldierFactory.Instance.GetSoldier(_barrack, _soldierType);
        }

    }
}
