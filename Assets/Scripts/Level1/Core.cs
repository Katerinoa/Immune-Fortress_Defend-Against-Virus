using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    private static int preventMaxNum = 10; //��ë����赲����

    private static Vector2 spawnIntervalRange = new Vector2(0.5f, 1.5f);  //����ʱ����

    private static float speed = 10.0f;  // �����ƶ��ٶ�

    private static int maxGenerateNum = 800;  // ������ɲ�������

    // �ṩpublic���������ڷ����������������������ⲿ�޸�
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
