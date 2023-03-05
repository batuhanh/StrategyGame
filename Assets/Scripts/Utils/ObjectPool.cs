using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Utils
{
    public class ObjectPool
    {
        private int _size;
        GameObject _prefab;
        Transform _parent;
        private List<GameObject> pool = new List<GameObject>();
        public ObjectPool(GameObject prefab, int size, Transform parent)
        {
            _prefab = prefab;
            _size = size;
            _parent = parent;
            CreatePoolObjects();
        }
        private void CreatePoolObjects()
        {
            for (int i = 0; i < _size; i++)
            {
                GameObject spawnedObj = GameObject.Instantiate(_prefab, _parent);
                spawnedObj.SetActive(false);
                pool.Add(spawnedObj);
            }
        }
        public GameObject GetObject()
        {
            int i = 0;
            while (pool[i].activeSelf)
            {
                i++;
                if (i >= pool.Count)
                {
                    GameObject spawnedObj = GameObject.Instantiate(_prefab, _parent);
                    pool.Add(spawnedObj);
                    return spawnedObj;
                }
            }
            pool[i].SetActive(true);
            return pool[i];
        }
        public void ReleaseObject(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}