using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public bool isInfected = false; // 侵染标志 用于控制计时器
    public ParticleSystem virusEffect; // 粒子特效

    private float timer = 0.0f; // 计时器
    private float infectTime = 4.0f; // 侵染时间

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("virus"))
        {
            other.gameObject.GetComponent<VirusController_Level3>().isStoped = true;
            other.GetComponentInChildren<VirusBehaviour>().isStopped = true;
            virusEffect.Play();
            isInfected = true;
            timer = 0.0f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("virus"))
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
                    other.gameObject.SetActive(false);
                }
                else if (!gameObject.activeSelf)
                {
                    virusEffect.Stop();
                    isInfected = false;
                }
            }
        }
    }
    private void SpawnMonster()
    {
        GameObject virus = ObjectPool.SharedInstance.GetPooledObject();
        if (virus != null)
        {
            virus.transform.position = transform.position;
            virus.GetComponent<VirusController_Level3>().baseHeight = transform.position.y;
            virus.SetActive(true);
        }
        Counter.generateCount++;
    }
}
