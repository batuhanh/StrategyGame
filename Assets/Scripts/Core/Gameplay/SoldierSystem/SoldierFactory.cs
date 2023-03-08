using JetBrains.Annotations;
using StrategyGame.Core.Gameplay.SoldierSystem;
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
        public GameObject GetSoldier(Barrack currentBarrack,string soldierTypeStr)
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
            GameObject spawnedSoldier = Instantiate(desiredPrefab, _soldiersParent);
            Vector3 spawnPos = GameGrid.Instance.GameGridController.PutSoldierToClosest(
                spawnedSoldier.GetComponent<Soldier>(), currentBarrack.GetStartPosition());
            spawnedSoldier.transform.position = spawnPos;
            return spawnedSoldier;
        }
    }
}
