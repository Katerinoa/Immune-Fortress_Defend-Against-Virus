using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("移动速度")]
    public float moveSpeed = 30;
    [Tooltip("滚轮灵敏度")]
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
