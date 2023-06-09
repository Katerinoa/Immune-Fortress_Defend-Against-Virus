using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    /**
     * ˵�������ڵ������壬����ֱ�ӽ�targetObject�Ͻ�ȥ��ΪĿ�����壨���ȼ����ߣ�
     *      ����Ԥ���壬��������Ŀ�����ƣ��Զ�Ѱ�Ҹ�����������ΪĿ������
     */
    public GameObject VirusPrefeb;
    public GameObject targetObject;
    public String ObjectName;
    public Vector2 floatHeightRange = new Vector2(3f, 5f);// Ư���߶ȷ�Χ
    public float speed = 5f;// Ѱ·�ٶ�
    public float attackDistance = 10f; // ������Χ
    public string Tag = "cell"; // ������ǩ

    [SerializeField]
    private NavMeshAgent navMeshAgent;
    public GameObject targetCell; // Ŀ��cell����
    private float floatAmplitude; // �������ȣ������
    [SerializeField]
    private float baseHeight; // Ư���Ļ����߶�
    [SerializeField]
    private float floatStartTime;
    [SerializeField]
    private Vector3 tempPosition;


    private void Awake()
    {
        if (targetObject == null)
            targetObject = GameObject.Find(ObjectName);
        // ��ȡѰ·���
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
    }

    void Start()
    {
        floatAmplitude = UnityEngine.Random.Range(1f, 3.0f); // ��ʼ�����¸����ķ���

        // ��ʼ��Ѱ·�ٶ�
        navMeshAgent.speed = speed;
        // ����Ѱ·���յ�
        navMeshAgent.SetDestination(targetObject.transform.position);
        // ��ʼ��Ư���߶�
        baseHeight = UnityEngine.Random.Range(floatHeightRange.x, floatHeightRange.y);

        floatStartTime = Time.time;

    }

    void Update()
    {
        // �жϵ�ǰĿ���Ƿ��Ѿ����� 
        if (targetCell != null && !targetCell.activeSelf)
            targetCell = null;

        if (targetCell == null)
        {
            if (navMeshAgent.enabled == false)
            {
                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(targetObject.transform.position);
                transform.position = tempPosition; // ����ȡ��Ѱ·ʱ���λ��˲��

            }
            else
            {
                float yPosition = Mathf.Sin((Time.time - floatStartTime) / floatAmplitude) * floatAmplitude;
                transform.position = new Vector3(transform.position.x, transform.position.y + yPosition + baseHeight, transform.position.z);
                tempPosition = transform.position;
            }

            SelectTarget();
        }

        ////���targetCell��Ϊ�գ��������ƶ�
        else
        {

            if (navMeshAgent.enabled == true)
            {
                navMeshAgent.enabled = false; // ֹͣѰ·
                transform.position = tempPosition; // ����ȡ��Ѱ·ʱ���λ��˲��
            }

            // ������Ҫ��ת���ĽǶ�
            Vector3 targetDir = targetCell.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);

            // ƽ��ת��
            float rotateSpeed = 100f; // ��ת�ٶ�
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            transform.rotation = rotation;

            // ��Ŀ���ƶ�
            transform.position += transform.forward * speed * Time.deltaTime;
            tempPosition = transform.position;
            //transform.position = Vector3.MoveTowards(transform.position, targetCell.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cell"))
        {
            for (int i = 0; i < 2; i++)
                SpawnMonster();
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void SpawnMonster()
    {
        Instantiate(VirusPrefeb, transform.position, transform.rotation);
        Counter.generateCount++;
    }

    private void SelectTarget()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag(Tag); // Ѱ�����б�ǩΪcell������
        List<GameObject> targetCells = new List<GameObject>();

        for (int i = 0; i < cells.Length; i++)
            if (Vector3.Distance(transform.position, cells[i].transform.position) <= attackDistance)
                targetCells.Add(cells[i]);

        if (targetCells.Count > 0)
        {
            targetCell = targetCells[UnityEngine.Random.Range(0, targetCells.Count)];
        }
    }
}
