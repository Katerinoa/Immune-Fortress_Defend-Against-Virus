using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    public static GameObject enemy;
    public float speed = 5.0f;
    Vector3 launchvec;
    Transform firepoint;
    GameObject parent;

    EnemyControll nearenemy;
    public static float mindis = 100;
    // Start is called before the first frame update
    void Start()
    {
        //把父物体的转向安置在子弹
        parent = gameObject.transform.parent.gameObject;
        //    Debug.Log("子弹的父物体是： " + parent.name);


        //不能这样找开火点
        // firepoint = GameObject.Find("launch");
        firepoint = parent.transform.Find("launch");
        //Debug.Log("firepoint: " + firepoint.position);
        

        // if (findmin() == null)
        // {
        //     Destroy(this.gameObject);
        //     return;
        // }
        // else enemy = nearenemy.gameObject;
        // //如果有敌人并且攻击距离在半径12米以内
        // if (enemy != null && mindis <= 12.0f)
        // {
        //     parent.transform.LookAt(enemy.transform, Vector3.up);
        //     launchvec = enemy.transform.position - firepoint.transform.position;
        //     launchvec.y = 0;
        // }
        // else if (mindis > 12.0f || enemy == null)
        // {
        //     Destroy(this.gameObject);
        // }
        // Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log(enemy.transform.position);
        this.transform.position += parent.GetComponent<AttackUI>().launchvec * speed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "virus")
        {
            //Debug.Log("碰上啦");
            Destroy(this.gameObject);


        }
    }

    //找距离最近者
    EnemyControll findmin()
    {
        EnemyControll[] allenemies = FindObjectsOfType<EnemyControll>();
        if (allenemies.Length < 1) return null;
        nearenemy = mindistance(allenemies, firepoint);
        //    nearenemy.gameObject.GetComponent<Renderer>().material.color = Color.red;
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
