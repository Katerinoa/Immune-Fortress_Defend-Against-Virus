using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyCreator : MonoBehaviour
{
    public GameObject virusprefab;
    GameObject virus;
    public Text virusinfo;
    
    //加上生成敌人间隔时间的机制
    int enemywave = 0;

    bool flag1 = false,flag2 = false;


    // Start is called before the first frame update
    //自动生成敌人
    void Start()
    {
        virusinfo.text = "";
        Debug.Log(Core2.VirusCounts(enemywave) + " , " + Core2.VirusCounts(enemywave + 1) + " , " + Core2.VirusCounts(enemywave + 2));
        //StartCoroutine("creatvirus");
        Invoke("VirusInfo",Core2.WaveInterval / 2 - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if((Core2.CreateVirusNum == Core2.VirusCounts(enemywave)) && !flag1)
        {
            Debug.Log("第 " + enemywave + " 波敌人已被全部生成");
            //生成足够，停止
            StopCoroutine("creatvirus");
            flag1 = true;
        }
       
        
        if((Core2.DestroyVirusNum == Core2.VirusCounts(enemywave)) && !flag2)
        {
            Debug.Log("第 " + enemywave + " 波敌人已被全部消灭");
            Invoke("VirusInfo",5.0f);
            //当前这一波消灭了，就要进入下一波
            enemywave++;

            //10秒后进入下一波
            Debug.Log("第 " + enemywave + " 波敌人即将来袭");
            flag2 = true;
        } 
    }

    //生成
    IEnumerator creatvirus()
    {
        while(true)
        {
            yield return new WaitForSeconds(Core2.CreatVirusInterval(enemywave));
            virus = Instantiate(virusprefab,this.transform.position,this.transform.rotation,this.transform);
            //改名
            virus.name = "virus";
            Core2.CreateVirusNum = Core2.CreateVirusNum + 1;
            Debug.Log("生成敌人的数量： " + Core2.CreateVirusNum);
            flag1 = false;
            flag2 = false;
        }
    }


    //进入下一波，波总数在这里判断
    void nextWave()
    {
        Debug.Log("进来");
        if(enemywave < 3) StartCoroutine("creatvirus");
    }

    void VirusInfo()
    {
        if(enemywave == 0) virusinfo.text = "First Wave Virus Is Coming.";
        else if(enemywave == 1) virusinfo.text = "Second Wave Virus Is Coming.";
        else if(enemywave == 2) virusinfo.text = "The Last Wave Virus Is Coming.";
        Invoke("CancelInfo",2.0f);
        Invoke("nextWave",2.0f);
    }

    void CancelInfo()
    {
        virusinfo.text = "";
    }
}
