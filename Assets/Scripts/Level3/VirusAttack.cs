using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VirusAttack : MonoBehaviour
{
    public bool isInfected = false; // 侵染标志 用于控制计时器
    public ParticleSystem virusEffect; // 粒子特效

    private float timer = 0.0f; // 计时器
    public float infectTime = 5.0f; // 侵染时间

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cell"))
        {
            gameObject.GetComponent<VirusController_Level3>().isStoped = true;
            GetComponentInChildren<VirusBehaviour>().isStopped = true;
            virusEffect.Play();
            isInfected = true;
            timer = 0.0f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("cell"))
        {
            if (isInfected)
            {
                timer += Time.deltaTime;

                if (timer >= infectTime)
                {
                    isInfected = false;
                    gameObject.GetComponent<VirusController_Level3>().isStoped = false;
                    GetComponentInChildren<VirusBehaviour>().isStopped = false;
                    other.gameObject.GetComponent<CellAffected>().virusCount++;
                    gameObject.GetComponent<VirusAttack>().virusEffect.Stop();
                    //gameObject.SetActive(false);
                    //other.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cell"))
        {
            Debug.Log(1);
            gameObject.GetComponent<VirusController_Level3>().isStoped = true;
            if (!isInfected)
            {
                timer = 0.0f;
                virusEffect.Play();
                isInfected = true;
          
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("cell"))
        {
            if (isInfected)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);

                if (timer >= infectTime)
                {
                    isInfected = false;
                    virusEffect.Stop();
                   /* SpawnMonster();
                    SpawnMonster();
                    SpawnMonster();
                    gameObject.SetActive(false);
                    collision.gameObject.SetActive(false);*/
                }
                /*else if (!gameObject.activeSelf)
                {
                    virusEffect.Stop();
                    isInfected = false;
                }*/
            }
        }
    }
}
