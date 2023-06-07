using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;// �������ʵ��
    [Tooltip("�����б�")]
    public List<GameObject> Viruses;// ���ɹ����б�
    public List<GameObject> pooledObjects;// ����ض����б�
    private int amountToPool;// ����ض�����

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
