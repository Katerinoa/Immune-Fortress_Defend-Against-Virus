/*
*  病毒机制——寻路与伤害、死亡、入侵
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControll : MonoBehaviour
{

    public Material watermaterial;      //机体材质
    private NavMeshAgent agent;         //寻路组件
    private GameObject target;          //终点
    private Vector3 tarplace;           //终点位置
    public Vector3 virusposition;       //病毒当前位置
    // Start is called before the first frame update
    private Hpcontrol hpcontrol;
    void Start()
    {
        
        target = GameObject.Find("destination");

        //寻路系统
        agent = GetComponent<NavMeshAgent>();
        tarplace = target.transform.position;
        tarplace.y = 1.0f;
        agent.SetDestination(tarplace);

        //病毒血条
        hpcontrol = GetComponent<Hpcontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        virusposition = this.gameObject.transform.position;
        Debug.Log(agent.destination - agent.nextPosition);
        //若病毒到达终点，此时机体变红，提示被病毒入侵
        if ((Mathf.Abs(agent.destination.x - agent.nextPosition.x) <= 0.5f)
        && (Mathf.Abs(agent.destination.y - agent.nextPosition.y) <= 0.5f)
        && (Mathf.Abs(agent.destination.z - agent.nextPosition.z) <= 0.5f))
        {
            Debug.Log("stop");
            watermaterial.color += Color.red;
            Destroy(this.gameObject);
        }

    }

    //病毒受到子弹攻击，若血条为0，则死亡
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "bullet")
        {
            hpcontrol.nowHp -= Core2.DamageValue;
            if (hpcontrol.nowHp == 0)
            {
                Destroy(this.gameObject);
                Core2.DestroyVirusNum = Core2.DestroyVirusNum + 1;
                Debug.Log("当前消灭敌人数量： " + Core2.DestroyVirusNum);
            }
        }
    }
}
