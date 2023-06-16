/**
 * �Ľű����ڿ�������ϸ��
 */
using UnityEngine;

public class macrophageController : MonoBehaviour
{
    public float maxSize = 2f;      // ���͵������

    private Vector3 startPos;       // ��ʼλ��
    private Vector3 startScale;     // ��ʼ����
    private float currentSize = 1f; // ��ǰ����
    private float targetSize = 1f;  // Ŀ������


    private void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale; // ��ȡ��ʼ����
    }

    private void Update()
    {
        // ����Ч��
        float offset = Mathf.Sin(Time.time + (startPos.x + startPos.y) * 100) * 0.2f;
        Vector3 newPos = startPos + new Vector3(0f, offset, 0f);
        transform.position = newPos;

        // ���ͽ���
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
        // ���ɲ��� ��������
        if (other.CompareTag("virus"))
        {
            other.gameObject.SetActive(false);
            targetSize = maxSize < targetSize * 1.2f ? maxSize : targetSize * 1.1f ; // Ŀ����������
        }
    }
}

