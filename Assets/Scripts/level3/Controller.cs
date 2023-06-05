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


    private void Start()
    {
        currentHealth = maxHealth;
        objectRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // ��鸽���Ƿ��ез�����
        if (GameObject.Find("GameObject").GetComponent<game>().if_start == 1)
        {

        
            Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        bool foundEnemy = false;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                foundEnemy = true;
                break;
            }
        }

        if (foundEnemy)
        {
            if (!isAttacking && Time.time - lastAttackTime > attackInterval)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            MoveForward();
        }
        }
    }

    private void Attack()
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
        // ��X�᷽��ǰ��
        transform.Translate(new Vector3(1,0,0) * Time.deltaTime,Space.World);
    }
}