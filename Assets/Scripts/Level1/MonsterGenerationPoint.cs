using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerationPoint : MonoBehaviour
{
    [Tooltip("怪物列表")]
    public List<GameObject> Viruses;
    [Tooltip("生成位置")]
    public Transform TargetPos;
    [Tooltip("生成间隔")]
    public float SpawnInterval = 5.0f;

    private void Start()
    {
        SpawnMonster();
        StartCoroutine(SpawnIntervalCoroutine());
    }

    private IEnumerator SpawnIntervalCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnInterval);
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

        int randomIndex = Random.Range(0, Viruses.Count);
        GameObject Virus = Viruses[randomIndex];
        Instantiate(Virus, TargetPos.position, TargetPos.rotation);
    }
}
