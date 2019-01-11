using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HighlightingSystem;

public class LookAtMe : MonoBehaviour
{
    AnimatorCtrl animationclip;

    private GameObject mainCamera;
    private Transform child;   
    private Animation chongdianzhuang;
    private Animation chongdianzhuang2;
    private Animation chongdianzhuang3;

    private Transform CDZTarget;
    private Transform chepeng;
    private Transform qiang;
    private Transform guangfuban;
    private Transform kongtiao;
    private Transform peidian;
    private Transform chuNeng;
    private Transform Target;

    //发光特效
    SpecialEffect effect;

    //地点标签
    DataShow dataShow;

    //数据面板
    DataPanel dataPanel;
    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main.gameObject;
        CDZTarget = GameObject.FindWithTag("CDZTarget").transform;
        chongdianzhuang = GameObject.FindWithTag("chongdianzhuang").GetComponent<Animation>();
        chongdianzhuang2 = GameObject.FindWithTag("chongdianzhuang2").GetComponent<Animation>();
        chongdianzhuang3 = GameObject.FindWithTag("chongdianzhuang3").GetComponent<Animation>();
        chepeng = GameObject.Find("chepeng").transform;
        qiang = GameObject.Find("qiang").transform;
        guangfuban = GameObject.Find("guangfuban").transform;
        kongtiao = GameObject.Find("kongtiao").transform;
        peidian = GameObject.Find("peidian").transform;
        chuNeng = GameObject.Find("chuNeng").transform;
        Target = GameObject.Find("Target").transform;
        dataShow = GameObject.Find("Canvas").GetComponent<DataShow>();
        dataPanel = GameObject.Find("DataChart").GetComponent<DataPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera.transform);
        transform.localEulerAngles = new Vector3(-transform.localEulerAngles.x, transform.localEulerAngles.y + 180, transform.localEulerAngles.z);
    }
      
    //按充电桩地标
    IEnumerator OnClickCharge()
    {      
        dataShow.boolInput = false;

        JumpTo(CDZTarget);

        StartCoroutine(PlayWhichData("ChargingPile"));

        StartCoroutine(PlayAnimation());  //播放充电桩抬起动画

        yield return new WaitForSecondsRealtime(3f);

        dataShow.boolInput = true;
    }
    public void ClickChargePile()
    {
        StartCoroutine("OnClickCharge");
    }

    //按空调地标
    IEnumerator OnClickAirconditon()
    {
        dataShow.boolInput = false;
        JumpTo(kongtiao);

        StartCoroutine(PlayAnimaton("kongtiao"));

        //播放发光特效
        effect = GameObject.Find("th1/pl11111/zhulou (1)/ktf1").GetComponent<SpecialEffect>();
        foreach (var item in effect.airconditionEffect)
        {
            item.GetComponent<AircondictionEffect>().enabled = true;
        }

        yield return new WaitForSecondsRealtime(7.3f);
        dataShow.boolInput = true;
    }
    public void ClickAircondition()
    {
        StartCoroutine("OnClickAirconditon");
    }

    //按储能地标
    IEnumerator OnClickStorage()
    {
        dataShow.boolInput = false;
        JumpTo(chuNeng);

        StartCoroutine(PlayAnimaton("chuneng"));
        yield return new WaitForSecondsRealtime(5.4f);
        dataShow.boolInput = true; 
    }
    public void ClickStorage()
    {
        StartCoroutine("OnClickStorage");
    }

    //按配电地标
    IEnumerator OnClickPower()
    {
        
        dataShow.boolInput = false;

        JumpTo(peidian);

        StartCoroutine(PlayAnimaton("peidian"));

        yield return new WaitForSecondsRealtime(4.7f);

        dataShow.boolInput = true;

    }
    public void ClickPower()
    {
        StartCoroutine("OnClickPower");
    }

    //按车棚地标
    IEnumerator OnClickCarport()
    {
        dataShow.boolInput = false;

        JumpTo( chepeng);

        StartCoroutine(PlayWhichData("Carport"));

        yield return new WaitForSecondsRealtime(3f);

        dataShow.boolInput = true; 
    }
    public void ClickCarPort()
    {
        StartCoroutine("OnClickCarport");
    }

    //按光伏屋顶地标
    IEnumerator OnClickPhotovoltaticRoof()
    {
        dataShow.boolInput = false; 

        JumpTo(guangfuban);

        StartCoroutine(PlayWhichData("GuangFuBan"));

        yield return new WaitForSecondsRealtime(3f);

        dataShow.boolInput = true;
    }
    public void ClickPhotovoltaticRoof()
    {
        StartCoroutine("OnClickPhotovoltaticRoof");
    }

    //按光伏幕墙地标
    IEnumerator OnClickPhotovoltaticWall()
    {
        dataShow.boolInput = false;

        JumpTo(qiang);

        StartCoroutine(PlayAnimaton("qiang"));

        yield return new WaitForSecondsRealtime(4.7f);

        dataShow.boolInput = true;
    }
    public void ClickPhotovoltaticWall()
    {
        StartCoroutine("OnClickPhotovoltaticWall");
    }
    private void OnMouseDown()
    {
        if (transform.name == "ChargingPile")
        {
            ClickChargePile();
        }
        if (transform.name == "AirConditioner")
        {
            ClickAircondition();
        }
        if (transform.name == "Battery")
        {
            ClickStorage();
        }
        if (transform.name == "Carport")
        {
            ClickCarPort();
        }
        if (transform.name == "GuangFuBan")
        {
            ClickPhotovoltaticRoof();
        }
        if (transform.name == "guangfuqiang")
        {
            ClickPhotovoltaticWall();
        }
        if (transform.name == "Power")
        {
            ClickPower();
        }
    }

    //跳转到指定位置
    private void JumpTo(Transform position)
    {
        //Debug.Log(transform.name);
        //地点标签消失
      
        dataShow.ShowPositionOut();

        mainCamera.GetComponent<Camera>().fieldOfView = 60f;
        mainCamera.transform.DOMove(position.position, 2.6f).SetEase(Ease.Linear);
        mainCamera.transform.DORotate(position.eulerAngles, 2.6f).SetEase(Ease.Linear);
        mainCamera.GetComponent<CameraRotate>().enabled = false;
    }

    IEnumerator PlayWhichData(string name)
    {
        yield return new WaitForSeconds(2f);

        if (name == "ChargingPile")
        {
            dataPanel.ChageDataInfo();
        }
        if (name== "Carport")
        {
            dataPanel.CarPortDataInfo();
        }
        if (name== "GuangFuBan")
        {
            dataPanel.PhotovoltaicDataInfo();
        }
    }

    //播放路径动画
    IEnumerator PlayAnimaton(string animationclip)
    {
        yield return new WaitForSeconds(2.5f);
        mainCamera.transform.SetParent(Target);
        mainCamera.GetComponent<Animation>()[animationclip].speed = 1;
        mainCamera.GetComponent<Animation>().Play(animationclip);
    }

    //播放充电桩动画
    IEnumerator PlayAnimation()
    {
        yield return new WaitForSecondsRealtime(4f);
        //chongdianzhuang.Play();
        //chongdianzhuang2.Play();
        chongdianzhuang3.Play();
    }
}
