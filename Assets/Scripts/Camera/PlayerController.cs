using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("�ƶ��ٶ�")]
    public float moveSpeed = 30;
    [Tooltip("����������")]
    public float scrollSensitivity = 500f;

    private CharacterController cc;

    private float horizontalMove, verticalMove, scrollMove;

    private Vector3 dir;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        verticalMove = Input.GetAxis("Vertical") * moveSpeed;
        scrollMove = -Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;

        dir = transform.forward * verticalMove + transform.right * horizontalMove + transform.up * scrollMove;

        cc.Move(dir * Time.deltaTime);
    }
}
