using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUI : MonoBehaviour
{
    public GameObject bulletprefab;
    public GameObject launchplace;
    GameObject bullet;
    GameObject enemy;

    int  flag = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("virus");
        if(enemy)    StartCoroutine("shoot");
    }

    // Update is called once per frame
    void Update()
    {
        //开始和停止射击机制
        if(enemy == null)
        {
            if(flag == 0) StopCoroutine("shoot");
            enemy = GameObject.Find("virus");
            if(enemy == null) flag = -1;
            else flag = 1;
        }
        else if(flag == 1)
        {
            // StartCoroutine("shoot");
            // flag = 0;
            if(BulletControll.mindis <= 12.0f)
            { 
                StartCoroutine("shoot");
                flag = 0;
            }
        }
        //不能，因为还要进去才能判断距离，否则midis不更新，死锁
        // else if(flag == 0 && BulletControll.mindis > 12.0f)
        // {
        //     StopCoroutine("shoot");
        //     flag = 1;
        // }
    }

    //生成子弹
    IEnumerator shoot()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            bullet = Instantiate(bulletprefab, launchplace.transform.position, bulletprefab.transform.rotation, this.transform);
            bullet.name = "bullet";
        //    Debug.Log(bullet.transform.position);
        //  Destroy(bullet,5.0f);        //在子弹预制体里面实现
        }

    }
}
