using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellAffected : MonoBehaviour
{
    public bool hasInfected = false;
    public int virusCount = 0;                  // ��Ⱦ��Ŀ
    public float infectedTime = 0;              // ��Ⱦ��ʼʱ��
    private float maxInfectedTime = 5f;         // �����Ⱦʱ��
    //private int maxVirus = 3;                   // �����Ⱦ��Ŀ
    private Coroutine infectedTimerCoroutine;
    private AudioSource audiosource;

    public AudioClip breakClip;
    public AudioClip bombClip;

    private void Start()
    {
        audiosource = GameObject.Find("MainControl").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (virusCount != 0)
        {
            hasInfected = true;
            infectedTimerCoroutine = StartCoroutine(InfectedTimer(maxInfectedTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EffectorTCell") && hasInfected)
        {
            audiosource.clip = bombClip;
            audiosource.Play();
            if (infectedTimerCoroutine != null)
            {
                StopCoroutine(infectedTimerCoroutine); // 终止携程
                infectedTimerCoroutine = null;
            }

            gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }

    IEnumerator InfectedTimer(float maxInfectedTime)
    {
        float infectedTime = 0f;
        while (infectedTime <= maxInfectedTime)
        {
            infectedTime += Time.deltaTime;
            yield return null;
        }

        audiosource.clip = breakClip;
        audiosource.Play();
        SpawnMonster();
        gameObject.SetActive(false);
    }
    private void SpawnMonster()
    {
        for (int i = 0; i < virusCount*2; ++i)
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

}
