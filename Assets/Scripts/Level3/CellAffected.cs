using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAffected : MonoBehaviour
{
    public bool hasInfected = false;
    public int virusCount = 0;                  // ��Ⱦ��Ŀ
    public float infectedTime = 0;              // ��Ⱦ��ʼʱ��
    private float maxInfectedTime = 5f;         // �����Ⱦʱ��
    //private int maxVirus = 3;                   // �����Ⱦ��Ŀ

    void Update()
    {
        if (virusCount != 0)
        {
            StartCoroutine(InfectedTimer(maxInfectedTime));
        }
    }

    IEnumerator InfectedTimer(float maxInfectedTime)
    {
        float infectedTime = 0f;
        while (infectedTime <= maxInfectedTime)
        {
            infectedTime += Time.deltaTime;
            yield return null;
        }

        SpawnMonster();
        gameObject.SetActive(false);
    }
    private void SpawnMonster()
    {
        for (int i = 0; i < virusCount; ++i)
        {
            GameObject virus = ObjectPool.SharedInstance.GetPooledObject();
            if (virus != null)
            {
                virus.transform.position = transform.position;
                Debug.Log(transform.position.y);
                virus.GetComponent<VirusController_Level3>().baseHeight = transform.position.y;
                virus.SetActive(true);
            }
            Counter.generateCount++;
        }
    }
}
