using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    private static int preventMaxNum = 10; //绒毛最大阻挡数量

    private static float speed = 2.0f;  // 病毒移动速度

    private static int maxGenerateNum = 100;  // 病毒最大生成数量


    //第2关相关变量
    private static float creatvirustime = 6.0f;  //间隔生成病毒的时间

    private static float creatbulletstime = 1.0f;  //生成子弹间隔

    public static int PreventMaxNum
    {
        get { return preventMaxNum; }
        set { preventMaxNum = value; }
    }

    public static float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public static int MaxGenerateNum
    {
        get { return maxGenerateNum; }
        set { maxGenerateNum = value; }
    }

    public static float CreatVirusTime
    {
        get {return creatvirustime;}
        set {creatvirustime = value;}
    }

    public static float CreatBulletTime
    {
        get {return creatbulletstime;}
        set {creatbulletstime = value;}
    }
}
