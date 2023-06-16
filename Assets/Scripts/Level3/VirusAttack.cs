/** 
 * 该脚本主要用于控制病毒的侵染
 */
using UnityEngine;

public class VirusAttack : MonoBehaviour
{
    public ParticleSystem virusEffect; // 粒子系统

    private float timer = 0.0f; // 计时器
    private float infectTime; // 侵染时间
    private bool isInfecting = false; // 是否正在侵染

    private void Awake()
    {
        infectTime = Core_Level3.infectTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 碰到细胞
        if (other.gameObject.CompareTag("cell"))
        {
            GetComponent<VirusController_Level3>().isStopped = true;
            GetComponentInChildren<VirusBehaviour>().isStopped = true; //停止移动
            virusEffect.Play(); //打开粒子效果 
            isInfecting = true; //开始侵染
            timer = 0.0f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // 始终停留在病毒表面
        if (other.gameObject.CompareTag("cell"))
        {
            if (isInfecting)
            {
                timer += Time.deltaTime;

                if (timer >= infectTime)  // 到时间后病毒进入细胞
                {
                    // 设置病毒的状态 与其他脚本交互
                    isInfecting = false;
                    GetComponent<VirusController_Level3>().isStopped = false;
                    GetComponent<VirusController_Level3>().innerCell = true;
                    GetComponentInChildren<VirusBehaviour>().isStopped = false;
                    other.gameObject.GetComponent<CellAffected>().virusCount++;
                    gameObject.GetComponent<VirusAttack>().virusEffect.Stop();
                }
            }
        }
    }
}
