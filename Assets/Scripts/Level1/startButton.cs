using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//startButton是开始的脚本，挂载到start按钮上
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
