using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public bool isInfected = false; // 侵染标志 用于控制计时器
    public ParticleSystem virusEffect; // 粒子特效

    private float timer = 0.0f; // 计时器
    private float infectTime = 2.0f; // 侵染时间

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("virus"))
        {
            virusEffect.Play();
            isInfected = true;    
            timer = 0.0f;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (isInfected)
        {
            timer += Time.deltaTime;

            if (timer >= infectTime)
            {
                isInfected = false;
                virusEffect.Stop();
                SpawnMonster();
                SpawnMonster();
                SpawnMonster();
                gameObject.SetActive(false);
                collision.gameObject.SetActive(false);
            }
            else if (!gameObject.activeSelf)
            {
                virusEffect.Stop();
                isInfected = false;
            }
        }
    }

    private void SpawnMonster()
    {
        GameObject virus = ObjectPool.SharedInstance.GetPooledObject();
        if (virus != null)
        {
            virus.transform.position = transform.position;
            virus.transform.rotation = transform.rotation;
            virus.SetActive(true);
        }
        Counter.generateCount++;
    }
}
