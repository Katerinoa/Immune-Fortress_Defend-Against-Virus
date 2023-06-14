using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoControl : MonoBehaviour
{
    public GameObject main;
    //public GameObject control;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ClickCancel()
    {
        main.SetActive(true);
        gameObject.SetActive(false);
    }
    public void NextPage()
    {

    }
    public void BeforePage()
    {

    }
}
