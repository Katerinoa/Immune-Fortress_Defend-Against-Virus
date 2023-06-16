using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    private static int preventMaxNum = 10; //绒毛最大阻挡数量

    private static float speed = 2.0f;  // 病毒移动速度

    private static int maxGenerateNum = 1000;  // 病毒最大生成数量



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

}
