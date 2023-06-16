/**
 * 该脚本用于控制抗体
 */
using UnityEngine;

public class AntibodyController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 20f;           // 控制飞行速度的变量
    public float rotateSpeed = 500f;    // 控制旋转速度的变量

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // 获取刚体组件
    }

    void Update()
    {
        // 向前移动
        Vector3 velocity = transform.forward * speed;
        rb.velocity = velocity;
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);// 碰到任何物体都销毁

        if (collision.gameObject.CompareTag("virus"))
        {
            collision.gameObject.SetActive(false); // 击杀病毒
        }
    }
}
