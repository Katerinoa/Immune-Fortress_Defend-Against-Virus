using System.Collections.Generic;
using UnityEngine;

public class EffectorBCellController : MonoBehaviour
{
    public GameObject antibodyPrefab;  // 需要生成的预制体
    public Transform firePos;  // 发射点位置
    public string virusTag = "virus";  // 病毒标签名称
    private GameObject targetObject;  // 目标物体transform组件
    private AudioSource audiosource;  // 音效组件

    void Start()
    {
        InvokeRepeating("GenerateAntibody", 0f, 1f);

        audiosource = GetComponentInChildren<AudioSource>();  // 获取音效组件
    }

    void GenerateAntibody()
    {
        // 将预制体实例化在发射点位置，并给它的朝向设置为当前物体的正前方（即z轴方向）
        GameObject antibody = Instantiate(antibodyPrefab, firePos.position, Quaternion.identity);
        antibody.transform.forward = transform.forward;  // 设置预制体朝向
        audiosource.Play();  // 播放音效
    }


    void Update()
    {
        if (targetObject != null && !targetObject.activeSelf)
        {
            targetObject = null;
        }
        if (targetObject == null)
        {
            SelectTarget();
        }
        else
        {
            transform.LookAt(targetObject.transform);  // 面向目标物体
        }
    }

    //private void SelectTarget()
    //{
    //    GameObject virus = GameObject.FindWithTag(virusTag);
    //    if (virus != null)
    //    {
    //        targetObject = virus;
    //    }
    //}

    private void SelectTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Mathf.Infinity, LayerMask.GetMask("Virus"));

        if (colliders.Length > 0)
        {
            GameObject closestVirus = null;
            float minDistance = Mathf.Infinity;

            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (distance < minDistance)
                {
                    closestVirus = collider.gameObject;
                    minDistance = distance;
                }
            }

            targetObject = closestVirus;
        }
    }

}
