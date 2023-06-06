using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public static int generateCount;
    public static int destroyCount;
    public static int passCount;

    // Start is called before the first frame update
    void Start()
    {
        generateCount = 0;
        destroyCount = 0;
        passCount = 0;
        InvokeRepeating("PrintCount", 3f, 3f);
    }

    // 打印生成病毒数量和拦截病毒数量
    private void PrintCount()
    {
        Debug.Log("生成病毒数量:" + generateCount + "个");
        Debug.Log("拦截病毒数量:" + destroyCount + "个");
        Debug.Log("逃逸病毒数量:" + passCount + "个");

    }
}
