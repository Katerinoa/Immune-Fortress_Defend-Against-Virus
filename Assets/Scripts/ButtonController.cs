using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button mpbutton;
    public Button cellbutton;
    public GameObject macrophageprefab;
    public GameObject cellprefab;
    GameObject gripobj;

    public static bool isClick = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onclick()
    {
        Debug.Log("can");
    }

        public void ClickMP()
    {
        gripobj = Instantiate(macrophageprefab,this.transform.position,this.transform.rotation);
        isClick = true;
    }

    public void ClickCell()
    {
        gripobj = Instantiate(cellprefab,this.transform.position,this.transform.rotation);
        isClick = true;
    }
}
