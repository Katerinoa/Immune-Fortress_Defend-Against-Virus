/**
 * �ýű����ڿ��ƿ���
 */
using UnityEngine;

public class AntibodyController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 20f;           // ���Ʒ����ٶȵı���
    public float rotateSpeed = 500f;    // ������ת�ٶȵı���

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // ��ȡ�������
    }

    void Update()
    {
        // ��ǰ�ƶ�
        Vector3 velocity = transform.forward * speed;
        rb.velocity = velocity;
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);// �����κ����嶼����

        if (collision.gameObject.CompareTag("virus"))
        {
            collision.gameObject.SetActive(false); // ��ɱ����
        }
    }
}
