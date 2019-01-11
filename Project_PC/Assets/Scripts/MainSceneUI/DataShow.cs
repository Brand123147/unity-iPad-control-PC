using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Networking;
using HighlightingSystem;

public class DataShow : MonoBehaviour
{
    public AudioSource BGM;
    static bool isbgm;
    public bool boolInput = true; //是否让键盘输入
    static bool showPosition = false;
    public GameObject DataPosition;
    static bool isData = true;
    static bool isIntroduce = false;
    Transform Imageleft;
    Transform Imageright;
    Transform Logo;
    Transform Introduce;
    IntroduceEffect effect;

    //界面逻辑
    DataPanel dataPanel;

    //景观漫游

    public GameObject mainCamera;
    public GameObject cameraManager;
    public GameObject MCDefaultTarget;
    bool isCruise = false;
    float timer = 0;

    //扫光特效
    public ShaderEffect roofLight, wallLight, carPortLight;
    //充电桩动画
    private Animation ChargeAni;
    //空调特效
    public GameObject whiteSmoke, light1, light2, light3;

    //登陆界面
    private GameObject login;
    NetworkHud showConnent;

    // Use this for initialization
    void Start()
    {
        dataPanel = GameObject.Find("Canvas/DataChart").GetComponent<DataPanel>();
        Imageleft = transform.Find("Imageleft");
        Imageright = transform.Find("Imageright");
        Logo = transform.Find("Logo");
        Introduce = transform.Find("Introduce");
        ChargeAni = GameObject.Find("MainScence/chongdianzhuang (3)").GetComponent<Animation>();
        login = transform.Find("LogIn").gameObject;
        showConnent = GameObject.Find("NetworkManager").GetComponent<NetworkHud>();

        effect = GetComponent<IntroduceEffect>();

        Tweener tLeft = Imageleft.DOLocalMoveX(-1130f, 0.5f);
        Tweener tRight = Imageright.DOLocalMoveX(1130f, 0.5f);
        Tweener logo = Logo.DOLocalMoveY(600f, 0.5f);
        logo.SetAutoKill(false);
        tLeft.SetAutoKill(false);
        tRight.SetAutoKill(false);
        tLeft.Pause();
        tRight.Pause();
        logo.Pause();

    }
    // Update is called once per frame
    void Update()
    {
        OnClickLogin();   //登陆
      //  BoolPlayBGM();   //背景音乐
        timer += Time.deltaTime;
        if (timer <= 6)
        {
            return;
        }
        else
        {
            timer = 10;
            if (boolInput)
            {
                OnClickDataShow();   //主页
                OnClickIntroduceCtrl();  //介绍
                OnClickCruiseLandspace();  //巡游             
                BoolPressAlpha3();  //地标   
            }

        }

    }

    #region 数据主页logo显示

    public void OnClickDataShow()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (isData == false)
            {
                DataLogoOut();
            }
            else
            {
                DataLogoIn();
            }

        }
    }
    public void DataLogoOut()
    {
        if (isData == true) return;
        Imageleft.DOPlayForward();
        Imageright.DOPlayForward();
        Logo.DOPlayForward();
        //特效隐藏
        effect.CharacterOFF();
        isData = true;
    }
    public void DataLogoIn()
    {
        StartCoroutine(LogicUI(1));
    }
    #endregion

    #region 项目介绍
    IEnumerator DefaultPosition()
    {
        yield return new WaitForSeconds(0.75f);
        Introduce.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1940f, 0);
    }
    public void IntroduceIn()
    {
        StartCoroutine(LogicUI(2));
    }
    public void IntroduceOut()
    {
        if (isIntroduce == false) return;
        Introduce.DOLocalMoveX(1940f, 0.7f);
        StartCoroutine("DefaultPosition");
        isIntroduce = false;
        Introduce.Find("Button").GetComponent<Button>().enabled = false;

        //关闭特效
        //effect.TwinkleOFF();
        effect.CircleOFF();
    }
    public void OnClickIntroduceCtrl()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (isIntroduce == false)
            {
                IntroduceIn();
            }
            else
            {
                IntroduceOut();
            }
        }
    }
    #endregion

    #region 地点地标
    public void ShowPositionIn()
    {
        StartCoroutine(LogicUI(3));
    }
    public void ShowPositionOut()
    {
        if (showPosition == false) return;

        //隐藏
        showPosition = false;
        for (int i = 0; i < DataPosition.transform.childCount; i++)
        {
            DataPosition.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
            DataPosition.transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
        }


    }
    public void BoolShowPosition()
    {
        if (showPosition == false)
        {
            ShowPositionIn();
        }
        else
        {
            ShowPositionOut();
        }

    }

    public void BoolPressAlpha3()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BoolShowPosition();
        }
    }

    #endregion

    #region 景观漫游
    public void CruiseLandspaceStart()
    {
        StartCoroutine(LogicUI(4));
    }

    public void CruiseLandspaceStop()
    {
        if (isCruise == false) return;

        mainCamera.GetComponent<Animation>().Stop("xunyou");
        mainCamera.transform.DOMove(MCDefaultTarget.transform.position, 3f);
        mainCamera.transform.DORotate(MCDefaultTarget.transform.eulerAngles, 3f);

        //关闭所有扫光
        roofLight.enabled = false;
        wallLight.enabled = false;
        carPortLight.enabled = false;
        //关闭充电桩动画
        //ChargeAni.wrapMode = WrapMode.Default;
        //ChargeAni.Stop();
        //关闭空调特效
        whiteSmoke.SetActive(false);
        light1.GetComponent<AircondictionEffect>().enabled = false;
        light2.GetComponent<AircondictionEffect>().enabled = false;
        light3.GetComponent<AircondictionEffect>().enabled = false;
        //打开鼠标控制旋转
        mainCamera.GetComponent<CameraRotate>().enabled = true;
        isCruise = false;

        //关闭所有数据图
        mainCamera.GetComponent<XunYouData>().SendMessage("CarportOut");
        mainCamera.GetComponent<XunYouData>().SendMessage("PowerDataOut");
        mainCamera.GetComponent<XunYouData>().SendMessage("WallDataOut");
        mainCamera.GetComponent<XunYouData>().SendMessage("StorageDataOut");
        mainCamera.GetComponent<XunYouData>().SendMessage("PhotovoltaicDataOut");
        mainCamera.GetComponent<XunYouData>().SendMessage("ChageDataOut");
        mainCamera.GetComponent<XunYouData>().SendMessage("AirconditionerDataOut");

    }
    void OnClickCruiseLandspace()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (isCruise == false)
            {
                CruiseLandspaceStart();
            }
            else
            {
                CruiseLandspaceStop();
            }
        }
    }
    #endregion

    #region 登陆界面
    bool isShowLogin = false;
    public void ShowLogin()
    {
        login.SetActive(true);
        showConnent.showGUI = true;
    }
    public void HideLogin()
    {
        login.SetActive(false);
        showConnent.showGUI = false;
    }
    void OnClickLogin()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isShowLogin = !isShowLogin;
            if (isShowLogin)
            {
                ShowLogin();     //显示
            }
            else
            {
                HideLogin();   //隐藏
            }

        }
    }
    #endregion

    #region UI界面数据图逻辑
    public IEnumerator LogicUI(int num)
    {
        boolInput = false;
        if (dataPanel.isCharge)
        {
            dataPanel.ChageDataOut();
            yield return new WaitForSeconds(3.1f);
        }
        if (dataPanel.isCarport)
        {
            dataPanel.CarPortDataOut();
            yield return new WaitForSeconds(3.1f);

        }
        if (dataPanel.isPhotovoltaicWall)
        {
            dataPanel.WallDataOut();
            yield return new WaitForSeconds(4.8f);
        }
        if (dataPanel.isPhotovoltaticRoof)
        {
            dataPanel.PhotovoltaicDataOut();
            yield return new WaitForSeconds(3.1f);
        }
        if (dataPanel.isAircondition)
        {
            dataPanel.AirconditionerDataOut();
            yield return new WaitForSeconds(7.4f);
        }
        if (dataPanel.isStorage)
        {
            dataPanel.StorageDataOut();
            yield return new WaitForSeconds(5.5f);
        }
        if (dataPanel.isPower)
        {
            dataPanel.PowerDataOut();
            yield return new WaitForSeconds(4.8f);
        }


        if (num == 1)   //主页
        {
            Imageleft.DOPlayBackwards();
            Imageright.DOPlayBackwards();
            Logo.DOPlayBackwards();
            //特效显示
            effect.CharacterON();
            isData = false;
            //隐藏介绍页，地点，漫游
            IntroduceOut();
            ShowPositionOut();
            CruiseLandspaceStop();
        }
        if (num == 2)  //介绍面板
        {
            Introduce.DOLocalMoveX(0, 0.7f);
            isIntroduce = true;
            Introduce.Find("Button").GetComponent<Button>().enabled = true;

            //特效出现
            effect.CircleON();

            //隐藏主页，地点，漫游
            DataLogoOut();
            ShowPositionOut();
            CruiseLandspaceStop();
        }
        if (num == 3)  //数据地标
        {
            //显示
            showPosition = true;
            for (int i = 0; i < DataPosition.transform.childCount; i++)
            {
                DataPosition.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                DataPosition.transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
            }
            //隐藏介绍页，主页，漫游
            IntroduceOut();
            DataLogoOut();
            CruiseLandspaceStop();
        }
        if (num == 4)   //巡游
        {
            isCruise = true;
            mainCamera.GetComponent<Camera>().fieldOfView = 60f;
            mainCamera.GetComponent<Animation>().Play("xunyou");

            //开启所有扫光
            roofLight.enabled = true;
            wallLight.enabled = true;
            carPortLight.enabled = true;
            //开启充电桩动画
            //ChargeAni.Play();
            //ChargeAni.wrapMode = WrapMode.Loop;
            //开启空调特效
            whiteSmoke.SetActive(true);
            light1.GetComponent<AircondictionEffect>().enabled = true;
            light2.GetComponent<AircondictionEffect>().enabled = true;
            light3.GetComponent<AircondictionEffect>().enabled = true;

            //关闭鼠标控制旋转
            mainCamera.GetComponent<CameraRotate>().enabled = false;

            //隐藏主页，地点，介绍页
            IntroduceOut();
            ShowPositionOut();
            DataLogoOut();
        }
        boolInput = true;
    }
    #endregion

    #region 背景音乐

   public void BGMON()
    {
        BGM.Play();
    }
   public void BGMOFF()
    {
        BGM.Pause();
    }
    void BoolPlayBGM()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (isbgm)
            {
                BGMON();
            }
            else
            {
                BGMOFF();
            }
            isbgm = !isbgm;
        }
    }
    #endregion
}
