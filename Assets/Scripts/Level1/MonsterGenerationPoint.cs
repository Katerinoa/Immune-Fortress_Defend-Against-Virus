using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerationPoint : MonoBehaviour
{
    [Tooltip("�����б�")]
    public List<GameObject> Viruses;
    [Tooltip("����λ��")]
    public Transform TargetPos;
    [Tooltip("���ɼ��")]
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
