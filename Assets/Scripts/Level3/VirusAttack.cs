using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VirusAttack : MonoBehaviour
{
    public ParticleSystem virusEffect; // 粒子系统

    private float timer = 0.0f; // 计时器
    private float infectTime; // 侵染时间
    private bool isInfecting = false; // 是否正在侵染

    private void Awake()
    {
        infectTime = Core_Level3.infectTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cell"))
        {
            GetComponent<VirusController_Level3>().isStopped = true;
            GetComponentInChildren<VirusBehaviour>().isStopped = true;
            virusEffect.Play();
            isInfecting = true;
            timer = 0.0f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("cell"))
        {
            if (isInfecting)
            {
                timer += Time.deltaTime;

                if (timer >= infectTime)  // 到时间后病毒进入细胞
                {
                    isInfecting = false;
                    GetComponent<VirusController_Level3>().isStopped = false;
                    GetComponent<VirusController_Level3>().innerCell = true;
                    GetComponentInChildren<VirusBehaviour>().isStopped = false;
                    other.gameObject.GetComponent<CellAffected>().virusCount++;
                    gameObject.GetComponent<VirusAttack>().virusEffect.Stop();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cell"))
        {
            gameObject.GetComponent<VirusController_Level3>().isStopped = true;
            if (!isInfecting)
            {
                timer = 0.0f;
                virusEffect.Play();
                isInfecting = true;

            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("cell"))
        {
            if (isInfecting)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);

                if (timer >= infectTime)
                {
                    isInfecting = false;
                    virusEffect.Stop();

                }

            }
        }
    }
}
