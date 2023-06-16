using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControll : MonoBehaviour
{

    public Material watermaterial;
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
        Debug.Log(agent.destination - agent.nextPosition);
        if ((Mathf.Abs(agent.destination.x - agent.nextPosition.x) <= 0.5f)
        && (Mathf.Abs(agent.destination.y - agent.nextPosition.y) <= 0.5f)
        && (Mathf.Abs(agent.destination.z - agent.nextPosition.z) <= 0.5f))
        {
            Debug.Log("stop");
            watermaterial.color += Color.red;
            Destroy(this.gameObject);
        }

    }

    //敌人死亡
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "bullet")
        {
            hpcontrol.nowHp -= Core2.DamageValue;
            //这里不能直接就销毁，要血量小于0病毒才能死亡
            // Core2.DestroyVirusNum = Core2.DestroyVirusNum + 1;
            // Debug.Log("当前消灭敌人数量： " + Core2.DestroyVirusNum);
            if (hpcontrol.nowHp == 0)
            {
                Destroy(this.gameObject);
                Core2.DestroyVirusNum = Core2.DestroyVirusNum + 1;
                Debug.Log("当前消灭敌人数量： " + Core2.DestroyVirusNum);
            }
        }
    }
}
