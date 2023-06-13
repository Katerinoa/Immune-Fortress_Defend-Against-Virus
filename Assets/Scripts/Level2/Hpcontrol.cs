using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hpcontrol : MonoBehaviour
{
    // Start is called before the first frame update
    public float AllHp = 100.0f;
    public float nowHp;
    private Transform HpTransform;
    private Image HpImage;
    void Start()
    {
        HpTransform = this.gameObject.transform.Find("Hp");
        HpImage = HpTransform.Find("HpNow").gameObject.GetComponent<Image>();
        nowHp = AllHp;
        Debug.Log("起始的血量：" + nowHp);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 target = new Vector3(cameraPos.x, HpTransform.position.y, cameraPos.z);
        HpTransform.LookAt(target);
        //：关于destroy移到enemy那里去了
        // if (nowHp <= 0.0f)
        // {
        //     Destroy(this.gameObject);
        //     Core2.DestroyVirusNum = Core2.DestroyVirusNum + 1;
        //     Debug.Log("��ǰ������������� " + Core2.DestroyVirusNum);
        // }
        // Debug.Log(nowHp);
        float HpPercent = nowHp / AllHp;
        HpImage.fillAmount = HpPercent;
    }
}
