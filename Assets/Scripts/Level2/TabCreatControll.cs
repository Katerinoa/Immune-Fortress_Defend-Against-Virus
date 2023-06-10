using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabCreatControll : MonoBehaviour
{
    Button button1, button2, button3, button4, button5, button6;
    string[] buttonname = { "button1", "button2", "button3", "button4", "button5", "button6" };
    public static GameObject followmouseprefab;  //����Ǹ�����궯���Ǹ�
    GameObject pane;
    public GameObject pane1, pane2, pane3, pane4, pane5, pane6;
    public GameObject[] Name = new GameObject[6];
    Camera mainCamera;


    int pastObject;  
    public static int currentObject;  //改成公有静态，给GridControll用
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
        for (int i = 0; i < 6; i++)  
        {
            if (currentObject == i)
            {
                GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = true;
                continue;
            }
            GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = false;
        }

        // if (GameObject.Find("level3start") == null)
        // {
        //     Destroy(followmouseprefab); 
        // }

        if (true)  
        {
            if (currentObject != pastObject)
            {
                pastObject = currentObject;
                if (currentObject == 1 || currentObject == 4)
                {
                    Destroy(followmouseprefab);
                    followmouseprefab = Instantiate(Name[currentObject]);
                   // followmouseprefab.GetComponentInChildren<Collider>().enabled = false;
                   //禁用脚本
                    if(currentObject == 4) {
                        followmouseprefab.GetComponent<AttackUI>().enabled = false;
                        followmouseprefab.transform.Find("bullet").gameObject.GetComponent<BulletControll>().enabled = false;
                        }
                }

            }

           //预点击后对象跟随鼠标
            if (currentObject != -1) 
            {
                //  prefab.GetComponentInChildren<Rigidbody>().useGravity = false;
                //  prefab.GetComponentInChildren<Controller>().enabled = false;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Vector3 groundPoint = new Vector3();
                if (Physics.Raycast(ray, out hit))
                {
                    // ��ȡ��������
                    groundPoint = hit.point;
                }
                groundPoint += new Vector3(0, 0.2f, 0);
                if(followmouseprefab != null) followmouseprefab.transform.position = groundPoint;
            }

            //这个不行，是只能放在特定格子里，所以判断条件不同，放在grid的脚本里更好操作，不用计算
            //缺点：封装性不够
            // if (Input.GetMouseButtonDown(0) && !(Input.mousePosition.x > 174 && Input.mousePosition.x < 685 && Input.mousePosition.y < 74) && !(Input.mousePosition.x > 340 && Input.mousePosition.x < 650 && Input.mousePosition.y > 340) && currentObject != -1)
            // {
            //     GameObject prefab1 = Instantiate(Name[currentObject]);

            //     prefab1.GetComponentInChildren<Collider>().enabled = true;
            //     // prefab1.GetComponentInChildren<Rigidbody>().useGravity = true;

            //     //这个应该是摆放位置
            //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //     RaycastHit hit;
            //     Vector3 groundPoint = new Vector3();
            //     if (Physics.Raycast(ray, out hit))
            //     {
            //         // ��ȡ��������
            //         groundPoint = hit.point;
            //     }
            //     groundPoint += new Vector3(0, 1, 0);
            //     prefab1.transform.position = groundPoint;
            // }
        }
    }

    
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

    // Update is called once per frame

}
