/**
 * 该脚本用于3D放置 
 * 详细注释请看level1中的InventoryPanel1
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelScript_Level3 : MonoBehaviour
{
    public GameObject Panel; // 面板
    public Camera mainCamera;
    public GameObject[] Pane = new GameObject[6];
    public GameObject[] Name = new GameObject[6];
    public LayerMask terrainLayer; // 地形的图层
    public float placementOffset = 7f;
    public float maxDistance = 40f; // 最大距离

    private string[] buttonname = { "button1", "button2", "button3", "button4", "button5", "button6" };
    private GameObject followmouseprefab;
    public int objectnum = 4;
    private int currentObject, pastObject;

    void Start()
    {
        currentObject = -1;
        pastObject = -1;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Panel.SetActive(!Panel.activeSelf);
        }

        if (Panel.activeSelf)
        {
            for (int i = 0; i < objectnum; i++)
            {
                if (currentObject == i)
                {
                    GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = true;
                    continue;
                }
                GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = false;
            }
        }


        if (GameObject.Find("level3start") == null)
        {
            if (currentObject != pastObject)
            {
                pastObject = currentObject;
                if (currentObject >= 0 && currentObject < 6)
                {
                    Destroy(followmouseprefab);
                    followmouseprefab = Instantiate(Name[currentObject]);
                }

            }

            if (currentObject != -1)
            {
                followmouseprefab.transform.position = getPlacePosition();
            }

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUI() && currentObject != -1)
            {
                GameObject prefab1 = Instantiate(Pane[currentObject]);

                prefab1.GetComponentInChildren<Collider>().enabled = true;

                prefab1.transform.position = getPlacePosition();

                Destroy(followmouseprefab);
                currentObject = -1;
            }
        }
    }

    Vector3 getPlacePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, terrainLayer))
        {
            // 计算放置位置
            Vector3 placementPosition = ray.origin + ray.direction * (hit.distance - placementOffset);

            return placementPosition;
        }
        else
        {
            // 超过最大距离时，将物体放置在射线的最远位置处
            Vector3 placementPosition = ray.origin + ray.direction * maxDistance;
            return placementPosition;
        }
    }

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

    private bool IsPointerOverUI()
    {
        // 检查鼠标点击是否在UI上
        return EventSystem.current.IsPointerOverGameObject();
    }

}
