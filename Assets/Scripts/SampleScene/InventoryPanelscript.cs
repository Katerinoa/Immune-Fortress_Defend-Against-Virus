using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//����ű����ص�Panel1�ϣ�����Ʒ���Ľű�
public class InventoryPanelscript : MonoBehaviour
{
    Button button1, button2, button3, button4, button5, button6;
    string[] buttonname = { "button1", "button2", "button3", "button4", "button5", "button6" };
    public GameObject followmouseprefab;  //����Ǹ�����궯���Ǹ�
    public Camera mainCamera;
    GameObject pane;
    public GameObject pane1, pane2, pane3, pane4, pane5, pane6;
    public GameObject[] Name = new GameObject[6];

    int objectnum = 0;
    public int currentObject, pastObject;  //currentObject���������ŵ�ǰѡ�еĸ�������һ����-1����δѡ��;pastobject��ʾ֮ǰѡ�е�����
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

        if (GameObject.Find("button1") != null)
        {
            button1 = GameObject.Find("button1").GetComponent<Button>();
            button1.onClick.AddListener(func1);
        }
        objectnum += 1;
        if (GameObject.Find("button2") != null)
        {
            button2 = GameObject.Find("button2").GetComponent<Button>();
            button2.onClick.AddListener(func2);
            objectnum += 1;
        }
        if (GameObject.Find("button3") != null)
        {
            button3 = GameObject.Find("button3").GetComponent<Button>();
            button3.onClick.AddListener(func3);
            objectnum += 1;
        }

        if (GameObject.Find("button4") != null)
        {
            button4 = GameObject.Find("button4").GetComponent<Button>();
            button4.onClick.AddListener(func4);
            objectnum += 1;
        }
        if (GameObject.Find("button5") != null)
        {
            button5 = GameObject.Find("button5").GetComponent<Button>();
            button5.onClick.AddListener(func5);
            objectnum += 1;
        }
        if (GameObject.Find("button6") != null)
        {
            button6 = GameObject.Find("button6").GetComponent<Button>();
            button6.onClick.AddListener(func6);
            objectnum += 1;
        }
    }

    void Update()
    {
        for (int i = 0; i < objectnum; i++)  //������������ѡ��Ч���Ƿ�ɼ�
        {
            if (currentObject == i)
            {
                GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = true;
                continue;
            }
            GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = false;
        }
        /*
        if (GameObject.Find("level3start") == null)  
        {
            Destroy(followmouseprefab); //��Ϸ�Ѿ���ʼ�ˣ��Ѹ�����궯������ɾ��
        }*/

        if (GameObject.Find("level3start") == null)  //�Ǹ���ť���ڣ�˵����δ��ʼ��Ϸ
        {
            if (currentObject != pastObject)
            {
                pastObject = currentObject;
                if (currentObject >= 0 && currentObject < 6)
                {
                    Destroy(followmouseprefab);
                    followmouseprefab = Instantiate(Name[currentObject]);
                    followmouseprefab.GetComponentInChildren<Collider>().enabled = false;
                    Controller controller = followmouseprefab.GetComponent<Controller>();
                    followmouseprefab.GetComponentInChildren<Controller>().enabled = false;
                }

            }

            //����һ�������������ƶ�
            if (currentObject != -1) //�������ԭ���������Լ�����Ч�������԰��±�ǰ���е�ע��ɾ��
            {
                //  prefab.GetComponentInChildren<Rigidbody>().useGravity = false;
                //  prefab.GetComponentInChildren<Controller>().enabled = false;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Vector3 groundPoint = new Vector3();
                if (Physics.Raycast(ray, out hit))
                {
                    // ��ȡ��������
                    groundPoint = hit.point;
                }
                groundPoint += new Vector3(0, 1, 0);
                followmouseprefab.transform.position = groundPoint;
                ;
            }

            //������������
            if (Input.GetMouseButtonDown(0)&&!IsPointerOverUI())
            {
                //2、3此处不同
                GameObject prefab1 = Instantiate(Name[currentObject]);

                prefab1.GetComponentInChildren<Collider>().enabled = true;
                // prefab1.GetComponentInChildren<Rigidbody>().useGravity = true;

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Vector3 groundPoint = new Vector3();
                if (Physics.Raycast(ray, out hit))
                {
                    // ��ȡ��������
                    groundPoint = hit.point;
                }
                groundPoint += new Vector3(0, 1, 0);
                prefab1.transform.position = groundPoint;
            }
        }
    }

    //һ����Щfunc()�������������ж��Ƿ���ѡ��״̬����Щ���ֲ���������
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
        if (currentObject != 3)
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
    private bool IsPointerOverUI()
    {
        // 检查鼠标点击是否在UI上
        return EventSystem.current.IsPointerOverGameObject();
    }

}

