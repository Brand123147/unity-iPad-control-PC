using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using HighlightingSystem;

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
    DetialPosition detialPosition;

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

        dataPanel = GameObject.Find("DataChart").GetComponent<DataPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera.transform);
        transform.localEulerAngles = new Vector3(-transform.localEulerAngles.x, transform.localEulerAngles.y + 180, transform.localEulerAngles.z);
    }


    public void ClickChargePile()
    {
        JumpTo("ChargeData", CDZTarget);

        StartCoroutine(PlayWhichData("ChargingPile"));

        StartCoroutine(PlayAnimation());  //播放充电桩抬起动画
    }

    public void ClickAircondition()
    {
        JumpTo("AirData", kongtiao);

        StartCoroutine(PlayAnimaton("kongtiao"));

        //播放发光特效
        effect = GameObject.Find("th1/ktf1").GetComponent<SpecialEffect>();
        foreach (var item in effect.airconditionEffect)
        {
            item.GetComponent<AircondictionEffect>().enabled = true;
        }
    }
    public void ClickStorage()
    {
        JumpTo("BatteryData", chuNeng);

        StartCoroutine(PlayAnimaton("chuneng"));
    }
    public void ClickPower()
    {
        JumpTo("PowerData", peidian);

        StartCoroutine(PlayAnimaton("peidian"));
    }
    public void ClickCarPort()
    {
        JumpTo("CarportData", chepeng);

        StartCoroutine(PlayWhichData("Carport"));
    }
    public void ClickPhotovoltaticRoof()
    {
        JumpTo("GuangFuBanData", guangfuban);

        StartCoroutine(PlayWhichData("GuangFuBan"));
    }
    public void ClickPhotovoltaticWall()
    {
        JumpTo("guangfuqiangData", qiang);

        StartCoroutine(PlayAnimaton("qiang"));
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
    private void JumpTo(string place, Transform position)
    {
        //Debug.Log(transform.name);
        //地点标签消失
        detialPosition = mainCamera.GetComponent<DetialPosition>();
        detialPosition.ShowPositionOut();

        //设置主摄像机
        mainCamera.GetComponent<Camera>().fieldOfView = 60f;
        mainCamera.transform.DOMove(position.position, 2.5f);
        mainCamera.transform.DORotate(position.eulerAngles, 2.5f);
        mainCamera.GetComponent<CameraRotate>().enabled = false;
    }

    IEnumerator PlayWhichData(string name)
    {
        yield return new WaitForSeconds(2f);

        if (name == "ChargingPile")
        {
            dataPanel.ChageDataInfo();
        }
        if (name == "Carport")
        {
            dataPanel.CarPortDataInfo();
        }
        if (name == "GuangFuBan")
        {
            dataPanel.PhotovoltaicDataInfo();
        }
    }

    //播放路径动画
    IEnumerator PlayAnimaton(string animationclip)
    {
        yield return new WaitForSeconds(1.8f);
        mainCamera.transform.SetParent(Target);
        mainCamera.GetComponent<Animation>()[animationclip].speed = 1;
        mainCamera.GetComponent<Animation>().Play(animationclip);
    }

    //播放充电桩动画
    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(4f);
        chongdianzhuang.Play();
        chongdianzhuang2.Play();
        chongdianzhuang3.Play();
    }
}
