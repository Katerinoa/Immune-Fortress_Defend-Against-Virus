using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    private static int preventMaxNum = 10; //绒毛最多阻挡数量

    private static Vector2 spawnIntervalRange = new Vector2(0.5f, 1.5f);  //生成时间间隔

    private static float speed = 10.0f;  // 病毒移动速度

    private static int maxGenerateNum = 800;  // 最大生成病毒数量

    // 提供public方法，用于访问上述三个变量并允许外部修改
    public static int PreventMaxNum
    {
        get { return preventMaxNum; }
        set { preventMaxNum = value; }
    }

    public static Vector2 SpawnIntervalRange
    {
        get { return spawnIntervalRange; }
        set { spawnIntervalRange = value; }
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
}
