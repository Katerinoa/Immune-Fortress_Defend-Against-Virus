using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MoneyControll : MonoBehaviour
{

    Text moneytxt;
    Text info;
    // Start is called before the first frame update
    void Start()
    {
        moneytxt = this.gameObject.transform.Find("text").gameObject.GetComponent<Text>();
        info = this.gameObject.transform.Find("hint").gameObject.GetComponent<Text>();

        //初始化
        info.text = "";
        moneytxt.text = Core2.OriginMoney.ToString();
        //自动加点钱
        InvokeRepeating("DefultMakeMoney",0.5f,1.0f);
    //    Debug.Log("金额： " + moneytxt.text);
    }

    // Update is called once per frame
    void Update()
    {
        moneytxt.text = Core2.NowMoney.ToString();
        info.text = GridControll.moneyhint;
    }


    void DefultMakeMoney()
    {
        Core2.NowMoney += 1;
    }
}
