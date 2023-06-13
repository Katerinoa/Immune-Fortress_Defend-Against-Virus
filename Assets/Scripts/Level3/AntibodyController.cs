using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntibodyController : MonoBehaviour
{
    Rigidbody rb;  // �������
    public float speed = 20f;  // ���Ʒ����ٶȵı���
    public float rotateSpeed = 500f;   // ������ת�ٶȵı���

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // ��ȡ�������
    }

    void Update()
    {
        // ��ȡ�����ǰ���򲢸�ֵ��velocity����
        Vector3 velocity = transform.forward * speed;
        rb.velocity = velocity;  // �޸ĸ����ٶȴﵽ���ٷ��е�Ч��
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("virus"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
