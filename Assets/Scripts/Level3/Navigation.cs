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
    public GameObject VirusPrefeb;
    public GameObject targetObject;
    public String ObjectName;
    public Vector2 floatHeightRange = new Vector2(3f, 5f);// 漂浮高度范围
    public float speed = 5f;// 寻路速度
    public float attackDistance = 10f; // 攻击范围
    public string Tag = "cell"; // 搜索标签

    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private GameObject targetCell; // 目标cell物体
    [SerializeField]
    private float floatAmplitude; // 浮动幅度（随机）
    [SerializeField]
    private float baseHeight; // 漂浮的基础高度
    [SerializeField]
    private float floatStartTime;
    [SerializeField]
    private Vector3 tempPosition;


    private void Awake()
    {
        if (targetObject == null)
            targetObject = GameObject.Find(ObjectName);
    }

    void Start()
    {
        floatAmplitude = UnityEngine.Random.Range(1f, 3.0f); // 初始化上下浮动的幅度

        // 获取寻路组件
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        if (targetCell!=null && !targetCell.activeSelf)
            targetCell = null;

        if (targetCell == null)
        {
            if (navMeshAgent.enabled == false)
            {
                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(targetObject.transform.position);
                transform.position = tempPosition; // 消除重新寻路时候的位置瞬移
                floatStartTime = Time.time;
            }
            else
            {
                float yPosition = Mathf.Sin((Time.time - floatStartTime) / floatAmplitude) * floatAmplitude;
                transform.position = new Vector3(transform.position.x, transform.position.y + yPosition + baseHeight, transform.position.z);
                tempPosition = transform.position;
            }


            SelectTarget();
        }
        //如果targetCell不为空，则向其移动
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
            //transform.position = Vector3.MoveTowards(transform.position, targetCell.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cell"))
        {
            for (int i = 0; i < 3; i++)
                SpawnMonster();
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void SpawnMonster()
    {
        Instantiate(VirusPrefeb, transform.position, transform.rotation);
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
