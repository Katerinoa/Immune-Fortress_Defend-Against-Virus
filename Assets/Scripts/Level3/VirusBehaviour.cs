using System;
using UnityEngine;

public class VirusBehaviour : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // 旋转速度
    public float rotationRange = 5.0f; // 旋转轴的变化幅度

    private Vector3 rotationAxis; // 旋转轴

    private bool isStopped = false;
    void Start()
    {
        // 初始化旋转轴
        rotationAxis = UnityEngine.Random.onUnitSphere;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("hair") && !isStopped)
        {
            isStopped = true; // 标记停止旋转
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("mucous") && !isStopped)
        {
            isStopped = true; // 标记停止旋转
        }
    }

    void Update()
    {
        // 添加随机偏移量
        rotationAxis += UnityEngine.Random.insideUnitSphere * rotationRange * Time.deltaTime;
        rotationAxis.Normalize();

        if (!isStopped)
            transform.RotateAround(transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
