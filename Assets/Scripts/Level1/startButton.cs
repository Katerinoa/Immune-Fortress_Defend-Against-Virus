using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//startButton�ǿ�ʼ�Ľű������ص�start��ť��
public class startButton : MonoBehaviour
{
    public GameObject start;
    
    public void StartGame()
    {
        start.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
