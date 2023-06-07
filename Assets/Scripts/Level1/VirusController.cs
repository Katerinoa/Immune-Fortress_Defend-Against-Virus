using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController : MonoBehaviour
{
    [Tooltip("移动方向")]
    public Vector3 direction = Vector3.forward;
    [Tooltip("死亡时间")]
    public float deathTime = 3f;

    private float speed;

    private bool hasLanded = true;

    public  bool isStopped = false;// 复用对象池需要使用

    private Rigidbody rig;

    private Vector3 swingDirection = Vector3.zero; // 摆动方向
    private float swingSpeed = 2f; // 摆动速度
    private float maxSwingAngle = 10f; // 最大摆动角度

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        speed = Core.Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (!hasLanded)
        //{
        //    if (collision.contacts[0].normal == Vector3.up) // 判断是否落地
        //    {
        //        hasLanded = true; // 标记已经落地
        //    }
        //}

        if (collision.gameObject.CompareTag("hair") && !isStopped)
        {
            isStopped = true; // 标记已经停止
            StartCoroutine(DestroyAfterDelay(deathTime)); // 启动协程
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hurt"))
        {
            StartCoroutine(DestroyAfterDelay(0)); // 启动协程
            Counter.passCount++;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("mucous"))
        {
            speed *= 0.99f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("mucous"))
            speed = Core.Speed*0.3f;
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        Counter.destroyCount++;
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!isStopped && hasLanded)
        {
            // 计算摆动方向
            Vector3 directionToSwing = Quaternion.Euler(0, Random.Range(-maxSwingAngle, maxSwingAngle), 0) * direction;
            swingDirection = Vector3.Lerp(swingDirection, directionToSwing, Time.fixedDeltaTime * swingSpeed).normalized;

            transform.position += (direction.normalized + swingDirection) * speed * Time.fixedDeltaTime; // 沿着指定方向和摆动方向移动
        }
        else if (isStopped)
        {
            rig.constraints = RigidbodyConstraints.FreezeAll; // 锁定位置
        }

        if (transform.position.y < -1)
        {
            gameObject.SetActive(false);
            Counter.passCount++;
        }
    }
}
