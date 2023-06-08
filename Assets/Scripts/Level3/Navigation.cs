using System;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public GameObject targetObject; // ��ǰĿ������
    public String ObjectName; //Ŀ����������
    public Vector2 floatHeightRange = new Vector2(3f, 5f);// Ư���߶ȷ�Χ
    public float speed = 5f;// Ѱ·�ٶ�

    private bool isStopped = false;
    private float floatAmplitude; // �������ȣ������
    private float phaseShift; // �������ࣨ�����

    private NavMeshAgent navMeshAgent;

    private float baseHeight; // Ư���Ļ����߶�

    private void Awake()
    {
        if (targetObject == null)
            targetObject = GameObject.Find(ObjectName);
    }

    void Start()
    {
        floatAmplitude = UnityEngine.Random.Range(0.5f, 3.0f); // ��ʼ�����¸����ķ���
        phaseShift = UnityEngine.Random.Range(0f, 2f * Mathf.PI);

        // ��ȡѰ·���
        navMeshAgent = GetComponent<NavMeshAgent>();
        // ��ʼ��Ѱ·�ٶ�
        navMeshAgent.speed = speed;
        // ����Ѱ·���յ�
        navMeshAgent.SetDestination(targetObject.transform.position);
        // ��ʼ��Ư���߶�
        baseHeight = UnityEngine.Random.Range(floatHeightRange.x, floatHeightRange.y);


    }

    void Update()
    {
        //// Ѱ·�յ�
        //navMeshAgent.SetDestination(targetObject.transform.position);

        // ���¸���
        float yPosition = Mathf.Sin(Time.time / floatAmplitude + phaseShift) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, transform.position.y + yPosition + baseHeight, transform.position.z);

    }
}