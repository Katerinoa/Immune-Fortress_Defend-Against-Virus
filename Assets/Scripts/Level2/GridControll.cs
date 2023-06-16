/*
*  UI——点击可种植区域可生成巨噬细胞或消炎因子
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridControll : MonoBehaviour
{
    public GameObject cellprefab;                //消炎因子的预制体
    public GameObject macrophageprefab;          //巨噬细胞的预制体 
    private bool isSet = false;                  //当前位置是否已经放置了细胞

    public static string moneyhint = "";         //提示语


    // 鼠标点击区域则可以判断是否添加
    private void OnMouseDown()
    {
        //若当前位置未添加过，可添加
        if (isSet == false)
        {
            isSet = true;

            //购买消炎因子
            if (TabCreatControll.currentObject == 1)
            {
                //判断当前体力是否足够，不够则撤销
                if (Core2.NowMoney < 30)
                {
                    moneyhint = "Your Amount is Insufficient.";  //给出提示语
                    Destroy(TabCreatControll.followmouseprefab);
                    TabCreatControll.currentObject = -1;
                    Invoke("HintCancel", 2.0f);
                }
                else //体力足够就生成
                {
                    Core2.NowMoney -= 30;
                    GameObject gripobj = Instantiate(cellprefab, this.transform.position, this.transform.rotation);
                    TabCreatControll.currentObject = -1;
                    Destroy(TabCreatControll.followmouseprefab);
                }
            }
            //购买巨噬细胞
            else if (TabCreatControll.currentObject == 4)
            {
                //判断当前体力是否足够，不够则撤销
                if (Core2.NowMoney < 50)
                {
                    moneyhint = "Your Amount is Insufficient.";
                    Destroy(TabCreatControll.followmouseprefab);
                    TabCreatControll.currentObject = -1;
                    Invoke("HintCancel", 2.0f);
                }
                else  //体力足够就生成
                {
                    Core2.NowMoney -= 50;
                    GameObject gripobj = Instantiate(macrophageprefab, this.transform.position + Vector3.up * 1.5f, this.transform.rotation);
                    gripobj.GetComponent<AttackUI>().enabled = true;
                    TabCreatControll.currentObject = -1;
                    Destroy(TabCreatControll.followmouseprefab);
                }
            }

        }
    }

    //关闭提示语
    void HintCancel()
    {
        moneyhint = "";
    }
}
