/*
*  子弹机制——向病毒方向发射并自我销毁
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    public float speed = 5.0f;    //发射速度
    private Vector3 launchvec;    //发射方向
    private GameObject parent;      //父物体

    void Start()
    {
        // 找父物体，设置子弹方向，解除父子物体绑定
        parent = gameObject.transform.parent.gameObject;
        this.transform.parent = null;
        launchvec = parent.GetComponent<AttackUI>().launchvec;
        Destroy(gameObject, 3.0f);           //不论是否击中，3s后自我销毁，节约资源
    }


    void Update()
    {
        //子弹运动
        this.transform.position += launchvec * speed * Time.deltaTime;
    }
    
    //触发器判断，若子弹射中病毒则自我销毁
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "virus")
        {
            Destroy(this.gameObject);
        }
    }
}
