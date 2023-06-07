using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerationPoint : MonoBehaviour
{
    [Tooltip("����λ��")]
    public Transform TargetPos;

    private Vector2 SpawnIntervalRange; //����ʱ����
    private int MaxGenerateNum; //�����������


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

        GameObject virus = ObjectPool.SharedInstance.GetPooledObject();
        if (virus != null)
        {
            virus.transform.position = TargetPos.transform.position;
            virus.transform.rotation = TargetPos.transform.rotation;
            virus.SetActive(true);
            virus.GetComponent<VirusController>().isStopped = false;
            virus.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
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
