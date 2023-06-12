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
    private Hpcontrol hpcontrol;
    void Start()
    {
        target = GameObject.Find("destination");

        //寻路系统
        agent = GetComponent<NavMeshAgent>();
        tarplace = target.transform.position;
        tarplace.y = 1.0f;
        agent.SetDestination(tarplace);

        hpcontrol = GetComponent<Hpcontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        virusposition = this.gameObject.transform.position;
    }

    //敌人死亡(到底为什么会触发两次)
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "bullet")
        {
            hpcontrol.nowHp -= Core2.DamageValue;
            Core2.DestroyVirusNum = Core2.DestroyVirusNum + 1;
            Debug.Log("当前消灭敌人数量： " + Core2.DestroyVirusNum);
        }
    }
}
