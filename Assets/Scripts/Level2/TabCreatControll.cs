/*
*  下方Tab栏UI——点击预生成对象
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabCreatControll : MonoBehaviour
{
    private Button button1, button2, button3, button4, button5, button6;                                        //细胞贴图放置的按钮
    private string[] buttonname = { "button1", "button2", "button3", "button4", "button5", "button6" };         //细胞对应编号
    public static GameObject followmouseprefab;                                                                 //预生成的细胞预制体
    private GameObject pane;                                                                                    //选择栏
    public GameObject pane1, pane2, pane3, pane4, pane5, pane6;                                                 //细胞对象
    public GameObject[] Name = new GameObject[6];                                                               //细胞对象数组
    private Camera mainCamera;                                                                                  //主相机


    private int pastObject;  
    public static int currentObject;  //改成公有静态，给GridControll用
    void Start()
    {
        //初始化确定栏中的各种细胞
        Name[0] = pane1;
        Name[1] = pane2;
        Name[2] = pane3;
        Name[3] = pane4;
        Name[4] = pane5;
        Name[5] = pane6;
        currentObject = -1;

        pastObject = -1;
        pane = GameObject.Find("Panel1");
        mainCamera = Camera.main;

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


    void Update()
    {
        //判断点击的对象
        for (int i = 0; i < 6; i++)  
        {
            if (currentObject == i)
            {
                GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = true;
                continue;
            }
            GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = false;
        }
        if (true)  
        {
            //预生成对象
            if (currentObject != pastObject)
            {
                pastObject = currentObject;
                if (currentObject == 1 || currentObject == 4)
                {
                    Destroy(followmouseprefab);
                    followmouseprefab = Instantiate(Name[currentObject]);
                    if(currentObject == 4) {
                        followmouseprefab.GetComponent<AttackUI>().enabled = false;  //禁用脚本，此时还无法攻击或修复
                        followmouseprefab.transform.Find("bullet").gameObject.GetComponent<BulletControll>().enabled = false;
                        }
                }

            }

           //预点击后要生成的对象跟随鼠标
            if (currentObject != -1) 
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Vector3 groundPoint = new Vector3();
                if (Physics.Raycast(ray, out hit))
                {
                    groundPoint = hit.point;
                }
                groundPoint += new Vector3(0, 0.2f, 0);
                if(followmouseprefab != null) followmouseprefab.transform.position = groundPoint;
            }
        }
    }

    
    //各个对象对应的下标值
    void func1()
    {
        currentObject = -1;
    }
    void func2()
    {
        if (currentObject != 1)
        {
            currentObject = 1;
        }
        else currentObject = -1;

    }
    void func3()
    {
        currentObject = -1;

    }
    void func4()
    {
        currentObject = -1;
    }
    void func5()
    {
        if (currentObject != 4)
        {
            currentObject = 4;
        }
        else currentObject = -1;
    }
    void func6()
    {
        if (currentObject != 5)
        {
            currentObject = 5;
        }
        else currentObject = -1;
    }

}
