using System.Collections.Generic;
using UnityEngine;
using MyOtherHalf.Tools;

namespace MyOtherHalf.Pooling
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size = 1;
        public bool allowtoIncrement = true;
    }

    public class PoolingSystem  : Singleton<PoolingSystem>
    {

        [SerializeField] private List<Pool> objectsToPool= new List<Pool>();
        [SerializeField] private Transform parentForObjects = null;
        private Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>> ();

        private void Awake () 
        {
            parentForObjects = parentForObjects == null ? transform : parentForObjects;    

            objectsToPool.ForEach(pool => CreatePool(pool));

        }

        public GameObject GetObjectFromPool (string poolName)
        {
            GameObject objectRequested = null;
            Pool poolRequested = GetPoolFromList(poolName);

            if (poolDic.TryGetValue(poolRequested.prefab.name, out Queue<GameObject> poolList)) 
            {
                if (poolList.Count == 0 && poolRequested.allowtoIncrement)
                {
                    objectRequested = CreateObject(poolRequested.prefab);
                }
                else
                {
                    objectRequested = poolList.Dequeue();
                }
                    
                poolList.Enqueue(objectRequested);
                return objectRequested;
            } 
            else 
            { 
                return null; 
            }
        }

        public void ResetObject (GameObject objectToEnqueue, string tag) 
        {
            objectToEnqueue.SetActive (false);
            objectToEnqueue.transform.SetParent(parentForObjects);

            ReturnObject(objectToEnqueue, tag);
        }

        private void CreatePool(Pool newPool)
        {
            Queue<GameObject> objectPool = new Queue<GameObject> ();
            
            for (int i = 0; i < newPool.size; i++)
            {
                objectPool.Enqueue(CreateObject (newPool.prefab));
            }

            if (!poolDic.ContainsKey(newPool.prefab.name))
            {
                poolDic.Add(newPool.prefab.name, objectPool);
            }
        }

        private GameObject CreateObject (GameObject prefab) 
        {
            GameObject obj = Instantiate (prefab);

            obj.name = prefab.name;
            obj.SetActive(false);
            obj.transform.SetParent(parentForObjects);

            return obj;
        }

        private void ReturnObject (GameObject objectToEnqueue, string tag) 
        {
            if (poolDic.TryGetValue (tag, out Queue<GameObject> poolList))
            {
                poolList.Enqueue (objectToEnqueue);
            }
        }

        private Pool GetPoolFromList(string poolName)
        {
            foreach (var pool in objectsToPool)
            {
                if (pool.prefab.name == poolName)
                {
                    return pool;
                }
            }
            return null;
        }
    }
}

