using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonscript : MonoBehaviour
{
    Button button1, button2, button3, button4, button5, button6;
    string[] buttonname = { "button1","button2" , "button3", "button4" , "button5", "button6" };
    // Start is called before the first frame update
    public int id;
    void Start()
    {
        id = -1;
        button1 = GameObject.Find("button1").GetComponent<Button>();
        button1.onClick.AddListener(func1);

        button2 = GameObject.Find("button2").GetComponent<Button>();
        button2.onClick.AddListener(func2);

        button3 = GameObject.Find("button3").GetComponent<Button>();
        button3.onClick.AddListener(func3);

        button4 = GameObject.Find("button4").GetComponent<Button>();
        button4.onClick.AddListener(func4);

        button5 = GameObject.Find("button5").GetComponent<Button>();
        button5.onClick.AddListener(func5);

        button6 = GameObject.Find("button6").GetComponent<Button>();
        button6.onClick.AddListener(func6);

    }

    void func1()
    {
        if (id != 0)
        {
            id = 0;
            Debug.Log("button1");
        }
        else id = -1;

        Debug.Log("1");
    }
    void func2()
    {
        if (id != 1)
        {
            id = 1;
        }
        else id = -1;
        Debug.Log("2");
    }
    void func3()
    {
        if (id != 2)
        {
            id = 2;
        }
        else id = -1;

        Debug.Log("3");
    }
    void func4()
    {
        if (id !=3)
        {
            id = 3;
        }
        else id = -1;

        Debug.Log("4");
    }
    void func5()
    {
        if (id != 4)
        {
            id = 4;
        }
        else id = -1;
        Debug.Log("5");
    }
    void func6()
    {
        if (id != 5)
        {
            id = 5;
        }
        else id = -1;
        Debug.Log("6");
    }

    // Update is called once per frame
    void Update()
    {
            for(int i = 0; i < 6; i++)
            {
                if (id == i) {
                GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = true;
              //  Debug.Log(buttonname[i]);
                continue;
             }
             GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = false;
            }
        
    }
}
