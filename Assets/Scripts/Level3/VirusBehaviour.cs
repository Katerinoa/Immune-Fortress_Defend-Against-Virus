/*
 * �ýű����ڿ��Ʋ����������ת
 * */
using System;
using UnityEngine;
using UnityEngine.AI;

public class VirusBehaviour : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // ��ת�ٶ�
    public float rotationRange = 5.0f; // ��ת��ı仯����

    private Vector3 rotationAxis; // ��ת��
    public bool isStopped = false;

    void Start()
    {
        // ��ʼ����ת��
        rotationAxis = UnityEngine.Random.onUnitSphere;
    }

    void Update()
    {
        // �������ƫ����
        rotationAxis += UnityEngine.Random.insideUnitSphere * rotationRange * Time.deltaTime;
        rotationAxis.Normalize();

        if (!isStopped)
            transform.RotateAround(transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
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
}

