using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;     // �ƶ��ٶ�
    public float mouseSensitivity = 2f;  // ���������

    private Rigidbody rb;
    private Camera playerCamera;
    private bool isMouseLocked = true;   // �������Ƿ�����
    public float jumpForce = 5f;     // ��Ծ����

    private bool isJumping = false;

    private float rotationX = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

        // �������
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // �����ƶ�
        float moveX = Input.GetAxis("Horizontal");   // ˮƽ�ƶ�����
        float moveZ = Input.GetAxis("Vertical");     // ��ֱ�ƶ�����

        // ��ȡ����ľֲ�ǰ����������
        Vector3 localForward = transform.forward;
        Vector3 localRight = transform.right;

        // �����ƶ�����
        Vector3 movement = (localForward * moveZ + localRight * moveX) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // ������ת
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // �������ˮƽ�ƶ���ת�Ƕ�
        transform.Rotate(Vector3.up * mouseX);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
        // �л������������ʾ
        if (Input.GetKeyDown(KeyCode.B))
        {
            isMouseLocked = !isMouseLocked;

            if (isMouseLocked)
            {
                // �������
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
                // �������
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
