using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.MVC;
using StrategyGame.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame.Core.Managers
{
    public class EffectsManager : MonoBehaviour
    {
        [SerializeField] private GameObject popUpPrefab;
        [SerializeField] private Transform popUpParent;
        private ObjectPool popUpPool;
        
        public static EffectsManager Instance { get; private set; }
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
        private void Start()    
        {
            popUpPool = new ObjectPool(new GameObject[] {popUpPrefab},20, popUpParent);
        }
        public void SpawnPopUpObject(Vector3 spawnPos,string data)
        {
            GameObject popUp = popUpPool.GetObject();
            popUp.transform.position = spawnPos;
            popUp.GetComponentInChildren<TextMeshPro>().text = data;
            StartCoroutine(PoolDestroyDelay(2f,popUp,popUpPool));
        }
        IEnumerator PoolDestroyDelay(float delayAmount,GameObject obj,ObjectPool pool)
        {
            yield return new WaitForSeconds(delayAmount);
            pool.ReleaseObject(obj);
        }
    }
}
