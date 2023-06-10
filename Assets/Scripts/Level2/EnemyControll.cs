using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControll : MonoBehaviour
{

    NavMeshAgent agent;
    GameObject target;
    Vector3 tarplace;
    public Vector3 virusposition;
    // Start is called before the first frame update
    float blood = 100.0f;
    void Start()
    {
        target = GameObject.Find("destination");

        //寻路系统
        agent = GetComponent<NavMeshAgent>();
        tarplace = target.transform.position;
        tarplace.y = 1.0f;

        agent.SetDestination(tarplace);
    }

    // Update is called once per frame
    void Update()
    {
        virusposition = this.gameObject.transform.position;
    }

    //敌人死亡(到底为什么会触发两次)
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("子弹要碰到病毒了");
        //bool flag = false;
        if (other.name == "bullet")
        {
            //flag = true;
            // Debug.Log("病毒病毒病毒病毒病毒病毒病毒病毒病毒病毒病毒病毒病毒");
            //不知道要不要加动画
            blood = blood - Core2.DamageValue;
           // Debug.Log("blood: " + blood);
            if (blood == 0)
            {
                blood = -1;
                Core2.DestroyVirusNum = Core2.DestroyVirusNum + 1;
                Debug.Log("当前消灭敌人数量： " + Core2.DestroyVirusNum);
                Destroy(this.gameObject);
            //    GetComponent<EnemyControll>().enabled = false;
            }
        }
    }
}
