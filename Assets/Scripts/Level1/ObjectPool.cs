using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;// 对象池类实例
    [Tooltip("怪物列表")]
    public List<GameObject> Viruses;// 生成怪物列表
    public List<GameObject> pooledObjects;// 对象池对象列表
    private int amountToPool;// 对象池对象数

    void Awake()
    {
        SharedInstance = this;
        amountToPool = Core.MaxGenerateNum / 2;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject virus;
            int randomIndex = Random.Range(0, Viruses.Count);
            virus = Instantiate(Viruses[randomIndex]);
            virus.SetActive(false);
            pooledObjects.Add(virus);
        }
        
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                Debug.Log(i);
                return pooledObjects[i];
            }
        }
        return null;
    }
}
