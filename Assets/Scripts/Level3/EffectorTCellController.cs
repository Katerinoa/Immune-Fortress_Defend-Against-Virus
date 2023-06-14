using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EffectorTCellController : MonoBehaviour
{
    public float speed;             //移动速度
    public float attackRange = 20f; //攻击范围

    private GameObject[] cells;    // 所有细胞的集合
    private GameObject targetCell; // 目标细胞
    private Vector3 startPos;      // 初始位置 用于浮动

    private void Start()
    {
        startPos = transform.position;
    }

    //void Update()
    //{
    //    if (targetCell != null && !targetCell.activeSelf)
    //    {
    //        startPos = transform.position;
    //        targetCell = null;
    //        agent.enabled = false;
    //    }

    //    if (targetCell == null)
    //    {
    //        SelectTarget();
    //        float offset = Mathf.Sin(Time.time * 5f + (startPos.x+startPos.y)*100) * 0.2f;
    //        Vector3 newPos = startPos + new Vector3(0f, offset, 0f);
    //        transform.position = newPos;
    //    }

    //}

    void Update()
    {
        if (targetCell != null && !targetCell.activeSelf)
        {
            startPos = transform.position;
            targetCell = null;
        }

        if (targetCell == null)
        {
            float offset = Mathf.Sin(Time.time * 5f + (startPos.x + startPos.y) * 100) * 0.2f;
            Vector3 newPos = startPos + new Vector3(0f, offset, 0f);
            transform.position = newPos;
            SelectTarget();
        }
        if (targetCell != null)
        {
            Vector3 direction = targetCell.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);// 转向目标物体
            transform.position += transform.forward * speed * Time.deltaTime;
        }

    }

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
            targetCell = targetCells[UnityEngine.Random.Range(0, targetCells.Count)];
        }
    }
}
