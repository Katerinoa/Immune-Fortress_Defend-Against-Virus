using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUI : MonoBehaviour
{
    public GameObject bulletprefab;
    public GameObject launchplace;
    GameObject bullet;
    GameObject enemy;

    public Vector3 launchvec;

    //int flag = 0;




    Transform firepoint;

    EnemyControll nearenemy;
    public static float mindis = 100;

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent<AttackUI>().enabled = false;
        // enemy = GameObject.Find("virus");
        // if (enemy) StartCoroutine("shoot");
        firepoint = launchplace.transform;
        InvokeRepeating("findmin",0.5f,1.0f);
    }

    // Update is called once per frame
    void Update()
    {

        //开始和停止射击机制
        // if (enemy == null)
        // {
        //     if (flag == 0) StopCoroutine("shoot");
        //     enemy = GameObject.Find("virus");
        //     if (enemy == null) flag = -1;
        //     else flag = 1;
        // }
        // else if (flag == 1)
        // {
        //     // StartCoroutine("shoot");
        //     // flag = 0;
        //     if (BulletControll.mindis <= 12.0f)
        //     {
        //         StartCoroutine("shoot");
        //         flag = 0;
        //     }
        // }

        //不能，因为还要进去才能判断距离，否则midis不更新，死锁
        // else if(flag == 0 && BulletControll.mindis > 12.0f)
        // {
        //     StopCoroutine("shoot");
        //     flag = 1;
        // }
        //生成子弹
        // if(mindis <= 12.0f)
        // {
        //     mindis = 100.0f;
        //     enemy = nearenemy.gameObject;
        //     launchvec = enemy.transform.position - firepoint.transform.position;
        //     transform.LookAt(enemy.transform,Vector3.up);
        //     bullet = Instantiate(bulletprefab, launchplace.transform.position, bulletprefab.transform.rotation, this.transform);
        //     bullet.name = "bullet";
        // }
    }
    // IEnumerator shoot()
    // {

    //     while (true)
    //     {
    //         yield return new WaitForSeconds(Core2.CreatBulletInterval);
    //         bullet = Instantiate(bulletprefab, launchplace.transform.position, bulletprefab.transform.rotation, this.transform);
    //         bullet.name = "bullet";
    //         //    Debug.Log(bullet.transform.position);
    //         //  Destroy(bullet,5.0f);        //在子弹预制体里面实现
    //     }

    // }

    //找距离最近者
    EnemyControll findmin()
    {
        EnemyControll[] allenemies = FindObjectsOfType<EnemyControll>();
        if (allenemies.Length < 1) return null;
        nearenemy = mindistance(allenemies, firepoint);
        //    nearenemy.gameObject.GetComponent<Renderer>().material.color = Color.red;
        //应该放在这里发射
        if(mindis <= 12.0f)
        {
            enemy = nearenemy.gameObject;
            launchvec = enemy.transform.position - firepoint.transform.position;
            transform.LookAt(enemy.transform, Vector3.up);
            bullet = Instantiate(bulletprefab, launchplace.transform.position, bulletprefab.transform.rotation, this.transform);
            bullet.name = "bullet";
        }
        return nearenemy;
    }


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
        //    Debug.Log("最小距离：" +mindis);
        return lasttar;
    }
}
