using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonClick : MonoBehaviour
{
    public GameObject setting;
    public void ChangeToLevel1()
    {
        SceneManager.LoadScene("prelevel1");
    }

    public void ChangeToSetting()
    {
        setting.SetActive(true);
        gameObject.SetActive(false);
        //SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
