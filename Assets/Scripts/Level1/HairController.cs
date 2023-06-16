/*
 * �ýű����ڿ�����ë����Ϊ
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairController : MonoBehaviour
{
    private int preventMaxNum;      // ���������
    private float preventCount = 0; // ��ǰ������

    private void Awake()
    {
        preventMaxNum = Core.PreventMaxNum; // ��Core�л�ȡ�����������
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("virus"))
        {
            preventCount++;
        }
        if (preventCount > preventMaxNum)
            Destroy(gameObject); // �����������������
    }

}

