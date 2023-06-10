using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAffected : MonoBehaviour
{
    private string Tag = "virus";
    public bool hasInfected = false;
    public int virusCount = 0;                  // 侵染数目
    public float infectedTime = 0;              // 侵染开始时间
    private float maxInfectedTime = 5f;         // 最大侵染时间
    private int maxVirus = 3;                   // 最大侵染数目

    //B6EA9E

    void Update()
    {
        if (virusCount != 0)
        {
            infectedTime += Time.deltaTime;
            if (infectedTime > maxInfectedTime || virusCount > maxVirus)
            {
                SpawnMonster();
                gameObject.SetActive(false);
            }
        }
    }

    private void SpawnMonster()
    {
        for (int i = 0; i < virusCount; ++ i)
        {
            GameObject virus = ObjectPool.SharedInstance.GetPooledObject();
            if (virus != null)
            {
                virus.transform.position = transform.position;
                virus.GetComponent<VirusController_Level3>().baseHeight = transform.position.y;
                virus.SetActive(true);
            }
            Counter.generateCount++;
        }
    }
}
