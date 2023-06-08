using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//这个脚本挂载到Panel1上，是物品栏的脚本
public class InventoryPanelscript : MonoBehaviour
{
    Button button1, button2, button3, button4, button5, button6;
    string[] buttonname = { "button1","button2" , "button3", "button4" , "button5", "button6" };
    GameObject followmouseprefab;  //这个是跟随鼠标动的那个
    GameObject pane;
    public GameObject pane1, pane2, pane3, pane4, pane5, pane6;
    public GameObject[] Name = new GameObject[6];
    Camera mainCamera;


    int currentObject, pastObject;  //currentObject变量代表着当前选中的格子是哪一个，-1代表未选中;pastobject表示之前选中的物体
    void Start()
    {
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
        for (int i = 0; i < 6; i++)  //这里用来调整选中效果是否可见
        {
            if (currentObject == i)
            {
                GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = true;
                continue;
            }
            GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = false;
        }

        if (GameObject.Find("level3start") == null)  
        {
            Destroy(followmouseprefab); //游戏已经开始了，把跟随鼠标动的物体删掉
        }
        
        if (GameObject.Find("level3start") != null)  //那个按钮还在，说明还未开始游戏
        {
            if (currentObject != pastObject)
            {
                pastObject = currentObject;
                if (currentObject >= 0 && currentObject < 6)
                {
                    Destroy(followmouseprefab);
                    followmouseprefab = Instantiate(Name[currentObject]);
                    followmouseprefab.GetComponentInChildren<Collider>().enabled = false;
                }

            }

            //创建一个物体跟着鼠标移动
            if (currentObject != -1) //如果物体原本有重力以及膨胀效果，可以把下边前两行的注释删掉
            {
                //  prefab.GetComponentInChildren<Rigidbody>().useGravity = false;
                //  prefab.GetComponentInChildren<Controller>().enabled = false;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Vector3 groundPoint = new Vector3();
                if (Physics.Raycast(ray, out hit))
                {
                    // 获取交点坐标
                    groundPoint = hit.point;
                }
                groundPoint += new Vector3(0,1,0);
                followmouseprefab.transform.position = groundPoint;
                 ;
            }

            //点击后放置物体
            if (Input.GetMouseButtonDown(0) && !(Input.mousePosition.x > 174 && Input.mousePosition.x < 685 && Input.mousePosition.y < 74) && !(Input.mousePosition.x > 340 && Input.mousePosition.x < 650 && Input.mousePosition.y > 340) && currentObject != -1)
            {
                GameObject prefab1 = Instantiate(Name[currentObject]);
             
                prefab1.GetComponentInChildren<Collider>().enabled = true;
               // prefab1.GetComponentInChildren<Rigidbody>().useGravity = true;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Vector3 groundPoint=new Vector3();
                if (Physics.Raycast(ray, out hit))
                {
                    // 获取交点坐标
                    groundPoint = hit.point;
                }
                groundPoint += new Vector3(0, 1, 0);
                prefab1.transform.position = groundPoint;
            }
        }
    }

    //一下这些func()函数就是用来判断是否处于选中状态。这些部分不用做更改
    void func1()
    {
        if (currentObject != 0)
        {
            currentObject = 0;
        }
        else currentObject = -1;

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
        if (currentObject != 2)
        {
            currentObject = 2;
        }
        else currentObject = -1;

    }
    void func4()
    {
        if (currentObject !=3)
        {
            currentObject = 3;
        }
        else currentObject = -1;
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

    // Update is called once per frame
   
}

