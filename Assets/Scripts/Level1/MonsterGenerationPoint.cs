using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerationPoint : MonoBehaviour
{
    [Tooltip("怪物列表")]
    public List<GameObject> Viruses;
    [Tooltip("生成位置")]
    public Transform TargetPos;

    private Vector2 SpawnIntervalRange; //生成时间间隔
    private int MaxGenerateNum; //最大生成数量


    private void Awake()
    {
        SpawnIntervalRange = Core.SpawnIntervalRange;
        MaxGenerateNum = Core.MaxGenerateNum;
    }
    private void Start()
    {
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
        if (TargetPos == null)
        {
            Debug.LogError("Spawn point is not set!");
            return;
        }

        //int randomIndex = Random.Range(0, Viruses.Count);
        //GameObject virus = Viruses[randomIndex];
        //Instantiate(virus, TargetPos.position, TargetPos.rotation);
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = TargetPos.transform.position;
            bullet.transform.rotation = TargetPos.transform.rotation;
            bullet.SetActive(true);
            //Debug.Log("1");
        }
        Counter.generateCount++;
    }

    private void Update()
    {
        if (Counter.generateCount > MaxGenerateNum)
            StopCoroutine("SpawnCoroutine");
    }


}
