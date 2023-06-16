/**
 * 该脚本用于控制细胞行为
 */
using System.Collections;
using UnityEngine;

public class CellAffected : MonoBehaviour
{
    public bool hasInfected = false;             // T细胞寻找目标使用
    public int virusCount = 0;                   // 侵染进入的细胞数量
    public AudioClip breakClip;                  // 病毒侵染破裂音效
    public AudioClip bombClip;                   // B细胞裂解音效

    private float SplitTime;                     // 裂解时间
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
            // 被病毒入侵
            hasInfected = true; 
            infectedTimerCoroutine = StartCoroutine(InfectedTimer(SplitTime)); //开启一个协程 细胞坏死时间
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 如果被入侵 则可以被效应T细胞裂解
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
            other.gameObject.GetComponent<EffectorTCellController>().Sleep(); // 效应T细胞进入冷却
        }

        // 避免抗体穿过细胞
        if (other.CompareTag("antibody"))
        {
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

        // 细胞坏死
        audiosource.clip = breakClip;
        audiosource.Play();
        SpawnMonster(); // 病毒繁殖
        gameObject.SetActive(false);
    }

    // 入侵的病毒繁殖新的病毒
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
        }
    }

}
