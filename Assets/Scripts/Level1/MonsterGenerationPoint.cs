using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerationPoint : MonoBehaviour
{
    public Vector2 SpawnIntervalRange = new Vector2(2.5f, 10.0f); // 生成时间间隔

    private Transform TargetPos; //生成位置
    private int MaxGenerateNum; //最大生成数量

    private void Awake()
    {
        MaxGenerateNum = Core_Level3.maxGenerateNum;
        SpawnIntervalRange = Core_Level3.SpawnIntervalRange;
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
            virus.SetActive(true);
            virus.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            VirusController controller = virus.GetComponent<VirusController>();
            if (controller != null)
                controller.isStopped = false;
        }
        Counter.generateCount++;
    }

    private void Update()
    {
        if (Counter.generateCount >= MaxGenerateNum)
            StopCoroutine("SpawnCoroutine");
    }


}
