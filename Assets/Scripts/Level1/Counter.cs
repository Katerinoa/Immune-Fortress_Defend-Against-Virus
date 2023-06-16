/*
 * 该脚本用于游戏计数
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public static int generateCount;  //生成病毒数量
    public static int destroyCount;   //阻挡病毒数量
    public static int passCount;      //入侵病毒数量

    void Awake()
    {
        // 初始化计数器
        generateCount = 0;
        destroyCount = 0;
        passCount = 0;
    }
}
