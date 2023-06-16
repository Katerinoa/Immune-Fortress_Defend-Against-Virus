using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LymphokineController : MonoBehaviour
{
    private string Tag = "BCell";
    public GameObject TargetBCell;
    public float moveSpeed = 10f;
    private Rigidbody rb;
    public int maxCollision = 2;
    public float timer = 10f;


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
                Vector3 direction = (TargetBCell.transform.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;
            }
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        rb.AddForce(randomDirection);
        maxCollision--;
        if (maxCollision == 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == TargetBCell)
        {
            TargetBCell.GetComponent<EffectorBCellController>().crazy = true;
            Destroy(gameObject);
        }
    }

    private void SelectTarget()
    {
        GameObject[] Bcells = GameObject.FindGameObjectsWithTag(Tag).Where(obj => obj.activeSelf).ToArray();

        if (Bcells.Length > 0)
        {
            TargetBCell = Bcells[UnityEngine.Random.Range(0, Bcells.Length)];
        }
    }
}
