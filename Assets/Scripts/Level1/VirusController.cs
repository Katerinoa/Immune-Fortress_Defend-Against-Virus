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

    private bool hasLanded = false;

    private bool isStopped = false;

    private Rigidbody rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        speed = Core.Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded)
        {
            if (collision.contacts[0].normal == Vector3.up) // 判断是否落地
            {
                hasLanded = true; // 标记已经落地
            }
        }

        if (collision.gameObject.CompareTag("hair") && !isStopped)
        {
            isStopped = true; // 标记已经停止
            StartCoroutine(DestroyAfterDelay(deathTime)); // 启动协程
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); // 销毁该物体
    }

    private void FixedUpdate()
    {
        if (!isStopped && hasLanded)
        {
            transform.position += direction.normalized * speed * Time.fixedDeltaTime; // 沿着指定方向移动
        }
        else if(isStopped)
        {
            rig.constraints = RigidbodyConstraints.FreezeAll; // 锁定位置
        }

        if (transform.position.y < -1)
        {
            Destroy(gameObject);
            Counter.passCount++;
        }
    }
}
