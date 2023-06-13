using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EffectorTCellController : MonoBehaviour
{
    public float speed;

    public GameObject[] cells;
    private GameObject targetCell; // Ŀ��ϸ��
    private Vector3 startPos;          // ��ʼλ��
    NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.enabled = false;
        startPos = transform.position;
    }

    void Update()
    {
        if (targetCell != null && !targetCell.activeSelf)
        {
            startPos = transform.position;
            targetCell = null;
            agent.enabled = false;
        }

        if (targetCell == null)
        {
            SelectTarget();
            float offset = Mathf.Sin(Time.time*5f) * 0.2f;
            Vector3 newPos = startPos + new Vector3(0f, offset, 0f);
            transform.position = newPos;
        }

    }

    private void SelectTarget()
    {
        cells = GameObject.FindGameObjectsWithTag("cell");
        List<GameObject> targetCells = new List<GameObject>();

        foreach (GameObject cell in cells)
        {
            CellAffected cellAffected = cell.GetComponent<CellAffected>();

            if (cellAffected != null && cellAffected.hasInfected)
            { 
                targetCells.Add(cell);
            }
        }

        if (targetCells.Count > 0)
        {
            targetCell = targetCells[UnityEngine.Random.Range(0, targetCells.Count)];
            agent.enabled = true;
            agent.SetDestination(targetCell.transform.position);
            agent.baseOffset = targetCell.transform.position.y;
        }
    }
}
