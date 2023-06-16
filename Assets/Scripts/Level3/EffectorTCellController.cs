/* �ýű����ڿ���ЧӦTϸ�� */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorTCellController : MonoBehaviour
{
    public float speed;                     // �ƶ��ٶ�
    public float attackRange = 20f;         // ������Χ

    private GameObject[] cells;             // ����ϸ���ļ���
    private GameObject targetCell;          // Ŀ��ϸ��
    private Vector3 startPos;               // ��ʼλ�ã����ڸ���
    private bool isSleeping = false;        // �Ƿ�������״̬
    private float sleepDuration = 10f;      // ���߳���ʱ��

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Ŀ������
        if (targetCell != null && !targetCell.activeSelf)
        {
            startPos = transform.position;
            targetCell = null;
        }

        if (targetCell == null || isSleeping)
        {
            // ����Ч��
            float offset = Mathf.Sin(Time.time * 5f + (startPos.x + startPos.y) * 100) * 0.2f;
            Vector3 newPos = startPos + new Vector3(0f, offset, 0f);
            transform.position = newPos;

            SelectTarget(); //Ѱ��Ŀ��
        }

        if (targetCell != null)
        {
            if (isSleeping)
            {
                return; // ��������״̬
            }

            Vector3 direction = targetCell.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime); // ת��Ŀ������
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    // Ѱ�����Ŀ��
    private void SelectTarget()
    {
        cells = GameObject.FindGameObjectsWithTag("cell");
        List<GameObject> targetCells = new List<GameObject>();

        foreach (GameObject cell in cells)
        {
            CellAffected cellAffected = cell.GetComponent<CellAffected>();

            if (cellAffected != null && cellAffected.hasInfected && Vector3.Distance(transform.position, cell.transform.position) < attackRange)
            {
                targetCells.Add(cell);
            }
        }

        if (targetCells.Count > 0)
        {
            targetCell = targetCells[Random.Range(0, targetCells.Count)];
        }
    }

    // ��ȴ
    public void Sleep()
    {
        StartCoroutine(EnterSleepMode());
    }

    // ��ȴ��ʱ
    private IEnumerator EnterSleepMode()
    {
        isSleeping = true;
        StartCoroutine(ChangeColor(gameObject, new Color(0f,0.75f,1f), 1)); // ��ɫ

        yield return new WaitForSeconds(sleepDuration);

        StartCoroutine(ChangeColor(gameObject, Color.white, 1)); // ��ɫ
        isSleeping = false;
    }

    // ��ɫ����
    IEnumerator ChangeColor(GameObject targetCell, Color targetColor, float duration)
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            Color currentColor = Color.Lerp(Color.white, targetColor, timeElapsed / duration);

            // �ı���ʵ���ɫ
            if (targetCell != null)
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
