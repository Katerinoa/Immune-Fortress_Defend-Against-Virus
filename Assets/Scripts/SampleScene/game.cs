using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour
{
    GameObject prefab;
    GameObject pane;
    public GameObject wcell,Tcell,virus,Bcell,cell,macrophage;
    public GameObject[] Name = new GameObject[6];
    GameObject start;
    public int if_start = 0;
    Camera mainCamera;

  
    int id;
    int id2;
    Color color;
    // Start is called before the first frame update
    void Start()
    {
        Name[0] = wcell;
        Name[1] =cell;
        Name[2] =Bcell;
        Name[3] =Tcell;
        Name[4] =macrophage;
        Name[5] =virus;
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
                prefab = Instantiate(Name[id]);
                prefab.GetComponentInChildren<Collider>().enabled = false;

                color = prefab.GetComponentInChildren<Renderer>().material.color;
                color.a = 0.5f;
                prefab.GetComponentInChildren<Renderer>().material.color = color;
            }


        }
        //    Debug.Log(Input.mousePosition);
        //创建一个物体跟着鼠标移动
        if (id != -1)
        {

            prefab.GetComponentInChildren<Rigidbody>().useGravity = false;
            prefab.GetComponentInChildren<Controller>().enabled = false;
            Vector3 mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = 13f; // 设置到摄像机的距离，这个值会影响你获得的三维坐标，你可以根据需要自行设置

            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition); // 将屏幕空间坐标转换为世界空间坐标 (三维坐标)
            prefab.transform.position = mouseWorldPosition;
        }

        if (Input.GetMouseButtonDown(0) && !(Input.mousePosition.x > 174 && Input.mousePosition.x < 685 && Input.mousePosition.y < 74)&&!(Input.mousePosition.x>340&&Input.mousePosition.x<650&&Input.mousePosition.y>340)&& id != -1)
        {
            GameObject prefab1 = Instantiate(Name[id]);
            Vector3 mouseScreenPosition = Input.mousePosition;
                prefab1.GetComponentInChildren<Collider>().enabled = true;
                mouseScreenPosition.z = 13f; // 设置到摄像机的距离，这个值会影响你获得的三维坐标，你可以根据需要自行设置

            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition); // 将屏幕空间坐标转换为世界空间坐标 (三维坐标)
            mouseWorldPosition.y = 1;
            prefab1.transform.position = mouseWorldPosition;
        }
    }
    }
}
