using System.Collections.Generic;
using UnityEngine;

public class EffectorBCellController : MonoBehaviour
{
    public GameObject antibodyPrefab;  // ��Ҫ���ɵ�Ԥ����
    public Transform firePos;  // �����λ��
    public string virusTag = "virus";  // ������ǩ����
    private GameObject targetObject;  // Ŀ������transform���
    private AudioSource audiosource;  // ��Ч���

    void Start()
    {
        InvokeRepeating("GenerateAntibody", 0f, 1f);

        audiosource = GetComponentInChildren<AudioSource>();  // ��ȡ��Ч���
    }

    void GenerateAntibody()
    {
        // ��Ԥ����ʵ�����ڷ����λ�ã��������ĳ�������Ϊ��ǰ�������ǰ������z�᷽��
        GameObject antibody = Instantiate(antibodyPrefab, firePos.position, Quaternion.identity);
        antibody.transform.forward = transform.forward;  // ����Ԥ���峯��
        audiosource.Play();  // ������Ч
    }


    void Update()
    {
        if (targetObject != null && !targetObject.activeSelf)
        {
            targetObject = null;
        }
        if (targetObject == null)
        {
            SelectTarget();
        }
        else
        {
            transform.LookAt(targetObject.transform);  // ����Ŀ������
        }
    }

    //private void SelectTarget()
    //{
    //    GameObject virus = GameObject.FindWithTag(virusTag);
    //    if (virus != null)
    //    {
    //        targetObject = virus;
    //    }
    //}

    private void SelectTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Mathf.Infinity, LayerMask.GetMask("Virus"));

        if (colliders.Length > 0)
        {
            GameObject closestVirus = null;
            float minDistance = Mathf.Infinity;

            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (distance < minDistance)
                {
                    closestVirus = collider.gameObject;
                    minDistance = distance;
                }
            }

            targetObject = closestVirus;
        }
    }

}
