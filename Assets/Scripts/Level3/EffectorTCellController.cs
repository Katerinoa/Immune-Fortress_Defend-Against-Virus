/* 该脚本用于控制效应T细胞 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorTCellController : MonoBehaviour
{
    public float speed;                     // 移动速度
    public float attackRange = 20f;         // 攻击范围

    private GameObject[] cells;             // 所有细胞的集合
    private GameObject targetCell;          // 目标细胞
    private Vector3 startPos;               // 初始位置，用于浮动
    private bool isSleeping = false;        // 是否处于休眠状态
    private float sleepDuration = 10f;      // 休眠持续时间

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // 目标死亡
        if (targetCell != null && !targetCell.activeSelf)
        {
            startPos = transform.position;
            targetCell = null;
        }

        if (targetCell == null || isSleeping)
        {
            // 浮动效果
            float offset = Mathf.Sin(Time.time * 5f + (startPos.x + startPos.y) * 100) * 0.2f;
            Vector3 newPos = startPos + new Vector3(0f, offset, 0f);
            transform.position = newPos;

            SelectTarget(); //寻找目标
        }

        if (targetCell != null)
        {
            if (isSleeping)
            {
                return; // 处于休眠状态
            }

            Vector3 direction = targetCell.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime); // 转向目标物体
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    // 寻找最近目标
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

    // 冷却
    public void Sleep()
    {
        StartCoroutine(EnterSleepMode());
    }

    // 冷却计时
    private IEnumerator EnterSleepMode()
    {
        isSleeping = true;
        StartCoroutine(ChangeColor(gameObject, new Color(0f,0.75f,1f), 1)); // 变色

        yield return new WaitForSeconds(sleepDuration);

        StartCoroutine(ChangeColor(gameObject, Color.white, 1)); // 变色
        isSleeping = false;
    }

    // 颜色渐变
    IEnumerator ChangeColor(GameObject targetCell, Color targetColor, float duration)
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            Color currentColor = Color.Lerp(Color.white, targetColor, timeElapsed / duration);

            // 改变材质的颜色
            if (targetCell != null)
                targetCell.GetComponentInChildren<Renderer>().material.SetColor("_Color", currentColor);

            // 更新已经过去的时间
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        // 时间到了就停止渐变
        if (targetCell != null)
            targetCell.GetComponentInChildren<Renderer>().material.SetColor("_Color", targetColor);
    }
}
