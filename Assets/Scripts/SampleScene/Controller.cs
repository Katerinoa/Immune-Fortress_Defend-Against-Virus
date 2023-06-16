using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float attackRange = 5f;          // ������Χ
    public int maxHealth = 100;             // ���Ѫ��
    public int attackDamage = 10;           // �����˺�
    public float attackInterval = 1f;       // �������
    public float fadeDuration = 2f;         // ͸���Ƚ���ʱ��

    private int currentHealth;              // ��ǰѪ��
    private bool isAttacking;               // �Ƿ����ڹ���
    private float lastAttackTime;           // �ϴι�����ʱ��
    private Renderer objectRenderer;        // �������Ⱦ��

    public GameObject bulletPrefab;  // �ӵ���Ԥ����

    public float moveSpeed = 0.05f;
    public float rotationSpeed = 100f;

    private Vector3 targetPosition;


    private void Start()
    {
        currentHealth = maxHealth;
        objectRenderer = GetComponent<Renderer>();
        // ��ʼʱ���ѡ��һ��Ŀ��λ��
        targetPosition = GetRandomPosition();
    }

    private void Update()
    {
        // ��鸽���Ƿ��ез�����
        if (GameObject.Find("level3start")==null)
        {

        
         Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        bool foundEnemy = false;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                foundEnemy = true;
                    Vector3 direction = collider.gameObject.transform.position - transform.position;
                    direction.y = 0f; // 可选：将Y轴置为0，使物体只在水平面上转动

                    if (direction != Vector3.zero)
                    {
                        // 计算目标旋转角度
                        Quaternion targetRotation = Quaternion.LookRotation(direction);

                        // 平滑地转向目标角度
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    }
                    break;
            }
        }
        if (foundEnemy)
        {
               
                if (!isAttacking && Time.time - lastAttackTime > attackInterval)
            {
                Attack();  //������Ϊ
                lastAttackTime = Time.time;
            }
        }
        else
        {
            MoveForward();
        }
        }
    }

    private void Attack() //����
    {
        // ��⹥����Χ�ڵĵ���
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {

                
                // ʵ�����ӵ�������λ�ú���ת
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

                // ���ӵ�����з���ɫ
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                bullet.transform.forward = direction;

                // ���ӵ�һ����ʼ�ٶȣ����Ը�����Ҫ����
                float bulletSpeed = 10f;
                bullet.GetComponentInChildren<Rigidbody>().velocity = direction * bulletSpeed;
                collider.gameObject.GetComponentInChildren<viruscontroller>().TakeDamage(attackDamage);

                // �����ӵ�
                Destroy(bullet, 0.8f);  // 2��������ӵ�
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("zidan"))
        {
            Destroy(collision.gameObject);
        }
        targetPosition = GetRandomPosition();
    }

    public void TakeDamage(int damage)   //�ܵ����˺�
    {
        Debug.Log("������2");
        if (currentHealth > 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {

        // ����͸���Ƚ���Э��
        StartCoroutine(FadeAndDestroy());
    }

    IEnumerator FadeAndDestroy()
    {
        // �𽥽�͸���ȼ�����0
        float elapsedTime = 0f;
        float startAlpha = objectRenderer.material.color.a;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float fadeAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            Color fadeColor = objectRenderer.material.color;
            fadeColor.a = fadeAlpha;
            objectRenderer.material.color = fadeColor;
            yield return null;
        }

        // ��������
        Destroy(gameObject);
    }

    private void MoveForward()
    {
        Debug.Log("hello");
        
        // ���㵱ǰλ�õ�Ŀ��λ�õķ�������
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        // ����Ŀ����ת�Ƕ�
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // ƽ������ת���峯��Ŀ�귽��
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
        if (angleDifference <= 1f)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        // �ƶ����峯��Ŀ��λ��
        
        Debug.Log(Vector3.Distance(transform.position, targetPosition));
        Debug.Log(transform.position);
        Debug.Log(targetPosition);
        // �ж��Ƿ񵽴�Ŀ��λ�ã�������ѡ���µ�Ŀ��λ��
        if (Vector3.Distance(transform.position, targetPosition) < 0.8f)
        {
            new WaitForSeconds(1000f);
            targetPosition = GetRandomPosition();
        }
    }

    private Vector3 GetRandomPosition()
    {
        // ���������Ŀ��λ��
        float x = Random.Range(-5f, 5f);
        float z = Random.Range(-5f, 5f);
        Debug.Log("x="+x);
        Debug.Log("z" + z);
        Vector3 randomPosition = new Vector3(x+transform.position.x, transform.position.y, z+transform.position.z);

        return randomPosition;
    }
    
}
