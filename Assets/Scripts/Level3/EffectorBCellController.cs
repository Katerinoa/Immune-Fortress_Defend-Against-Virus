using System.Collections.Generic;
using UnityEngine;

public class EffectorBCellController : MonoBehaviour
{
    public GameObject antibodyPrefab;  // 需要生成的预制体
    public Transform firePos;  // 发射点位置
    public string virusTag = "virus";  // 病毒标签名称
    public GameObject targetObject;  // 目标物体
    private AudioSource audiosource;  // 音效组件
    private Vector3 startPos;          // 初始位置
    public float attackRange = 10f;
    public float fireRate = 1f; // 发射频率

    void Start()
    {
        startPos = transform.position;
        InvokeRepeating("GenerateAntibody", Random.Range(0,1f), 1 / fireRate);
        audiosource = GetComponentInChildren<AudioSource>();  // 获取音效组件
    }

    void GenerateAntibody()
    {
        if (targetObject == null)
            return;
        GameObject antibody = Instantiate(antibodyPrefab, firePos.position, Quaternion.identity);
        antibody.transform.LookAt(targetObject.transform.position);
        antibody.GetComponent<AntibodyController>().targetTransform = targetObject.transform;
        audiosource.Play();  // 播放音效
    }


    void Update()
    {
        if (targetObject != null)
        {
            /* 重新搜索逻辑 */
            // 目标死亡
            if (!targetObject.activeSelf)
            {
                targetObject = null;
                return;
            }
            // 目标进入细胞
            else if (targetObject.GetComponent<VirusController_Level3>().innerCell)
            {
                targetObject = null;
                return;
            }
            // 目标太远
            else if (Vector3.Distance(transform.position, targetObject.transform.position) > attackRange)
            {
                targetObject = null;
                return;
            }

            transform.LookAt(targetObject.transform);  // 面向目标物体

        }

        if (targetObject == null)
        {
            SelectTarget();
        }

        float offset = Mathf.Sin(Time.time * 5f + (startPos.x + startPos.y) * 100) * 0.2f;
        Vector3 newPos = startPos + new Vector3(0f, offset, 0f);
        transform.position = newPos;
    }

    private void SelectTarget()
    {
        GameObject[] viruses = GameObject.FindGameObjectsWithTag("virus");

        if (viruses.Length > 0)
        {
            GameObject closestVirus = null;
            float minDistance = Mathf.Infinity;

            foreach (GameObject virus in viruses)
            {
                float distance = Vector3.Distance(transform.position, virus.transform.position);

                if (virus.GetComponent<VirusController_Level3>().innerCell)
                    continue;
                if (Vector3.Distance(transform.position, virus.transform.position) > attackRange)
                    continue;

                if (distance < minDistance)
                {
                    closestVirus = virus;
                    minDistance = distance;
                }
            }

            targetObject = closestVirus;

        }
    }

}
