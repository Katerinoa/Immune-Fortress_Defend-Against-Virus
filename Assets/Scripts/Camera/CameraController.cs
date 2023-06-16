using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("绑定的相机")]
    public Transform player;

    [Tooltip("鼠标灵敏度")]
    public float mouseSensitivity = 100f;

    public float baseRotationX = 0f;

    private float mouseX, mouseY;

    private float xRotation = 0f;

    private bool isLocked = true; // 初始状态为锁定

    private void Update()
    {
        if (!isLocked)
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -10f, 35f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLocked = !isLocked; // 按下空格键切换锁定状态
        }

        if (!isLocked)
        {
            player.Rotate(Vector3.up * mouseX);
            transform.localRotation = Quaternion.Euler(xRotation+ baseRotationX, 0, 0);
        }
    }
}
