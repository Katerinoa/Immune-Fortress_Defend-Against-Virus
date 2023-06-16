/*
*  体力机制——显示当前体力值与系统自动修复体力值,直到体力达到最大值
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MoneyControll : MonoBehaviour
{

    private Text moneytxt;      //金币数量显示
    private Text info;          //金币数量不足提示语
    void Start()
    {
        //体力值初始化
        moneytxt = this.gameObject.transform.Find("text").gameObject.GetComponent<Text>();
        info = this.gameObject.transform.Find("hint").gameObject.GetComponent<Text>();
        info.text = "";
        moneytxt.text = Core2.OriginMoney.ToString();
        InvokeRepeating("DefultMakeMoney",0.5f,2.0f);

    }

    void Update()
    {
        //实时监测当前体力值
        moneytxt.text = Core2.NowMoney.ToString();
        info.text = GridControll.moneyhint;
        if(Core2.NowMoney >= Core2.MaxMoney) Core2.NowMoney = Core2.MaxMoney;  //体力修复则停止
    }

    //系统自动慢慢恢复体力值
    void DefultMakeMoney()
    {
        Core2.NowMoney += 1;
    }
}
