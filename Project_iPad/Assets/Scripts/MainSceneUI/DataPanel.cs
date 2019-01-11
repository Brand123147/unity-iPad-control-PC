using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//using HighlightingSystem;

public class DataPanel : MonoBehaviour
{

    public List<Transform> dataPanelPos;

    private GameObject mainCamera;
    private GameObject MCDefaultTarget;

    DetialPosition detialPosition;

    Transform CameraManager;

    //光伏板扫光特效
    SpecialEffect effect;
    public ShaderEffect roofLight;
    public ShaderEffect wallLight;
    public ShaderEffect carPortLight;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main.gameObject;
        MCDefaultTarget = GameObject.FindWithTag("MCDefaultTarget");
        CameraManager = GameObject.Find("CameraManager").transform;
    
    }

    //充电桩数据 0
    public void ChageDataInfo()
    {
        SetDoMoveX(dataPanelPos[0], 550);
    }
    public void ChageDataOut()
    {
        SetDoMoveX(dataPanelPos[0], 1370);
        ReturnDefault();
    }

    //车棚数据 1
    public void CarPortDataInfo()
    {
        SetDoMoveX(dataPanelPos[1], -550);

        //打开扫光特效
        carPortLight.enabled = true;
    }
    public void CarPortDataOut()
    {
        SetDoMoveX(dataPanelPos[1], -1370);
        ReturnDefault();

        //关闭扫光特效
        carPortLight.enabled = false;
    }

    //光伏墙数据 2
    public void WallDataInfo()
    {
        //打开扫光特效
        wallLight.enabled = true;
        SetDoMoveX(dataPanelPos[2], 550);
    }
    public void WallDataOut()
    {
        SetDoMoveX(dataPanelPos[2], 1370);
        PlayBack("qiang");
        Invoke("ReturnDefault", 1.7f);
        //关闭扫光特效
        wallLight.enabled = false;
    }

    //光伏板数据 3
    public void PhotovoltaicDataInfo()
    {
        //打开扫光特效
        roofLight.enabled = true;
        SetDoMoveX(dataPanelPos[3], -550);
    }
    public void PhotovoltaicDataOut()
    {
        SetDoMoveX(dataPanelPos[3], -1370);
        ReturnDefault();
        //关闭扫光特效
        roofLight.enabled = false;
    }

    //空调数据 4
    public void AirconditionerDataInfo()
    {
        SetDoMoveX(dataPanelPos[4], 550);
    }
    public void AirconditionerDataOut()
    {
        SetDoMoveX(dataPanelPos[4], 1370);
        PlayBack("kongtiao");
        Invoke("ReturnDefault", 4.3f);


        //关闭发光特效
        effect = GameObject.Find("ktf1").GetComponent<SpecialEffect>();

        foreach (var item in effect.airconditionEffect)
        {
            item.GetComponent<AircondictionEffect>().enabled = false;
        }
    }

    //储能设备数据 5
    public void StorageDataInfo()
    {
        SetDoMoveX(dataPanelPos[5], 550);
    }
    public void StorageDataOut()
    {
        SetDoMoveX(dataPanelPos[5], 1370);
        PlayBack("chuneng");
        Invoke("ReturnDefault", 2.4f);
    }

    //配电系统数据 6
    public void PowerDataInfo()
    {
        SetDoMoveX(dataPanelPos[6], -550);
    }
    public void PowerDataOut()
    {
        SetDoMoveX(dataPanelPos[6], -1370);
        PlayBack("peidian");
        Invoke("ReturnDefault", 1.7f);
    }


    public void SetDoMoveX(Transform transform, float xValue)
    {
        transform.DOLocalMoveX(xValue, 0.5f);
    }

    //相机归位
    private void PlayBack(string animationclip)
    {
        //倒播动画TODO，关掉动画TODO，相机父物体设置回来，归位相机
        mainCamera.GetComponent<Animation>()[animationclip].time = mainCamera.GetComponent<Animation>()[animationclip].clip.length - 0.5f;
        mainCamera.GetComponent<Animation>()[animationclip].speed = -1;
        mainCamera.GetComponent<Animation>().Play(animationclip);

    }
    private void ReturnDefault()
    {
        mainCamera.transform.SetParent(CameraManager);
        mainCamera.transform.DOMove(MCDefaultTarget.transform.position, 3f);
        mainCamera.transform.DORotate(MCDefaultTarget.transform.eulerAngles, 3f);

        StartCoroutine(ReturnShowTag());
    }

    IEnumerator ReturnShowTag()
    {
        yield return new WaitForSeconds(2.5f);
        //标签显示
        detialPosition = mainCamera.GetComponent<DetialPosition>();
        detialPosition.ShowPositionIn();
        mainCamera.GetComponent<CameraRotate>().enabled = true; //开启脚本
    }
    // Update is called once per frame
    void Update()
    {

    }


}
