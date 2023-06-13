using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiFactorControll : MonoBehaviour
{
    // Start is called before the first frame update
    //无情的赚钱机器罢了
    void Start()
    {
        InvokeRepeating("MakeMoney",0.5f,2.0f);
    }

    void MakeMoney()
    {
        Core2.NowMoney += 5;
    }
}
