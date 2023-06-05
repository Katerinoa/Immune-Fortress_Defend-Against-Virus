using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridControll : MonoBehaviour
{
    // public Button mpbutton;
    // public Button cellbutton;
    public GameObject macrophageprefab;
    bool isClick = false;
    bool isSet = false;
    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            isSet = true;
            GameObject gripobj = Instantiate(macrophageprefab,this.transform.position,this.transform.rotation);
        }
    }
}
