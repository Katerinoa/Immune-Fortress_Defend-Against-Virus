using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellAffected : MonoBehaviour
{
    public bool hasInfected = false;    // T细胞寻找目标使用
    public int virusCount = 0;          // 侵染进入的细胞数量
    public AudioClip breakClip;         // 病毒侵染破裂
    public AudioClip bombClip;          // B细胞裂解

    private float SplitTime;             // 裂解时间
    private Coroutine infectedTimerCoroutine;
    private AudioSource audiosource;

    private void Awake()
    {
        SplitTime = Core_Level3.SplitTime;
    }

    private void Start()
    {
        audiosource = GameObject.Find("MainControl").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (virusCount != 0)
        {
            hasInfected = true;
            infectedTimerCoroutine = StartCoroutine(InfectedTimer(SplitTime));
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

        if (other.CompareTag("antibody"))
        {
            Destroy(other);
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
