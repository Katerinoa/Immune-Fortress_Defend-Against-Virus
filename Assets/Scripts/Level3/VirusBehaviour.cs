using System;
using UnityEngine;

public class VirusBehaviour : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // ��ת�ٶ�
    public float rotationRange = 5.0f; // ��ת��ı仯����

    private Vector3 rotationAxis; // ��ת��

    private bool isStopped = false;
    void Start()
    {
        // ��ʼ����ת��
        rotationAxis = UnityEngine.Random.onUnitSphere;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("hair") && !isStopped)
        {
            isStopped = true; // ���ֹͣ��ת
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("mucous") && !isStopped)
        {
            isStopped = true; // ���ֹͣ��ת
        }
    }

    void Update()
    {
        // ������ƫ����
        rotationAxis += UnityEngine.Random.insideUnitSphere * rotationRange * Time.deltaTime;
        rotationAxis.Normalize();

        if (!isStopped)
            transform.RotateAround(transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
