using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonClick : MonoBehaviour
{
    public GameObject setting;
    public void ClickStart()
    {
        SceneManager.LoadScene("Middlepic");
    }
    public void ClickSetting()
    {
        setting.SetActive(true);
        gameObject.SetActive(false);
    }
    public void ClickQuit()
    {
        Application.Quit();
    }

    public void ClickLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void ClickLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void ClickLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

}
