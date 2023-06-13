using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntibodyController : MonoBehaviour
{
    Rigidbody rb;  // 刚体组件
    public float speed = 20f;  // 控制飞行速度的变量
    public float rotateSpeed = 500f;   // 控制旋转速度的变量

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // 获取刚体组件
    }

    void Update()
    {
        // 获取物体的前方向并赋值给velocity向量
        Vector3 velocity = transform.forward * speed;
        rb.velocity = velocity;  // 修改刚体速度达到匀速飞行的效果
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("virus"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
