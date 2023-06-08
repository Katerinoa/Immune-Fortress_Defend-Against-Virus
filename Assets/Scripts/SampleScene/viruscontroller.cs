using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class viruscontroller : MonoBehaviour
{
    public float attackRange = 6f;          // 攻击范围
    public int maxHealth = 100;             // 最大血量
    public int attackDamage = 10;           // 攻击伤害
    public float attackInterval = 1f;       // 攻击间隔
    public float fadeDuration = 2f;         // 透明度渐变时间

    private int currentHealth;              // 当前血量
    private bool isAttacking;               // 是否正在攻击
    private float lastAttackTime;           // 上次攻击的时间
    private Renderer objectRenderer;        // 物体的渲染器

    public GameObject bulletPrefab;  // 子弹的预制体

    private void Start()
    {
        currentHealth = maxHealth;
        objectRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // 检查附近是否有敌方物体
        if (GameObject.Find("level3start") ==null)
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
            bool foundEnemy = false;

            foreach (Collider collider in colliders)
            {
              
                if (collider.CompareTag("cell"))
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
        // 攻击逻辑

        // 检测攻击范围内的敌人
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("cell"))
            {
                // 实例化子弹并设置位置和旋转
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

                // 让子弹朝向敌方角色
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                bullet.transform.forward = direction;

                // 给子弹一个初始速度，可以根据需要调整
                float bulletSpeed = 10f;
                bullet.GetComponentInChildren<Rigidbody>().velocity = direction * bulletSpeed;
                collider.gameObject.GetComponentInChildren<Controller>().TakeDamage(attackDamage);

                // 销毁子弹
                Destroy(bullet, 0.8f);  // 2秒后销毁子弹
            }
        }
    }

    public void TakeDamage(int damage)   //受到的伤害
    {

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

        // 启动透明度渐变协程
        StartCoroutine(FadeAndDestroy());
    }

    IEnumerator FadeAndDestroy()
    {
        // 逐渐将透明度减少至0
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

        // 销毁物体
        Destroy(gameObject);
    }

    private void MoveForward()
    {
        // 在X轴方向前进
        transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
