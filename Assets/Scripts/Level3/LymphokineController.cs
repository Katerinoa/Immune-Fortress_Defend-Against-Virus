using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LymphokineController : MonoBehaviour
{
    private string Tag = "BCell";
    public GameObject TargetBCell;
    public float forceMagnitude = 10f; // 推力大小
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (TargetBCell == null)
        {
            SelectTarget();
        }
        else
        {
            Vector3 targetDirection = TargetBCell.transform.position - transform.position;
            if (targetDirection != Vector3.zero)
            {
                Vector3 forceDirection = targetDirection.normalized * forceMagnitude;
                rb.AddForce(forceDirection);
                transform.forward = rb.velocity.normalized;
            }
        }
    }

    void Update()
    {
        if (TargetBCell != null && Vector3.Distance(TargetBCell.transform.position, transform.position) < 1f)
        {
            TargetBCell.GetComponent<EffectorBCellController>().crazy = true;
            Destroy(gameObject);
        }
    }
    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject != TargetBCell)
    //     {
    //         Vector3 randomDirection = Random.insideUnitSphere.normalized * forceMagnitude;
    //         // rb.AddForce(randomDirection);
    //     }
    // }

    private void SelectTarget()
    {
        GameObject[] Bcells = GameObject.FindGameObjectsWithTag(Tag).Where(obj => obj.activeSelf).ToArray();

        if (Bcells.Length > 0)
        {
            TargetBCell = Bcells[UnityEngine.Random.Range(0, Bcells.Length)];
        }
    }
}
