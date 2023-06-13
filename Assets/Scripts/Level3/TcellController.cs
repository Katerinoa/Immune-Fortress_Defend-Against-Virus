using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TcellController : MonoBehaviour
{
    public GameObject LymphokinePrefab;  // 淋巴因子预制体
    public GameObject EffectorTPrefab;  // 效应T细胞预制体
    private string Tag = "virus";
    public int LymphokineNum = 3;
    public float radius = 2.5f;
    public float IntervalTime = 5f;
    bool flag = false;             // 判断是否已经生成淋巴因子

    // Update is called once per frame
    void Update()
    {
        GameObject[] virus = GameObject.FindGameObjectsWithTag(Tag).Where(obj => obj.activeSelf).ToArray();
        if (virus.Length != 0)
        {
            if (flag == false)
            {
                for (int i = 0; i < LymphokineNum; ++ i)
                {
                    Vector3 position = transform.position + Random.insideUnitSphere * radius;
                    GameObject Lymphokine = Instantiate(LymphokinePrefab, position, Quaternion.identity);
                }
                flag = true;
            }
            StartCoroutine(generate(IntervalTime));
        }
    }

    IEnumerator generate(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject EffectorT = Instantiate(EffectorTPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
