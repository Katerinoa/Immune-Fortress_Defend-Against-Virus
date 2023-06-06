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

    // ��ӡ���ɲ������������ز�������
    private void PrintCount()
    {
        Debug.Log("���ɲ�������:" + generateCount + "��");
        Debug.Log("���ز�������:" + destroyCount + "��");
        Debug.Log("���ݲ�������:" + passCount + "��");

    }
}
