/* �ýű����ڿ���ˢ�ֵ� */
using System.Collections;
using UnityEngine;

public class MonsterGenerationPoint : MonoBehaviour
{
    public Vector2 SpawnIntervalRange = new Vector2(2.5f, 10.0f); // ����ʱ����

    private Transform TargetPos; //����λ��
    private int MaxGenerateNum; //�����������

    private void Awake()
    {
        MaxGenerateNum = Core.MaxGenerateNum; // ��ȡ�����������
    }
    private void Start()
    {
        TargetPos = transform;
        StartCoroutine("SpawnCoroutine"); // ��һ��Э�����ɲ���
    }

    // ���ɲ���Э��
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            float interval = Random.Range(SpawnIntervalRange.x, SpawnIntervalRange.y);
            yield return new WaitForSeconds(interval);
            SpawnMonster();
        }
    }

    // ����һ������
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
            StopCoroutine("SpawnCoroutine"); // �ﵽ�����������ֹЭ�� ֹͣ���ɹ���
    }


}
