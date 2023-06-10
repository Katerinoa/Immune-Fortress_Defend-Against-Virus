using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//startButton是开始的脚本，挂载到start按钮上
public class startButton : MonoBehaviour
{
    GameObject start;
    public int if_start = 0;  //判断是否开始，0为未开始，1为开始
    

    void Start()
    {
        start = GameObject.Find("level3start");  //获取到level3对象，这里可能要重新设置名字
        start.GetComponent<Button>().onClick.AddListener(Start1); //按钮点击事件
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
