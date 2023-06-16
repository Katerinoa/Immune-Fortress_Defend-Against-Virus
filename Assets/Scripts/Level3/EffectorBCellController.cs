/**
 * �ýű����ڿ���ЧӦBϸ������Ϊ
 */
using UnityEngine;
using System.Collections;

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
    private float crazyTime;                // ǿ��ʱ��

    private void Awake()
    {
        // ��Core�л�ȡ��ֵ
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
        // ���ɿ���
        if (targetObject == null)
            return;
        GameObject antibody = Instantiate(antibodyPrefab, firePos.position, Quaternion.identity);
        antibody.transform.LookAt(targetObject.transform.position);
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

            // ˳��ת��Ŀ�겡��
            Vector3 direction = targetObject.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f*Time.deltaTime);

        }

        if (targetObject == null)
        {
            SelectTarget(); // Ѱ��Ŀ��
        }

        // ���¸���Ч��
        float offset = Mathf.Sin(Time.time * 5f + (startPos.x + startPos.y) * 100) * 0.2f;
        Vector3 newPos = startPos + new Vector3(0f, offset, 0f);
        transform.position = newPos;

        // ǿ������
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

    // ǿ��״̬
    IEnumerator CrazyTime()
    {
        attackRange *= 2; // �������
        StartCoroutine(ChangeColor(gameObject, new Color(1.0f,0.5f,0.5f,1.0f), 1)); // ��ɫ
        CancelInvoke("GenerateAntibody");
        InvokeRepeating("GenerateAntibody", 0, 0.5f / fireSpeed); //�������

        yield return new WaitForSeconds(crazyTime);

        isRunning = false;
        attackRange /= 2; // ���ӻָ�
        CancelInvoke("GenerateAntibody");
        InvokeRepeating("GenerateAntibody", 0, 1f / fireSpeed); //�ָ�����
        StartCoroutine(ChangeColor(gameObject, Color.white, 1)); // ��ɫ

    }

    // Ѱ��Ŀ��
    private void SelectTarget()
    {
        GameObject[] viruses = GameObject.FindGameObjectsWithTag("virus"); // �ҵ����в���

        if (viruses.Length > 0)
        {
            GameObject closestVirus = null;
            float minDistance = Mathf.Infinity;

            foreach (GameObject virus in viruses)
            {
                float distance = Vector3.Distance(transform.position, virus.transform.position);

                if (virus.GetComponent<VirusController_Level3>().innerCell) //�ų�����ϸ����
                    continue;
                if (Vector3.Distance(transform.position, virus.transform.position) > attackRange) // �ų�������Χ֮���
                    continue;

                if (distance < minDistance) // Ѱ�ҷ���Ҫ�������Ĳ���
                {
                    closestVirus = virus;
                    minDistance = distance;
                }
            }
            targetObject = closestVirus;
        }
    }

    // ��ɫ����
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
