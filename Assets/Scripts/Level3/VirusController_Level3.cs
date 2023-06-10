using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class VirusController_Level3 : MonoBehaviour
{
    public GameObject VirusPrefeb;
    public String ObjectName;
    public Vector2 floatHeightRange = new Vector2(3f, 5f);// 浮动基础高度范围
    public float baseHeight = 0; // 浮动基础高度
    public float speed = 0.05f;// 移动速度
    public float attackDistance = 10f; // 监测攻击范围

    private GameObject targetObject;
    private string Tag = "cell"; // 目标标签
    private NavMeshAgent navMeshAgent;
    public GameObject targetCell; // 目标物体
    public float floatAmplitude; // 浮动振幅
    private float floatStartTime;
    public bool isStoped = false;

    private void Awake()
    {
        targetObject = GameObject.Find(ObjectName);
 
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
    }

    void Start()
    {
        floatAmplitude = UnityEngine.Random.Range(1f, 3.0f); 

        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(targetObject.transform.position);
        if(baseHeight == 0)
            baseHeight = UnityEngine.Random.Range(floatHeightRange.x, floatHeightRange.y);

        floatStartTime = Time.time;
    }

    void Update()
    {
        /* 以下为病毒移动控制 */
        if (targetCell != null && !targetCell.activeSelf)
        {
            isStoped = false;
            targetCell = null;
            gameObject.GetComponent<VirusAttack>().virusEffect.Stop();
        }

        if (targetCell == null)
        {
            if (navMeshAgent.enabled == false)
            {
                navMeshAgent.enabled = true; // 重新寻路
                navMeshAgent.SetDestination(targetObject.transform.position);
                gameObject.GetComponentInChildren<VirusBehaviour>().isStopped = false;
            }
            float yPosition = Mathf.Sin((Time.time - floatStartTime) / floatAmplitude) * floatAmplitude;
            navMeshAgent.baseOffset = yPosition + baseHeight;

            SelectTarget();
        }
        else
        {
            if (navMeshAgent.enabled == true)
            {
                navMeshAgent.enabled = false; // 终止寻路
            }

            Vector3 targetDir = targetCell.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);

            float rotateSpeed = 100f; // 转向速度
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            if (!isStoped && Vector3.Distance(targetCell.transform.position, transform.position) > 0.5f)
            {
                transform.rotation = rotation;
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            if (Vector3.Distance(targetCell.transform.position, transform.position) <= 0.5f)
            {
                targetCell.GetComponentInChildren<Renderer>().material.SetColor("_Color", new Color(0.71f, 0.92f, 0.62f, 1.0f)); //入侵变绿
            }
        }

    }

    private void SelectTarget()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag(Tag); 
        List<GameObject> targetCells = new List<GameObject>();

        for (int i = 0; i < cells.Length; i++)
            if (Vector3.Distance(transform.position, cells[i].transform.position) <= attackDistance)
                targetCells.Add(cells[i]);

        if (targetCells.Count > 0)
        {
            targetCell = targetCells[UnityEngine.Random.Range(0, targetCells.Count)];
        }
    }

}
