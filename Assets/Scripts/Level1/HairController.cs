/*
 * 该脚本用于控制纤毛的行为
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairController : MonoBehaviour
{
    private int preventMaxNum;      // 最大阻拦数
    private float preventCount = 0; // 当前阻拦数

    private void Awake()
    {
        preventMaxNum = Core.PreventMaxNum; // 从Core中获取最大阻拦数量
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("virus"))
        {
            preventCount++;
        }
        if (preventCount > preventMaxNum)
            Destroy(gameObject); // 超过最大阻拦数销毁
    }

}

