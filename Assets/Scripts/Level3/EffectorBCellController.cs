using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.AI;


public class EffectorBCellController : MonoBehaviour
{
    public GameObject antibodyPrefab;  // ��Ҫ���ɵ�Ԥ����
    public Transform firePos;  // �����λ��
    public string virusTag = "virus";  // ������ǩ����
    public GameObject targetObject;  // Ŀ������
    private AudioSource audiosource;  // ��Ч���
    private Vector3 startPos;          // ��ʼλ��
    public bool crazy;
    private bool isRunning;
    private Coroutine myCoroutine;
    public float attackRange = 10f;
    public float fireRate = 1f; // ����Ƶ��

    void Start()
    {
        startPos = transform.position;
        InvokeRepeating("GenerateAntibody", UnityEngine.Random.Range(0,1f), 1 / fireRate);
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

            transform.LookAt(targetObject.transform);  // ����Ŀ������

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
            crazy = false;
            isRunning = true;
            StartCoroutine(CrazyTime());
        }
    }

    IEnumerator CrazyTime()
    {
        attackRange *= 2;
        StartCoroutine(ChangeColor(gameObject, Color.red, 1f)); // ��ɫ
        CancelInvoke("GenerateAntibody");
        InvokeRepeating("GenerateAntibody", UnityEngine.Random.Range(0,1f), 1 / (fireRate*5));

        yield return new WaitForSeconds(5f);

        attackRange /= 2;

        CancelInvoke("GenerateAntibody");
        InvokeRepeating("GenerateAntibody", UnityEngine.Random.Range(0,1f), 1 / fireRate);

        StartCoroutine(ChangeColor(gameObject, Color.white, 2f)); // ��ɫ
        isRunning = false;

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
