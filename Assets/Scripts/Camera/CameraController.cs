using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("�󶨵����")]
    public Transform player;

    [Tooltip("���������")]
    public float mouseSensitivity = 100f;

    public bool needLock = true;

    private float mouseX, mouseY;

    private float xRotation = 0f;

    private bool isLocked = true; // ��ʼ״̬Ϊ����

    private void Update()
    {
        if (!isLocked || !needLock)
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -10f, 35f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLocked = !isLocked; // ���¿ո���л�����״̬
        }

        if (!isLocked || !needLock)
        {
            player.Rotate(Vector3.up * mouseX);
            transform.localRotation = Quaternion.Euler(xRotation-15, 0, 0);
        }
    }
}
