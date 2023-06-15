using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core2 : MonoBehaviour
{
        //第2关相关变量
    private static float[] creatvirusinterval = {4.0f, 3.0f,2.0f};  //每波当中生成病毒的时间

    private static float creatbulletsinterval = 1.0f;  //生成子弹间隔

    private static int[] viruscounts = {10,25,45};  //累计病毒数量之和

    private static int createvirusnum = 0;   //当前生成的敌人数量
    
    private static int destroyvirusnum = 0;  //当前消灭的敌人数量

    private static int maxvirusnum = viruscounts[2];   //最大生成的敌人数量

    private static int waveinterval = 10;

    private static int damagevalue = 5;  //一颗子弹的伤害值


    //金币相关
    private static int originmoney = 40;     //初始额度
    
    private static int cellprice = 30;       //消炎因子的价格

    private static int macrophageprice = 50;     //巨噬细胞的价格

    private static int cellwage = 3;     //消炎因子的赚钱能力(每两秒)

    private static int nowmoney = originmoney; //现在的额度


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

    public static int OriginMoney
    {
        get { return originmoney; }
        set { originmoney = value; }
    }

    public static int CellPrice
    {
        get {return cellprice; }
        set { cellprice = value; } 
    }

    public static int MacrophagePrice
    {
        get { return macrophageprice; }
        set { macrophageprice = value; }
    }

    public static int CellWage
    {
        get { return cellwage; }
        set { cellwage = value; }
    }

    public static int NowMoney
    {
        get { return nowmoney; }
        set { nowmoney = value; }
    }

     public static int WaveInterval
    {
        get { return waveinterval; }
        set { waveinterval = value; }
    }
}
