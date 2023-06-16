/**
 * 该脚本用于控制效应B细胞的行为
 */
using UnityEngine;
using System.Collections;

public class EffectorBCellController : MonoBehaviour
{
    public GameObject antibodyPrefab;       // 需要生成的预制体
    public Transform firePos;               // 发射点位置
    public bool crazy;                      // 是否被强化
    public float attackRange = 20f;         // 攻击距离
    public bool isRunning;                 // 是否在强化中
    public float fireSpeed;


    private GameObject targetObject;        // 目标物体
    private AudioSource audiosource;        // 音效组件
    private Vector3 startPos;               // 初始位置
    private float crazyTime;                // 强化时长

    private void Awake()
    {
        // 从Core中获取数值
        fireSpeed = Core_Level3.fireSpeed;
        crazyTime = Core_Level3.crazyTime;
    }

    void Start()
    {
        startPos = transform.position;
        InvokeRepeating("GenerateAntibody", UnityEngine.Random.Range(0,1f), 1/fireSpeed);
        audiosource = GetComponentInChildren<AudioSource>();  // 获取音效组件
    }

    void GenerateAntibody()
    {
        // 生成抗体
        if (targetObject == null)
            return;
        GameObject antibody = Instantiate(antibodyPrefab, firePos.position, Quaternion.identity);
        antibody.transform.LookAt(targetObject.transform.position);
        audiosource.Play();  // 播放音效
    }


    void Update()
    {
        if (targetObject != null)
        {
            /* 重新搜索逻辑 */
            // 目标死亡
            if (!targetObject.activeSelf)
            {
                targetObject = null;
                return;
            }
            // 目标进入细胞
            else if (targetObject.GetComponent<VirusController_Level3>().innerCell)
            {
                targetObject = null;
                return;
            }
            // 目标太远
            else if (Vector3.Distance(transform.position, targetObject.transform.position) > attackRange)
            {
                targetObject = null;
                return;
            }

            // 顺滑转向目标病毒
            Vector3 direction = targetObject.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f*Time.deltaTime);

        }

        if (targetObject == null)
        {
            SelectTarget(); // 寻找目标
        }

        // 上下浮动效果
        float offset = Mathf.Sin(Time.time * 5f + (startPos.x + startPos.y) * 100) * 0.2f;
        Vector3 newPos = startPos + new Vector3(0f, offset, 0f);
        transform.position = newPos;

        // 强化控制
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

    // 强化状态
    IEnumerator CrazyTime()
    {
        attackRange *= 2; // 增加射程
        StartCoroutine(ChangeColor(gameObject, new Color(1.0f,0.5f,0.5f,1.0f), 1)); // 变色
        CancelInvoke("GenerateAntibody");
        InvokeRepeating("GenerateAntibody", 0, 0.5f / fireSpeed); //提高射速

        yield return new WaitForSeconds(crazyTime);

        isRunning = false;
        attackRange /= 2; // 增加恢复
        CancelInvoke("GenerateAntibody");
        InvokeRepeating("GenerateAntibody", 0, 1f / fireSpeed); //恢复射速
        StartCoroutine(ChangeColor(gameObject, Color.white, 1)); // 变色

    }

    // 寻找目标
    private void SelectTarget()
    {
        GameObject[] viruses = GameObject.FindGameObjectsWithTag("virus"); // 找到所有病毒

        if (viruses.Length > 0)
        {
            GameObject closestVirus = null;
            float minDistance = Mathf.Infinity;

            foreach (GameObject virus in viruses)
            {
                float distance = Vector3.Distance(transform.position, virus.transform.position);

                if (virus.GetComponent<VirusController_Level3>().innerCell) //排除进入细胞的
                    continue;
                if (Vector3.Distance(transform.position, virus.transform.position) > attackRange) // 排除攻击范围之外的
                    continue;

                if (distance < minDistance) // 寻找符合要求的最近的病毒
                {
                    closestVirus = virus;
                    minDistance = distance;
                }
            }
            targetObject = closestVirus;
        }
    }

    // 颜色渐变
    IEnumerator ChangeColor(GameObject targetCell, Color targetColor, float duration)
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            Color currentColor = Color.Lerp(Color.white, targetColor, timeElapsed / duration);

            // 改变材质的颜色
            if(targetCell != null)
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
