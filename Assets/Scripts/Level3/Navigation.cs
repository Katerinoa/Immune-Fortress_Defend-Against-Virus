using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    /**
     * 说明：对于单个物体，可以直接将targetObject拖进去作为目标物体（优先级更高）
     *      对于预制体，可以填入目标名称，自动寻找该名称物体作为目标物体
     */
    public GameObject targetObject;
    public String ObjectName;
    public Vector2 floatHeightRange = new Vector2(3f, 5f);// 漂浮高度范围
    public float speed = 5f;// 寻路速度
    public float attackDistance = 10f; // 攻击范围
    public string Tag = "cell"; // 搜索标签

    private NavMeshAgent navMeshAgent;
    public GameObject targetCell; // 目标cell物体
    private float floatAmplitude; // 浮动幅度（随机）
    private float baseHeight; // 漂浮的基础高度
    private float floatStartTime;
    private Vector3 tempPosition;

    private void Awake()
    {
        if (targetObject == null)
            targetObject = GameObject.Find(ObjectName);
        // 获取寻路组件
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
    }

    void Start()
    {
        floatAmplitude = UnityEngine.Random.Range(1f, 3.0f); // 初始化上下浮动的幅度

        // 初始化寻路速度
        navMeshAgent.speed = speed;
        // 设置寻路的终点
        navMeshAgent.SetDestination(targetObject.transform.position);
        // 初始化漂浮高度
        baseHeight = UnityEngine.Random.Range(floatHeightRange.x, floatHeightRange.y);

        floatStartTime = Time.time;

    }

    void Update()
    {
        // 判断当前目标是否已经死亡 
        if (targetCell != null && !targetCell.activeSelf)
            targetCell = null;

        if (targetCell == null)
        {
            if (navMeshAgent.enabled == false)
            {
                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(targetObject.transform.position);
                transform.position = tempPosition; // 消除取消寻路时候的位置瞬移

            }
            else
            {
                float yPosition = Mathf.Sin((Time.time - floatStartTime) / floatAmplitude) * floatAmplitude;
                transform.position = new Vector3(transform.position.x, transform.position.y + yPosition + baseHeight, transform.position.z);
                tempPosition = transform.position;
            }

            SelectTarget();
        }

        ////如果targetCell不为空，则向其移动
        else
        {

            if (navMeshAgent.enabled == true)
            {
                navMeshAgent.enabled = false; // 停止寻路
                transform.position = tempPosition; // 消除取消寻路时候的位置瞬移
            }

            // 计算需要旋转到的角度
            Vector3 targetDir = targetCell.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);

            // 平滑转向
            float rotateSpeed = 100f; // 旋转速度
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            transform.rotation = rotation;

            // 向目标移动
            transform.position += transform.forward * speed * Time.deltaTime;
            tempPosition = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("cell"))
        {
            for (int i = 0; i < 2; i++)
                SpawnMonster();
            collision.gameObject.SetActive(false);
            //gameObject.SetActive(false);
        }
    }

    private void SpawnMonster()
    {
        GameObject virus = ObjectPool.SharedInstance.GetPooledObject();
        if (virus != null)
        {
            virus.transform.position = transform.position;
            virus.SetActive(true);
        }
        Counter.generateCount++;
    }

    private void SelectTarget()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag(Tag); // 寻找所有标签为cell的物体
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
