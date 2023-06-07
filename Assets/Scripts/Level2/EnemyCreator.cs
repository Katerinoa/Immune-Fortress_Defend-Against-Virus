using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCreator : MonoBehaviour
{
    public GameObject virusprefab;
    GameObject virus;
    
    // Start is called before the first frame update
    //自动生成敌人
    void Start()
    {
        StartCoroutine(creatvirus());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //生成
    IEnumerator creatvirus()
    {
        while(true)
        {
            yield return new WaitForSeconds(6f);
            virus = Instantiate(virusprefab,this.transform.position,this.transform.rotation,this.transform);
            //改名
            virus.name = "virus";
        }
    }
}
