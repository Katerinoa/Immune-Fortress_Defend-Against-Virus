using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairController : MonoBehaviour
{
    private int preventMaxNum;
    private float preventCount = 0;

    private void Awake()
    {
        preventMaxNum = Core.PreventMaxNum;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("virus"))
        {
            preventCount++;
        }
        if (preventCount > preventMaxNum)
            Destroy(gameObject);
    }

}
