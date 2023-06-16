/*
*  巨噬细胞攻击机制——寻找攻击范围内距离最近的敌人并发射子弹
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUI : MonoBehaviour
{
    public GameObject bulletprefab;   //子弹预制体
    public GameObject launchplace;   //发射点对象
    private GameObject bullet;       //子弹生成
    private GameObject enemy;        //距离最近的病毒
    public Vector3 launchvec;         //发射方向
    private Transform firepoint;      //发射点组件
    private EnemyControll nearenemy;  //寻找的最小病毒的脚本
    public static float mindis = 100;  //最小距离

    // Start is called before the first frame update
    void Start()
    {
        firepoint = launchplace.transform;
        //每秒寻找一次距离最近者
        InvokeRepeating("findmin", 0.5f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //找距离最近者并生成子弹
    EnemyControll findmin()
    {
        EnemyControll[] allenemies = FindObjectsOfType<EnemyControll>();
        if (allenemies.Length < 1) return null;
        nearenemy = mindistance(allenemies, firepoint);  //计算最近的敌人
        if (mindis <= 12.0f)  //监测到12m范围内有敌人即生成子弹
        {
            enemy = nearenemy.gameObject;
            launchvec = enemy.transform.position - firepoint.transform.position;
            transform.LookAt(enemy.transform, Vector3.up);
            bullet = Instantiate(bulletprefab, launchplace.transform.position, bulletprefab.transform.rotation, this.transform);
            bullet.name = "bullet";
        }
        return nearenemy;
    }

    //寻找所有病毒中最小距离
    EnemyControll mindistance(EnemyControll[] tararr, Transform ori)
    {
        if (tararr.Length <= 0) return null;
        mindis = Vector3.Distance(tararr[0].transform.position, ori.position);
        EnemyControll lasttar = tararr[0];
        for (int i = 0; i < tararr.Length; ++i)
        {
            float nextdis = Vector3.Distance(ori.position, tararr[i].transform.position);
            if (mindis > nextdis)
            {
                lasttar = tararr[i];
                mindis = nextdis;
            }
        }
        return lasttar;
    }
}

