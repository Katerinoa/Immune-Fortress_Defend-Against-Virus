using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public bool isInfected = false; // ��Ⱦ��־ ���ڿ��Ƽ�ʱ��
    public ParticleSystem virusEffect; // ������Ч

    private float timer = 0.0f; // ��ʱ��
    private float infectTime = 2.0f; // ��Ⱦʱ��

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
