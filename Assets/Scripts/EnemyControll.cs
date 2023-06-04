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

    //敌人生成
    private void OnTriggerEnter(Collider other) {
        if(other.name.Substring(0) == "bullet")
        {
            
            // Debug.Log("病毒病毒病毒病毒病毒病毒病毒病毒病毒病毒病毒病毒病毒");
            //不知道要不要加动画
            blood = blood - 5;
            if(blood == 0)  Destroy(this.gameObject);
        }
    }
}
