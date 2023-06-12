using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;     // 移动速度
    public float mouseSensitivity = 2f;  // 鼠标灵敏度

    private Rigidbody rb;
    private Camera playerCamera;
    private bool isMouseLocked = true;   // 标记鼠标是否被锁定
    public float jumpForce = 5f;     // 跳跃力量

    private bool isJumping = false;

    private float rotationX = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

        // 锁定鼠标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // 处理移动
        float moveX = Input.GetAxis("Horizontal");   // 水平移动输入
        float moveZ = Input.GetAxis("Vertical");     // 垂直移动输入

        // 获取物体的局部前后左右向量
        Vector3 localForward = transform.forward;
        Vector3 localRight = transform.right;

        // 计算移动方向
        Vector3 movement = (localForward * moveZ + localRight * moveX) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // 处理旋转
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // 根据鼠标水平移动旋转角度
        transform.Rotate(Vector3.up * mouseX);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
        // 切换鼠标锁定和显示
        if (Input.GetKeyDown(KeyCode.B))
        {
            isMouseLocked = !isMouseLocked;

            if (isMouseLocked)
            {
                // 锁定鼠标
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameObject.Find("Canvas3").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Canvas3").GetComponentInChildren<InventoryPanelscript>().currentObject = -1;
                if (GameObject.Find("Canvas3").GetComponentInChildren<InventoryPanelscript>().followmouseprefab != null)
                {
                    Destroy(GameObject.Find("Canvas3").GetComponentInChildren<InventoryPanelscript>().followmouseprefab);
                }
                GameObject.Find("Canvas3").GetComponentInChildren<InventoryPanelscript>().enabled = false;
            }
            else
            {
                // 解锁鼠标
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GameObject.Find("Canvas3").GetComponent<Canvas>().enabled = true;
                GameObject.Find("Canvas3").GetComponentInChildren<InventoryPanelscript>().enabled = true;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
