/*
*  病毒生成机制——每波生成的时间间隔与每一波内病毒生成的时间间隔
*/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyCreator : MonoBehaviour
{

    public Material watermaterial;      //机体颜色
    public GameObject virusprefab;      //病毒生成的预制体
    private GameObject virus;          //生成的病毒对象
    public Text virusinfo;             //生成病毒提示语
    private int enemywave = 0;         //当前的病毒波次
    private bool flag1 = false;       //判断是否开始新的生成


    void Start()
    {
        //开始准备生成敌人
        virusinfo.text = "";
        Invoke("VirusInfo",Core2.WaveInterval / 2 - 1);  

        //机体初始颜色健康
        Color nowcolor = new Color(0,1,1);
        watermaterial.color = nowcolor;
    }

    void Update()
    {
        //判断当前生成数量是否达到预定值，达到则这一波生成结束停止生成
        if((Core2.CreateVirusNum == Core2.VirusCounts(enemywave)) && !flag1)
        {
            Debug.Log("第 " + enemywave + " 波敌人已被全部生成");
            //生成足够，停止
            StopCoroutine("creatvirus");
            flag1 = true;

            //当前这一波生成结束，生成下一波
            Invoke("VirusInfo",45.0f);
            enemywave++;
            Debug.Log("第 " + enemywave + " 波敌人即将来袭");
        }
    }

    //生成病毒
    IEnumerator creatvirus()
    {
        while(true)
        {
            yield return new WaitForSeconds(Core2.CreatVirusInterval(enemywave));
            virus = Instantiate(virusprefab,this.transform.position,this.transform.rotation,this.transform);
            virus.name = "virus";   //统一修改名称
            Core2.CreateVirusNum = Core2.CreateVirusNum + 1;
            Debug.Log("生成敌人的数量： " + Core2.CreateVirusNum);
        }
    }


    //开始生成下一波病毒，波总数在这里判断
    void nextWave()
    {
        Debug.Log("进来");
        if(enemywave < 3) StartCoroutine("creatvirus");
    }


    //生成敌人提示语
    void VirusInfo()
    {
        if(enemywave == 0) virusinfo.text = "First Wave Virus Is Coming.";
        else if(enemywave == 1) virusinfo.text = "Second Wave Virus Is Coming.";
        else if(enemywave == 2) virusinfo.text = "The Last Wave Virus Is Coming.";
        Invoke("CancelInfo",2.0f);  
        Invoke("nextWave",2.0f); 
    }

    //关闭提示文字
    void CancelInfo()
    {
        virusinfo.text = "";
    }
}
