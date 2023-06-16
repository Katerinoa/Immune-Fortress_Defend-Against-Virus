/*
 * �ýű�������Ϸ����
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public static int generateCount;  //���ɲ�������
    public static int destroyCount;   //�赲��������
    public static int passCount;      //���ֲ�������

    void Awake()
    {
        // ��ʼ��������
        generateCount = 0;
        destroyCount = 0;
        passCount = 0;
    }
}
