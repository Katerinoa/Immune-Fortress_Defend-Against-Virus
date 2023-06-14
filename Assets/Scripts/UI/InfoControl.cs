using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoControl : MonoBehaviour
{
    public GameObject main;
    //public GameObject control;
    private void Awake()
    {
        Time.timeScale = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Click()
    {
        gameObject.SetActive(true);
        main.SetActive(false);
        Time.timeScale = 0;

    }

    public void ClickCancel()
    {
        main.SetActive(true);
        gameObject.SetActive(false);
        Time.timeScale = 1;

    }
    public void NextPage()
    {

    }
    public void BeforePage()
    {

    }
}
