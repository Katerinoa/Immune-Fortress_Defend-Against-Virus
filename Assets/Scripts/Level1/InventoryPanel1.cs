/* 
 * 该脚本用于放置道具
 */
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventoryPanel1 : MonoBehaviour
{
    public GameObject Panel;            // 道具面板
    public GameObject generatePoses;    // 刷怪点
    public Camera mainCamera;           // 主摄像机
    public GameObject[] Pane = new GameObject[6];   // 用于放置的物体
    public GameObject[] Name = new GameObject[6];   // 跟随鼠标的物体（没有组件的模型）
    public int[] Num = new int[6] { 5, 2, 0, 0, 0, 0 };  // 每个道具的数量
    public LayerMask terrainLayer; // 地形图层 
    public TextMeshProUGUI[] remainNum = new TextMeshProUGUI[6];  //道具数量显示

    private int[] Count = new int[6];   // 道具计数器
    private float placementOffset = 5f; // 放置偏移
    private bool gameStart = false;     // 判断游戏是否开始
    private string[] buttonname = { "button1", "button2", "button3", "button4", "button5", "button6" };
    private GameObject followmouseprefab; //跟随鼠标的物体
    private int objectnum = 0;
    private int currentObject, pastObject;
    private Button button1, button2, button3, button4, button5, button6; //物品格子

    private void Start()
    {
        // 初始化索引
        currentObject = -1;
        pastObject = -1;

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
        // 更新道具数量值
        for (int i = 0; i < objectnum; i++)
            remainNum[i].text = (Num[i]-Count[i]).ToString();

        // 选中物品栏
        if (Panel.activeSelf)
        {
            for (int i = 0; i < objectnum; i++)
            {
                if (currentObject == i)
                {
                    if (Count[currentObject] >= Num[currentObject])
                        return;
                    GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = true;
                    continue;
                }
                GameObject.Find(buttonname[i]).GetComponent<RawImage>().enabled = false;
            }
        }

        // 获取点击后，跟随鼠标的物体
        if (currentObject != pastObject)
        {
            pastObject = currentObject;
            if (currentObject >= 0 && currentObject < 6)
            {
                Destroy(followmouseprefab);
                if (Count[currentObject] >= Num[currentObject])
                    return; 
                followmouseprefab = Instantiate(Name[currentObject]);
            }

        }

        // 鼠标跟随
        if (currentObject != -1)
        {
            followmouseprefab.transform.position = getPlacePosition();
        }

        // 道具放置
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI() && currentObject != -1)
        {
            if (Count[currentObject] >= Num[currentObject])
                return;

            GameObject prefab1 = Instantiate(Pane[currentObject]);

            Count[currentObject]++;

            prefab1.GetComponentInChildren<Collider>().enabled = true;

            prefab1.transform.position = getPlacePosition();

            Destroy(followmouseprefab);
            currentObject = -1;
        }
    }

    // 计算放置点
    Vector3 getPlacePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 得到射线与地形的交点
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainLayer))
        {
            Vector3 placementPosition = ray.origin + ray.direction * (hit.distance - placementOffset); //添加一个偏移量，避免嵌入地面中
            return placementPosition;
        }
        return Vector3.zero;    
    }

    // 按钮控制 游戏开始
    public void StartGame()
    {
        Panel.SetActive(false);
        generatePoses.SetActive(true);
        gameStart = true;
    }

    // 按钮事件监听
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

