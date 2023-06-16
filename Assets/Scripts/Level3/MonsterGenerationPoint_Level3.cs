/**
 * �ýű����ڿ��Ƶ����ص�ˢ�ֵ� 
 * ��ϸע���뿴Level1�е�MonsterGenerationPoint
 */
using System.Collections;
using UnityEngine;

public class MonsterGenerationPoint_Level3 : MonoBehaviour
{
    public Vector2 SpawnIntervalRange;// ����ʱ����

    private Transform TargetPos; //����λ��
    private int MaxGenerateNum; //�����������
    private static int generateCount = 0;

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
            virus.GetComponent<VirusController_Level3>().isStopped = false;
            virus.SetActive(true);
        }
        generateCount++;
    }

    private void Update()
    {
        if (generateCount >= MaxGenerateNum)
            StopCoroutine("SpawnCoroutine");
    }
}
