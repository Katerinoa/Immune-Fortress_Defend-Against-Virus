using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAffected : MonoBehaviour
{
    private string Tag = "virus";
    public bool hasInfected = false;
    public int virusCount = 0;                  // ��Ⱦ��Ŀ
    public float infectedTime = 0;              // ��Ⱦ��ʼʱ��
    private float maxInfectedTime = 5f;         // �����Ⱦʱ��
    private int maxVirus = 3;                   // �����Ⱦ��Ŀ

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
