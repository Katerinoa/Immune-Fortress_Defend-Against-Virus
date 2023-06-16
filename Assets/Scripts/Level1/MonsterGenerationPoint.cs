/* 该脚本用于控制刷怪点 */
using System.Collections;
using UnityEngine;

public class MonsterGenerationPoint : MonoBehaviour
{
    public Vector2 SpawnIntervalRange = new Vector2(2.5f, 10.0f); // 生成时间间隔

    private Transform TargetPos; //生成位置
    private int MaxGenerateNum; //最大生成数量

    private void Awake()
    {
        MaxGenerateNum = Core.MaxGenerateNum; // 获取最大生成数量
    }
    private void Start()
    {
        TargetPos = transform;
        StartCoroutine("SpawnCoroutine"); // 开一个协程生成病毒
    }

    // 生成病毒协程
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            float interval = Random.Range(SpawnIntervalRange.x, SpawnIntervalRange.y);
            yield return new WaitForSeconds(interval);
            SpawnMonster();
        }
    }

    // 生成一个病毒
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
            StopCoroutine("SpawnCoroutine"); // 达到最大数量则终止协程 停止生成怪物
    }


}
