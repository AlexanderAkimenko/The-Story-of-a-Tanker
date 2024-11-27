using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject Prefab;
        public int Size;
        public int CountNumber; 
    }

    public static ObjectPooler Instance;

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {
        Instance = this;
        PoolInitialize();

    }

    public GameObject SpawnFromPoll(string tag, Vector3 position, Quaternion rotation)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.Log("Missing NO TAG");
            return null;
        }
        else
        {
            if (PoolDictionary[tag] == null)
            {
                Debug.Log("NULL");
                return null;
            }
            else
            {
                GameObject objectToSpawn = PoolDictionary[tag].Dequeue();
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;

                PoolDictionary[tag].Enqueue(objectToSpawn);
                return objectToSpawn;

            }
        }
    }

    private void PoolInitialize()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                ObjectNumber objectNumber = obj.AddComponent<ObjectNumber>();
                objectNumber.Number = pool.CountNumber; 
                objectPool.Enqueue(obj);
                pool.CountNumber++;
            }

            PoolDictionary.Add(pool.Tag, objectPool);

        }
    }
}
