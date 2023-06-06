using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControll : MonoBehaviour
{
    public int fps = 60;
    private void Awake()
    {
        Application.targetFrameRate = fps; // ÓÎÏ·Ö¡ÂÊ
    }
}
