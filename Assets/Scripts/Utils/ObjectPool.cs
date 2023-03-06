using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Utils
{
    public class ObjectPool
    {
        private int _size;
        GameObject[] _prefabs;
        Transform _parent;
        private List<GameObject> pool = new List<GameObject>();
        public ObjectPool(GameObject[] prefabs, int size, Transform parent)
        {
            _prefabs = prefabs;
            _size = size;
            _parent = parent;
            CreatePoolObjects();
        }
        public void CreatePoolObjects()
        {
            for (int i = 0; i < _size; i++)
            {
                GameObject spawnedObj = GameObject.Instantiate(_prefabs[i%_prefabs.Length], _parent);
                spawnedObj.SetActive(false);
                pool.Add(spawnedObj);
            }
        }
        public virtual GameObject GetObject()
        {
            int i = 0;
            while (pool[i].activeSelf)
            {
                i++;
                if (i >= pool.Count)
                {
                    GameObject spawnedObj = GameObject.Instantiate(_prefabs[i % _prefabs.Length], _parent);
                    pool.Add(spawnedObj);
                    return spawnedObj;
                }
            }
            pool[i].SetActive(true);
            return pool[i];
        }
        public virtual void ReleaseObject(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}