using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StrategyGame.Core.UI
{
    public class MenuItem : MonoBehaviour
    {
        [SerializeField] private string _buildingType;
        [SerializeField] private Button myButton;
        private void Start()
        {
            myButton.onClick.AddListener(CallFactory);
        }
        private void CallFactory()
        {
            GameObject createdBuilding = BuildingFactory.Instance.GetBuilding(_buildingType);
            GameGrid.Instance.GameGridController.UpdatOffset(createdBuilding.transform.position);
            if (GameGrid.Instance.GameGridView.objToPlace)
                Destroy(GameGrid.Instance.GameGridView.objToPlace.gameObject);
            GameGrid.Instance.GameGridView.objToPlace = createdBuilding.GetComponent<Building>();
        }

    }
}
