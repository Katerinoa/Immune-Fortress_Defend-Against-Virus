/**
 * 改脚本用于控制淋巴因子朝着效应B细胞移动
 */
using System.Linq;
using UnityEngine;

public class LymphokineController : MonoBehaviour
{
    private string Tag = "BCell";           // 效应B细胞的标签
    public GameObject TargetBCell;          // 淋巴因子的目标B细胞
    public float moveSpeed = 10f;           // 淋巴因子的移动速度
    private Rigidbody rb;
    public int maxCollision = 2;            // 最大碰撞次数
    public float timer = 10f;               // 最长存在时间


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 选择目标效应B细胞
        if (TargetBCell == null)
        {
            SelectTarget();
        }
        else
        {
            // 计算前进方向
            Vector3 targetDirection = TargetBCell.transform.position - transform.position;
            // 未移动到终点
            if (targetDirection != Vector3.zero)
            {
                Vector3 direction = (TargetBCell.transform.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;    // 朝着终点加速度
            }
        }
        // 存在时间太长，淋巴因子消失
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 随机加力使淋巴因子能够继续移动
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        rb.AddForce(randomDirection);
        maxCollision--;
        // 碰撞次数过多
        if (maxCollision == 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        // 触发效应B细胞触发器时，让效应B细胞进入crazy状态
        if (collision.gameObject == TargetBCell)
        {
            TargetBCell.GetComponent<EffectorBCellController>().crazy = true;
            Destroy(gameObject);
        }
    }

    // 寻找目标效应B细胞
    private void SelectTarget()
    {
        // 找到在场景中且存活的效应B细胞
        GameObject[] Bcells = GameObject.FindGameObjectsWithTag(Tag).Where(obj => obj.activeSelf).ToArray();

        // 随机选择一个效应B细胞作为目标
        if (Bcells.Length > 0)
        {
            TargetBCell = Bcells[UnityEngine.Random.Range(0, Bcells.Length)];
        }
    }
}
