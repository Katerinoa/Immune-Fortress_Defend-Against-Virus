using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Level3 : MonoBehaviour
{
    public static int maxGenerateNum = 100;  // 病毒最大生成数量

    public static Vector2 SpawnIntervalRange = new Vector2(2.5f, 5.0f); // 生成间隔

    public static float infectTime = 3.0f; // 侵染时间

    public static float SplitTime = 5.0f;  // 裂解时间

    public static float virusSpeed = 4.0f; // 病毒移速

    public static float crazyTime = 5.0f;  // B细胞强化时长

    public static float fireSpeed = 2f;    // T细胞攻击速度

}
