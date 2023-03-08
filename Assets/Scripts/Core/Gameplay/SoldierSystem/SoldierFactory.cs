using StrategyGame.MVC;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace StrategyGame.Core.Gameplay.BuildingSystem
{
    public class SoldierFactory : MonoBehaviour
    {
        public static SoldierFactory Instance { get; private set; }
        [SerializeField] private GameObject _sergeantPrefab;
        [SerializeField] private GameObject _captainPrefab;
        [SerializeField] private GameObject _colonelPrefab;

        [SerializeField] private Transform _soldiersParent;
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
        public GameObject GetSoldier(string soldierTypeStr)
        {
            GameObject desiredPrefab;
            switch (soldierTypeStr)
            {
                case "Sergeant":
                    desiredPrefab = _sergeantPrefab;
                    break;
                case "Captain":
                    desiredPrefab = _captainPrefab;
                    break;
                case "Colonel":
                    desiredPrefab = _colonelPrefab;
                    break;
                default:
                    return null;
            }
            Vector3 spawnPos = new Vector3();
            return Instantiate(desiredPrefab, spawnPos, Quaternion.identity, _soldiersParent);
        }
    }
}
