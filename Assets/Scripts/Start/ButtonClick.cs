using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonClick : MonoBehaviour
{
    public void ChangeToLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeToSetting()
    {
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
