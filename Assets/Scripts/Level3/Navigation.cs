using System;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public GameObject targetObject; // 当前目标物体
    public String ObjectName; //目标物体名称
    public Vector2 floatHeightRange = new Vector2(3f, 5f);// 漂浮高度范围
    public float speed = 5f;// 寻路速度

    private bool isStopped = false;
    private float floatAmplitude; // 浮动幅度（随机）
    private float phaseShift; // 浮动初相（随机）

    private NavMeshAgent navMeshAgent;

    private float baseHeight; // 漂浮的基础高度

    private void Awake()
    {
        if (targetObject == null)
            targetObject = GameObject.Find(ObjectName);
    }

    void Start()
    {
        floatAmplitude = UnityEngine.Random.Range(0.5f, 3.0f); // 初始化上下浮动的幅度
        phaseShift = UnityEngine.Random.Range(0f, 2f * Mathf.PI);

        // 获取寻路组件
        navMeshAgent = GetComponent<NavMeshAgent>();
        // 初始化寻路速度
        navMeshAgent.speed = speed;
        // 设置寻路的终点
        navMeshAgent.SetDestination(targetObject.transform.position);
        // 初始化漂浮高度
        baseHeight = UnityEngine.Random.Range(floatHeightRange.x, floatHeightRange.y);


    }

    void Update()
    {
        //// 寻路终点
        //navMeshAgent.SetDestination(targetObject.transform.position);

        // 上下浮动
        float yPosition = Mathf.Sin(Time.time / floatAmplitude + phaseShift) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, transform.position.y + yPosition + baseHeight, transform.position.z);

    }
}