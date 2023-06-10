using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core2 : MonoBehaviour
{
        //第2关相关变量
    private static float[] creatvirusinterval = {6.0f, 5.0f,4.0f};  //每波间隔生成病毒的时间

    private static float creatbulletsinterval = 1.0f;  //生成子弹间隔

    private static int[] viruscounts = {5,10,15};  //累计病毒数量之和

    private static int createvirusnum = 0;   //当前生成的敌人数量
    
    private static int destroyvirusnum = 0;  //当前消灭的敌人数量

    private static int maxvirusnum = viruscounts[2];   //最大生成的敌人数量

    private static int damagevalue = 5;  //一颗子弹的伤害值



    public static float CreatVirusInterval(int i)
    {
        if(i >= 0 && i < 3) return creatvirusinterval[i];
        else return -1;
    }

    public static float VirusCounts(int i)
    {
        if(i >= 0 && i < 3) return viruscounts[i];
        else return -1;
    }

    public static float CreatBulletInterval
    {
        get {return creatbulletsinterval;}
        set {creatbulletsinterval = value;}
    }


    public static int CreateVirusNum
    {
        get {return createvirusnum;}
        set {createvirusnum = value;}
    }

    public static int DestroyVirusNum
    {
        get {return destroyvirusnum;}
        set {destroyvirusnum = value;}
    }

    public static int MaxVirusNum
    {
        get {  return maxvirusnum;}
        set {  maxvirusnum = value; }
    }

    public static int DamageValue
    {
        get { return damagevalue; }
        set { damagevalue = value; }
    }
}
