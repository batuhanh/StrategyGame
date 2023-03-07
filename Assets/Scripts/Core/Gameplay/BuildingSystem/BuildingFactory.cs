using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace StrategyGame.Core.Gameplay.BuildingSystem
{
    public class BuildingFactory : MonoBehaviour
    {
        public static BuildingFactory Instance { get; private set; }
        [SerializeField] private GameObject _barrackPrefab;
        [SerializeField] private GameObject _armoryPrefab;
        [SerializeField] private GameObject _militaryTowerPrefab;
        [SerializeField] private GameObject _powerPlantPrefab;
        [SerializeField] private Transform _buildingsParent;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        public GameObject GetBuilding(string buildingTypeStr)
        {
            GameObject desiredPrefab;
            switch (buildingTypeStr)
            {
                case "Barrack":
                    desiredPrefab = _barrackPrefab;
                    break;
                case "Armory":
                    desiredPrefab = _armoryPrefab;
                    break;
                case "MilitaryTower":
                    desiredPrefab = _militaryTowerPrefab;
                    break;
                case "PowerPlant":
                    desiredPrefab = _powerPlantPrefab;
                    break;
                default:
                    return null;
            }
            return Instantiate(desiredPrefab,Vector3.zero,Quaternion.identity, _buildingsParent);
        }
    }
}
