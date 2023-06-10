using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public bool isInfected = false; // ��Ⱦ��־ ���ڿ��Ƽ�ʱ��
    public ParticleSystem virusEffect; // ������Ч

    private float timer = 0.0f; // ��ʱ��
    private float infectTime = 4.0f; // ��Ⱦʱ��

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
