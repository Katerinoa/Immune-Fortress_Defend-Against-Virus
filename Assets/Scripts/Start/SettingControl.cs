using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour
{
    public GameObject main;
    public GameObject control;
    public int difficultykind;
    private AudioSource mainaudio;
    private Slider audioslider;
    private Toggle easy;
    private Toggle medium;
    private Toggle difficult;
    // Start is called before the first frame update
    void Start()
    {
        mainaudio = control.GetComponent<AudioSource>();
        audioslider = gameObject.transform.Find("audio/audioslider").GetComponent<Slider>();
        easy = gameObject.transform.Find("difficulty/easy").GetComponent<Toggle>();
        medium = gameObject.transform.Find("difficulty/medium").GetComponent<Toggle>();
        difficult = gameObject.transform.Find("difficulty/difficult").GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(difficultykind);
        mainaudio.volume = audioslider.value;
    }

    public void ClickCancel()
    {
        main.SetActive(true);
        gameObject.SetActive(false);
    }
    public void ClickEasy()
    {
        if (easy.isOn == true)
        {
            difficultykind = 0;
        }
    }
    public void ClickMedium()
    {
        if (medium.isOn == true)
        {
            difficultykind = 1;
        }
    }
    public void ClickDifficult()
    {
        if (difficult.isOn == true)
        {
            difficultykind = 2;
        }
    }
}
