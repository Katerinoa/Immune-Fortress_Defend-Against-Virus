using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.AI;


public class EffectorBCellController : MonoBehaviour
{
    public GameObject antibodyPrefab;       // ��Ҫ���ɵ�Ԥ����
    public Transform firePos;               // �����λ��
    public bool crazy;                      // �Ƿ�ǿ��
    public float attackRange = 20f;         // ��������
    public bool isRunning;                 // �Ƿ���ǿ����
    public float fireSpeed;


    private GameObject targetObject;        // Ŀ������
    private AudioSource audiosource;        // ��Ч���
    private Vector3 startPos;               // ��ʼλ��
    private float crazyTime;

    private void Awake()
    {
        fireSpeed = Core_Level3.fireSpeed;
        crazyTime = Core_Level3.crazyTime;
    }

    void Start()
    {
        startPos = transform.position;
        InvokeRepeating("GenerateAntibody", UnityEngine.Random.Range(0,1f), 1/fireSpeed);
        audiosource = GetComponentInChildren<AudioSource>();  // ��ȡ��Ч���
    }

    void GenerateAntibody()
    {
        if (targetObject == null)
            return;
        GameObject antibody = Instantiate(antibodyPrefab, firePos.position, Quaternion.identity);
        antibody.transform.LookAt(targetObject.transform.position);
        antibody.GetComponent<AntibodyController>().targetTransform = targetObject.transform;
        audiosource.Play();  // ������Ч
    }


    void Update()
    {
        if (targetObject != null)
        {
            /* ���������߼� */
            // Ŀ������
            if (!targetObject.activeSelf)
            {
                targetObject = null;
                return;
            }
            // Ŀ�����ϸ��
            else if (targetObject.GetComponent<VirusController_Level3>().innerCell)
            {
                targetObject = null;
                return;
            }
            // Ŀ��̫Զ
            else if (Vector3.Distance(transform.position, targetObject.transform.position) > attackRange)
            {
                targetObject = null;
                return;
            }

            Vector3 direction = targetObject.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f*Time.deltaTime);// ת��Ŀ������

        }

        if (targetObject == null)
        {
            SelectTarget();
        }

        float offset = Mathf.Sin(Time.time * 5f + (startPos.x + startPos.y) * 100) * 0.2f;
        Vector3 newPos = startPos + new Vector3(0f, offset, 0f);
        transform.position = newPos;

        if (crazy && !isRunning)
        {
            isRunning = true;
            crazy = false;
            StartCoroutine(CrazyTime());
        }
        else if(crazy)
        {
            crazy = false;
        }
    }

    IEnumerator CrazyTime()
    {
        attackRange *= 2;
        StartCoroutine(ChangeColor(gameObject, new Color(1.0f,0.5f,0.5f,1.0f), 1)); // ��ɫ
        CancelInvoke("GenerateAntibody");
        InvokeRepeating("GenerateAntibody",0, 1 / fireSpeed);

        yield return new WaitForSeconds(crazyTime);

        isRunning = false;
        attackRange /= 2;
        CancelInvoke("GenerateAntibody");
        InvokeRepeating("GenerateAntibody", 0, 0.5f / fireSpeed);
        StartCoroutine(ChangeColor(gameObject, Color.white, 1)); // ��ɫ

    }

    private void SelectTarget()
    {
        GameObject[] viruses = GameObject.FindGameObjectsWithTag("virus");

        if (viruses.Length > 0)
        {
            GameObject closestVirus = null;
            float minDistance = Mathf.Infinity;

            foreach (GameObject virus in viruses)
            {
                float distance = Vector3.Distance(transform.position, virus.transform.position);

                if (virus.GetComponent<VirusController_Level3>().innerCell)
                    continue;
                if (Vector3.Distance(transform.position, virus.transform.position) > attackRange)
                    continue;

                if (distance < minDistance)
                {
                    closestVirus = virus;
                    minDistance = distance;
                }
            }

            targetObject = closestVirus;

        }
    }

    IEnumerator ChangeColor(GameObject targetCell, Color targetColor, float duration)
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            Color currentColor = Color.Lerp(Color.white, targetColor, timeElapsed / duration);

            // �ı���ʵ���ɫ
            if(targetCell != null)
                targetCell.GetComponentInChildren<Renderer>().material.SetColor("_Color", currentColor);

            // �����Ѿ���ȥ��ʱ��
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        // ʱ�䵽�˾�ֹͣ����
        if (targetCell != null)
            targetCell.GetComponentInChildren<Renderer>().material.SetColor("_Color", targetColor);
    }

}
