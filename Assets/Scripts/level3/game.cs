using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour
{
    GameObject prefab;
    GameObject pane;
    public GameObject wcell,Tcell,virus,Bcell,cell,macrophage;
    public GameObject[] name=new GameObject[6];
    GameObject start;
    public int if_start = 0;
    Camera mainCamera;

  
    int id;
    int id2;
    Color color;
    // Start is called before the first frame update
    void Start()
    {
        name[0] = wcell;
        name[1] =cell;
        name[2] =Bcell;
        name[3] =Tcell;
        name[4] =macrophage;
        name[5] =virus;
        id = -1;
        id2 = -1;
        pane = GameObject.Find("Panel1");
        mainCamera = Camera.main;
         color= new Color();
        start = GameObject.Find("start");
        start.GetComponent<Button>().onClick.AddListener(Start1);
        
    }
    void Start1()
    {
        if_start = 1;
        start.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameObject").GetComponent<game>().if_start == 1)
        {
            Destroy(prefab);
        }
        id = pane.GetComponentInChildren<buttonscript>().id;
        if (GameObject.Find("GameObject").GetComponent<game>().if_start == 0)
        {
        if (id != id2)
        {
            id2 = id;
            if (id >= 0 && id < 6)
            {
                Destroy(prefab);
                prefab = Instantiate(name[id]);
                prefab.GetComponentInChildren<Collider>().enabled = false;

                color = prefab.GetComponentInChildren<Renderer>().material.color;
                color.a = 0.5f;
                prefab.GetComponentInChildren<Renderer>().material.color = color;
            }


        }
        //����һ�������������ƶ�
        if (id != -1)
        {

            prefab.GetComponentInChildren<Rigidbody>().useGravity = false;
            prefab.GetComponentInChildren<Controller>().enabled = false;
            Vector3 mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = 13f; // ���õ�������ľ��룬���ֵ��Ӱ�����õ���ά���꣬����Ը�����Ҫ��������

            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition); // ����Ļ�ռ�����ת��Ϊ����ռ����� (��ά����)


            prefab.transform.position = mouseWorldPosition;
        }

        if (Input.GetMouseButtonDown(0) && !(Input.mousePosition.x > 174 && Input.mousePosition.x < 685 && Input.mousePosition.y < 74) && id != -1)
        {
            GameObject prefab1 = Instantiate(name[id]);
            Vector3 mouseScreenPosition = Input.mousePosition;

            mouseScreenPosition.z = 13f; // ���õ�������ľ��룬���ֵ��Ӱ�����õ���ά���꣬����Ը�����Ҫ��������

            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition); // ����Ļ�ռ�����ת��Ϊ����ռ����� (��ά����)
            mouseWorldPosition.y = 1;
            prefab1.transform.position = mouseWorldPosition;
        }
    }
    }
}