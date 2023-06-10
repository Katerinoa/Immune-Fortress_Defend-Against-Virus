using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerationPoint_Level3 : MonoBehaviour
{
    public Vector2 SpawnIntervalRange = new Vector2(2.5f, 10.0f); // 生成时间间隔

    private Transform TargetPos; //生成位置
    private int MaxGenerateNum; //最大生成数量

    private void Awake()
    {
        MaxGenerateNum = Core.MaxGenerateNum;
    }
    private void Start()
    {
        TargetPos = transform;
        StartCoroutine("SpawnCoroutine");
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            float interval = Random.Range(SpawnIntervalRange.x, SpawnIntervalRange.y);
            yield return new WaitForSeconds(interval);
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        GameObject virus = ObjectPool.SharedInstance.GetPooledObject();
        if (virus != null)
        {
            virus.transform.position = TargetPos.transform.position;
            virus.transform.rotation = TargetPos.transform.rotation;
            virus.GetComponent<VirusController_Level3>().isStoped = false;
            virus.SetActive(true);
        }
        Counter.generateCount++;
    }
}
