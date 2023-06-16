/**
 * �Ľű����ڿ����ܰ����ӳ���ЧӦBϸ���ƶ�
 */
using System.Linq;
using UnityEngine;

public class LymphokineController : MonoBehaviour
{
    private string Tag = "BCell";           // ЧӦBϸ���ı�ǩ
    public GameObject TargetBCell;          // �ܰ����ӵ�Ŀ��Bϸ��
    public float moveSpeed = 10f;           // �ܰ����ӵ��ƶ��ٶ�
    private Rigidbody rb;
    public int maxCollision = 2;            // �����ײ����
    public float timer = 10f;               // �����ʱ��


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // ѡ��Ŀ��ЧӦBϸ��
        if (TargetBCell == null)
        {
            SelectTarget();
        }
        else
        {
            // ����ǰ������
            Vector3 targetDirection = TargetBCell.transform.position - transform.position;
            // δ�ƶ����յ�
            if (targetDirection != Vector3.zero)
            {
                Vector3 direction = (TargetBCell.transform.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;    // �����յ���ٶ�
            }
        }
        // ����ʱ��̫�����ܰ�������ʧ
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        // �������ʹ�ܰ������ܹ������ƶ�
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        rb.AddForce(randomDirection);
        maxCollision--;
        // ��ײ��������
        if (maxCollision == 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        // ����ЧӦBϸ��������ʱ����ЧӦBϸ������crazy״̬
        if (collision.gameObject == TargetBCell)
        {
            TargetBCell.GetComponent<EffectorBCellController>().crazy = true;
            Destroy(gameObject);
        }
    }

    // Ѱ��Ŀ��ЧӦBϸ��
    private void SelectTarget()
    {
        // �ҵ��ڳ������Ҵ���ЧӦBϸ��
        GameObject[] Bcells = GameObject.FindGameObjectsWithTag(Tag).Where(obj => obj.activeSelf).ToArray();

        // ���ѡ��һ��ЧӦBϸ����ΪĿ��
        if (Bcells.Length > 0)
        {
            TargetBCell = Bcells[UnityEngine.Random.Range(0, Bcells.Length)];
        }
    }
}
