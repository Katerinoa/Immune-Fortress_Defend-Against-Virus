/**
 * 该脚本用于控制病毒的行为
 */
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class VirusController : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;     // 移动方向
    public float deathTime = 3f;                    // 死亡时间
    public String destinationName;                  //目标名称
    public bool isStopped = false;                  // 复用对象池需要使用

    private GameObject targetObject;                // 目标物体 根据名称获取
    private float speed;                            // 移动速度
    private bool hasLanded = false;                 // 是否落地
    private Vector3 swingDirection = Vector3.zero;  // 随机摆动方向
    private float swingSpeed = 2f;                  // 摆动速度
    private float maxSwingAngle = 10f;              // 最大摆动角度
    private Rigidbody rig;
    private NavMeshAgent agent;


    private void Start()
    {
        targetObject = GameObject.Find(destinationName);  // 寻路目的地
        rig = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;  // 开始时不启用navMesh
        speed = Core.Speed;     // 从Core中获取移动速度
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 碰撞到纤毛
        if (collision.gameObject.CompareTag("hair") && !isStopped)
        {
            isStopped = true; // 标记已经停止
            StartCoroutine(DestroyAfterDelay(deathTime)); // 启动协程 销毁病毒
        }

        // 落地
        if (!hasLanded)
        {
            if (collision.contacts[0].normal == Vector3.up) // 判断是否落地
            {
                hasLanded = true; // 标记已经落地
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 碰到伤口
        if (other.CompareTag("hurt"))
        {
            StartCoroutine(DestroyAfterDelay(0)); // 启动协程 销毁病毒
            Counter.passCount++; //入侵数量加一
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // 停留在粘液中
        if (other.CompareTag("mucous") && agent.enabled)
        {
            agent.speed *= 0.99f;
            if (agent.speed <0.1)
                agent.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // 离开粘液
        if (other.CompareTag("mucous"))
        {
            if (agent.enabled == false)
                agent.enabled = true;
            agent.speed = Core.Speed * 0.6f;
        }
    }
    // 延时销毁
    private IEnumerator DestroyAfterDelay(float delay)
    {
        Counter.destroyCount++;
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); //使用对象池 因此不需要Destroy
    }

    private void FixedUpdate()
    {
        if (!isStopped && !hasLanded)
        {
            // 随机摆动 避免排队直线移动
            Vector3 directionToSwing = Quaternion.Euler(0, UnityEngine.Random.Range(-maxSwingAngle, maxSwingAngle), 0) * direction;
            swingDirection = Vector3.Lerp(swingDirection, directionToSwing, Time.fixedDeltaTime * swingSpeed).normalized;
            transform.position += (direction.normalized + swingDirection) * speed * Time.fixedDeltaTime; // 沿着指定方向和摆动方向移动
        }
        else if(!isStopped && hasLanded)
        {
            if(agent.enabled == false)
            {
                agent.enabled = true;
                agent.SetDestination(targetObject.transform.position); // 落地后启用寻路系统
            }
        }
        else if (isStopped)
        {
            rig.constraints = RigidbodyConstraints.FreezeAll; // 锁定位置
            if (agent.enabled == true)
            {
                agent.enabled = false; // 停用寻路系统
            }
        }

        if (Vector3.Distance(transform.position,targetObject.transform.position) < 1f)
        {
            gameObject.SetActive(false);
            Counter.passCount++; // 到达目的地 入侵数量加一
        }
    }
}
