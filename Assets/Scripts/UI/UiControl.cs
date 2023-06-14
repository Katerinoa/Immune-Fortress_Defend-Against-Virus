using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UiControl : MonoBehaviour
{

    public void ClickInfo()
    {
        Time.timeScale = 0;
        //SceneManager.LoadScene(0);
    }


    public void ClickSetting()
    {
        //SceneManager.LoadScene(0);
    }
    public void ClickHome()
    {
        SceneManager.LoadScene(0);
    }
}
