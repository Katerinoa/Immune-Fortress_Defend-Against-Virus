using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridControll : MonoBehaviour
{
    // public Button mpbutton;
    // public Button cellbutton;
    public GameObject cellprefab;
    public GameObject macrophageprefab;
    bool isSet = false;
    

    // 鼠标点击就添加
    private void OnMouseDown() {
        // if(!mpbutton.IsActive() && isSet == false)
        // {
        //     mpbutton.gameObject.SetActive(true);
        //     cellbutton.gameObject.SetActive(true);
        //     isOpen = true;
        //     //让界面放到这里来
        // }
        // else if(mpbutton.IsActive())
        // {
        //     mpbutton.gameObject.SetActive(false);
        //     cellbutton.gameObject.SetActive(false);
        //     isClick = false;
        // }
        if(isSet == false)
        {
            //Debug.Log("可以");
            isSet = true;
            if(TabCreatControll.currentObject == 1){
                  GameObject gripobj = Instantiate(cellprefab,this.transform.position,this.transform.rotation);
                  TabCreatControll.currentObject = -1;
                  Destroy(TabCreatControll.followmouseprefab);
            }
            else if(TabCreatControll.currentObject == 4)
            {
                GameObject gripobj = Instantiate(macrophageprefab,this.transform.position + Vector3.up * 1.5f,this.transform.rotation);
                gripobj.GetComponent<AttackUI>().enabled = true;
                TabCreatControll.currentObject = -1;
                Destroy(TabCreatControll.followmouseprefab);
            }

        }
    }
}
