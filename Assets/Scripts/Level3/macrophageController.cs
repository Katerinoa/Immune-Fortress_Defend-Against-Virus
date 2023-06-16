/**
 * 改脚本用于控制吞噬细胞
 */
using UnityEngine;

public class macrophageController : MonoBehaviour
{
    public float maxSize = 2f;      // 体型的最大倍数

    private Vector3 startPos;       // 初始位置
    private Vector3 startScale;     // 初始体型
    private float currentSize = 1f; // 当前体型
    private float targetSize = 1f;  // 目标体型


    private void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale; // 获取初始体型
    }

    private void Update()
    {
        // 浮动效果
        float offset = Mathf.Sin(Time.time + (startPos.x + startPos.y) * 100) * 0.2f;
        Vector3 newPos = startPos + new Vector3(0f, offset, 0f);
        transform.position = newPos;

        // 体型渐变
        if (currentSize < targetSize)
        {
            float newSize = Mathf.Lerp(currentSize, targetSize, 2f * Time.deltaTime);
            Vector3 newScale = startScale * newSize;
            transform.localScale = newScale;

            currentSize = newSize;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 吞噬病毒 增大体型
        if (other.CompareTag("virus"))
        {
            other.gameObject.SetActive(false);
            targetSize = maxSize < targetSize * 1.2f ? maxSize : targetSize * 1.1f ; // 目标体型增大
        }
    }
}

