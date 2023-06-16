/*
*  消炎因子机制——恢复体力
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiFactorControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //定时反复调用
        InvokeRepeating("MakeMoney",0.5f,2.0f);
    }

    //修复体力值
    void MakeMoney()
    {
        Core2.NowMoney += Core2.CellWage;
    }
}
