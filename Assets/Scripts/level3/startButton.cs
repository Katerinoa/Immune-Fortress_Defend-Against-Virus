using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//startButton�ǿ�ʼ�Ľű������ص�start��ť��
public class startButton : MonoBehaviour
{
    GameObject start;
    public int if_start = 0;  //�ж��Ƿ�ʼ��0Ϊδ��ʼ��1Ϊ��ʼ
    

    void Start()
    {
        start = GameObject.Find("level3start");  //��ȡ��level3�����������Ҫ������������
        start.GetComponent<Button>().onClick.AddListener(Start1); //��ť����¼�
    }
    void Start1()
    {
        if_start = 1;
        start.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
