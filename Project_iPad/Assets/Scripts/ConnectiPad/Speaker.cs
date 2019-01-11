using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[NetworkSettings(channel = 0, sendInterval = 0.1f)]   //设置网络频到，发送间隔
public class Speaker : NetworkBehaviour
{

    //DetialPosition detialPosition;

    //DataShow dataShow;

    //LookAtMe lookAtMe;

    //DataPanel dataPanel;

    iPadButtonManager iPadButtonList;

    private Button zoomINButton;

    private Button zoomOutButton;

    private Button charge;

    private Button power;

    private Button carPort;

    private Button photovoltaicWall;

    private Button photovoltaicRoof;

    private Button aircondition;

    private Button storage;

    private Button autoPlay;

    private Button set;

    private Button introduce;

    private Button position;

    private Button home;

    private Button BGM;

    public static bool syncCamera = true; //同步是否开相机同步flag，默认为false，当PC端传过来true才为true

    Text loading1, loading2, loading3;

    //登陆界面
    private GameObject login;
    NetworkHud showConnent;

    // [SyncVar]  //同步在线人数变量
    // private int onlineNum = 0;

    [SyncVar]  //同步放大缩小
    private float fieldofView = 60f;

    public override void OnDeserialize(NetworkReader reader, bool initialState)
    {
        base.OnDeserialize(reader, initialState);
    }
    //private void OnEnable()
    //{
    //    Debug.Log("SpeakerisCharge" + isCharge);
    //    Debug.Log("SpeakerisPower" + isPower);
    //    Debug.Log("SpeakerisCarPort" + isCarPort);
    //    Debug.Log("SpeakerisAircondition" + isAircondition);
    //    Debug.Log("SpeakerisPhotovoltaticRoof" + isPhotovoltaticRoof);
    //    Debug.Log("SpeakerisPhotovoltaticWall" + isPhotovoltaticWall);
    //    Debug.Log("SpeakerisStorage" + isStorage);
    //    Debug.Log("SpeakersyncCamera" + syncCamera);
    //}
    // Use this for initialization
    void Start()
    {
        loading1 = GameObject.Find("Canvas/loading1").GetComponent<Text>();
        loading2 = GameObject.Find("Canvas/loading2").GetComponent<Text>();
        loading3 = GameObject.Find("Canvas/loading3").GetComponent<Text>();

        login = GameObject.Find("Canvas/ButtonManager/LogIn");

        showConnent = GameObject.Find("NetworkManager").GetComponent<NetworkHud>();

        //detialPosition = Camera.main.GetComponent<DetialPosition>();

        //dataShow = GameObject.Find("Canvas").GetComponent<DataShow>();

        //lookAtMe = new LookAtMe()/*GameObject.Find("DataTag/AirConditioner").GetComponent<LookAtMe>()*/;

        //dataPanel = new DataPanel()/*GameObject.Find("Canvas/DataChart").GetComponent<DataPanel>()*/;

        iPadButtonList = GameObject.Find("Canvas/ButtonManager").GetComponent<iPadButtonManager>();

        zoomINButton = GameObject.Find("Canvas/ButtonManager/ZoonInOff").GetComponent<Button>();

        zoomINButton.onClick.AddListener(OnClickZoomIN);    //监听放大按钮

        zoomOutButton = GameObject.Find("Canvas/ButtonManager/ZoonOutOff").GetComponent<Button>();

        zoomOutButton.onClick.AddListener(OnClickZoomOut);   //监听缩小按钮

        charge = GameObject.Find("Canvas/ButtonManager/ChargeOff").GetComponent<Button>();

        charge.onClick.AddListener(OnClickCharge);  //监听充电桩按钮

        power = GameObject.Find("Canvas/ButtonManager/PowerOff").GetComponent<Button>();

        power.onClick.AddListener(OnClickPower);  //监配电房按钮

        carPort = GameObject.Find("Canvas/ButtonManager/CarPortOff").GetComponent<Button>();

        carPort.onClick.AddListener(OnClickCarPort); //监听车棚按钮

        photovoltaicWall = GameObject.Find("Canvas/ButtonManager/PhotovoltaicWallOff").GetComponent<Button>();

        photovoltaicWall.onClick.AddListener(OnClickPhotovoltaicWall);  //监听光伏墙按钮

        photovoltaicRoof = GameObject.Find("Canvas/ButtonManager/PhotovoltaicRoofOff").GetComponent<Button>();

        photovoltaicRoof.onClick.AddListener(OnClickPhotovoltaicRoof);  //监听光伏屋顶按钮

        aircondition = GameObject.Find("Canvas/ButtonManager/AirconditionOff").GetComponent<Button>();

        aircondition.onClick.AddListener(OnClickAircondition);   //监听空调按钮

        storage = GameObject.Find("Canvas/ButtonManager/StorageOff").GetComponent<Button>();

        storage.onClick.AddListener(OnClickStorage);   //监听储能设备按钮

        autoPlay = GameObject.Find("Canvas/ButtonManager/AutoPlayOff").GetComponent<Button>();

        autoPlay.onClick.AddListener(OnClickAutoPlay);    //监听巡游动画按钮

        set = GameObject.Find("Canvas/ButtonManager/SetOff").GetComponent<Button>();

        set.onClick.AddListener(OnClickSet);    //监听设置链接按钮

        introduce = GameObject.Find("Canvas/ButtonManager/IntroduceOff").GetComponent<Button>();

        introduce.onClick.AddListener(OnClickIntroduce);    //监听设置链接按钮

        position = GameObject.Find("Canvas/ButtonManager/PositionOff").GetComponent<Button>();

        position.onClick.AddListener(OnClickPosition);    //监听设置链接按钮

        home = GameObject.Find("Canvas/ButtonManager/HomeOff").GetComponent<Button>();

        home.onClick.AddListener(OnClickHome);    //监听设置链接按钮

        //BGM = GameObject.Find("Canvas/ButtonManager/BGMOff").GetComponent<Button>();

        //BGM.onClick.AddListener(OnClickBGM);

    }

    void OnClickZoomIN()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (fieldofView > 3)
        {
            fieldofView -= 2;
        }
        else
        {
            return;
        }
        CmdView(fieldofView);

        iPadButtonList.OnClickZoon(11, true);     //按下放大按钮
    }
    void OnClickZoomOut()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (fieldofView < 80)
        {
            fieldofView += 2;
        }
        else
        {
            return;
        }
        CmdView(fieldofView);

        iPadButtonList.OnClickZoon(12, true);     //按下缩小按钮
    }
    [Command]
    void CmdAutoPlayIn()
    {
       // dataShow.CruiseLandspaceStart();
    }
    [Command]
    void CmdAutoPlayOut()
    {
       // dataShow.CruiseLandspaceStop();
    }
    bool isAutoPlay = false;
    void OnClickAutoPlay()
    {
        StartCoroutine("JudgeBack", 4);   //巡游按钮
    }

    bool isSet = true;
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
    #endregion
    void OnClickSet()
    {
        isPosition = false;
        isIntroduce = false;
        isHome = false;
        isAutoPlay = false;
        isSet = !isSet;

        if (isSet)
        {
            iPadButtonList.OnClickFunctionButton(13, isSet);     //按下设置按钮     没必要退出其他操作
            ShowLogin();
        }
        else
        {
            iPadButtonList.OnClickFunctionButton(13, isSet);
            HideLogin();
        }

    }

    [Command]
    void CmdIntroduceIn()
    {
       // dataShow.IntroduceIn();
    }
    [Command]
    void CmdIntroduceOut()
    {
       // dataShow.IntroduceOut();
    }
    bool isIntroduce = false;
    void OnClickIntroduce()
    {

        StartCoroutine("JudgeBack", 2);   //介绍按钮

    }

    [Command]
    void CmdClickPositionIn()
    {
       // detialPosition.BoolShowPosition();
    }
    [Command]
    void CmdClickPositionOut()
    {
       // detialPosition.BoolShowPosition();
    }
    bool isPosition = false;
    void OnClickPosition()
    {
        StartCoroutine("JudgeBack", 3);   //位置按钮

    }

    [Command]
    void CmdClickHomeIn()
    {
       // dataShow.DataLogoIn();
    }
    [Command]
    void CmdClickHomeOut()
    {
       // dataShow.DataLogoOut();
    }
    bool isHome = true;
    public void OnClickHome()
    {
        StartCoroutine("JudgeBack", 1);   //主页按钮
    }


    #region 位置同步

    [Command]  //客户端执行，服务端调用
    void CmdSendServerPos(Vector3 pos, Quaternion rotate)
    {
        Camera.main.GetComponent<Transform>().position = pos;
        Camera.main.GetComponent<Transform>().rotation = rotate;
    }

    private void Update()
    {
        if (!isLocalPlayer) { return; }

        if (syncCamera)
        {
            //如果是本地创建 则把数据更新至服务器
            CmdSendServerPos(/*CameraRotate.tr.position*/Camera.main.transform.position, Camera.main.transform.rotation);
        }
    }
    #endregion

    #region 放大缩小同步
    [Command]
    void CmdView(float f)
    {
        CameraRotate.fieldView = f;
        // Camera.main.fieldOfView = f;
        RpcSetView(f);
    }
    [ClientRpc]
    void RpcSetView(float f)
    {
        CameraRotate.fieldView = f;
    }

    #endregion

    #region 消息同步
    //[Command]   //客户端调用，服务端执行
    //void CmdSend(string str)
    //{
    //    RpcShowMessage(str);
    //}

    //[ClientRpc]  //服务端调用，客户端执行
    //void RpcShowMessage(string str)
    //{
    //    GameObject item = Instantiate(itemPrefab, content) as GameObject;

    //    item.GetComponentInChildren<Text>().text = str;
    //}
    #endregion


    #region 实时显示人数

    //private void OnGUI()
    //{
    //    if (!isLocalPlayer)
    //    {
    //        return;
    //    }
    //    GUI.Label(new Rect(new Vector2(10, 10), new Vector2(150, 50)), string.Format("在线人数{0}", onlineNum));
    //}


    //// Update is called once per frame
    //void Update()
    //{
    //    if (isServer)
    //    {
    //        onlineNum = NetworkManager.singleton.numPlayers;   //实时显示人数 
    //                                                           //服务器调用，客户端运行
    //                                                           //  RpcSendClientPos(CameraRotate.tr.position, CameraRotate.tr.rotation);
    //    }
    //}

    #endregion

    #region 7个点位的按钮逻辑

    [Command]
    void CmdClickChargeIn()
    {
        //控制PC端，这里面根本没有运行，只是iPad端调用该函数，PC端运行该函数内容
      //  lookAtMe.ClickChargePile();
    }
    [Command]
    void CmdClickChargeOut()
    {
       // dataPanel.ChageDataOut();
    }
    public static bool isCharge = false;  //和pc端同步状态
    void OnClickCharge()
    {
        //按下充电桩
        StartCoroutine("SevenLogic", 1);
    }

    [Command]
    void CmdClickPowerIn()
    {
       // lookAtMe.ClickPower();
    }
    [Command]
    void CmdClickPowerOut()
    {
       // dataPanel.PowerDataOut();
    }
    public static bool isPower = false; //和pc端同步状态
    void OnClickPower()
    {
        //按下配电房
        StartCoroutine("SevenLogic", 2);
    }
    [Command]
    void CmdClickCarPortIn()
    {
       // lookAtMe.ClickCarPort();
    }
    [Command]
    void CmdClickCarPortOut()
    {
       // dataPanel.CarPortDataOut();
    }
    public static bool isCarPort = false;//和pc端同步状态
    void OnClickCarPort()
    {
        //按下光伏车棚
        StartCoroutine("SevenLogic", 3);
    }
    [Command]
    void CmdClickPhotovoltaticWallIn()
    {
       // lookAtMe.ClickPhotovoltaticWall();
    }
    [Command]
    void CmdClickPhotovoltaticWallOut()
    {
       // dataPanel.WallDataOut();
    }
    public static bool isPhotovoltaticWall = false;//和pc端同步状态
    void OnClickPhotovoltaicWall()
    {
        //按下光伏幕墙
        StartCoroutine("SevenLogic", 4);
    }
    [Command]
    void CmdClickPhotovoltaticRoofIn()
    {
       // lookAtMe.ClickPhotovoltaticRoof();
    }
    [Command]
    void CmdClickPhotovoltaticRoofOut()
    {
       // dataPanel.PhotovoltaicDataOut();
    }
    public static bool isPhotovoltaticRoof = false;//和pc端同步状态
    void OnClickPhotovoltaicRoof()
    {
        //按下光伏屋顶
        StartCoroutine("SevenLogic", 5);
    }

    [Command]
    void CmdClickAirconditionIn()
    {
       // lookAtMe.ClickAircondition();
    }
    [Command]
    void CmdClickAirconditionOut()
    {
       // dataPanel.AirconditionerDataOut();
    }
    public static bool isAircondition = false;//和pc端同步状态
    void OnClickAircondition()
    {
        //按下空调
        StartCoroutine("SevenLogic", 6);
    }
    [Command]
    void CmdClickStorageIn()
    {
       // lookAtMe.ClickStorage();
    }
    [Command]
    void CmdClickStorageOut()
    {
       // dataPanel.StorageDataOut();
    }
    public static bool isStorage = false;//和pc端同步状态

    void OnClickStorage()
    {
        //按下储能设备
        StartCoroutine("SevenLogic", 7);
    }

    private IEnumerator SevenLogic(int num)
    {
        BoolEnable(false);
        if (num == 1)
        {
            if (isPower == true)
            {
                //  dataPanel.PowerDataOut();
                CmdClickPowerOut();
                yield return new WaitForSeconds(5f);

            }
            if (isCarPort == true)
            {
                CmdClickCarPortOut();
                // dataPanel.CarPortDataOut();
                yield return new WaitForSeconds(4f);

            }
            if (isPhotovoltaticWall == true)
            {
                //  dataPanel.WallDataOut();
                CmdClickPhotovoltaticWallOut();
                yield return new WaitForSeconds(4.7f);

            }
            if (isPhotovoltaticRoof == true)
            {
                //  dataPanel.PhotovoltaicDataOut();
                CmdClickPhotovoltaticRoofOut();
                yield return new WaitForSeconds(4f);

            }
            if (isAircondition == true)
            {
                CmdClickAirconditionOut();
                // dataPanel.AirconditionerDataOut();
                yield return new WaitForSeconds(7.3f);

            }

            if (isStorage == true)
            {
                //  dataPanel.StorageDataOut();
                CmdClickStorageOut();
                yield return new WaitForSeconds(6f);

            }

            isPower = false;
            isCarPort = false;
            isPhotovoltaticWall = false;
            isPhotovoltaticRoof = false;
            isAircondition = false;
            isStorage = false;

            isCharge = !isCharge;

            if (isCharge)
            {
                syncCamera = false;
                Camera.main.transform.position = new Vector3(-8f, 101f, 192f);
                Camera.main.transform.rotation = Quaternion.Euler(new Vector3(30f, 180f, 0f));
                Camera.main.transform.localScale = Vector3.one;
                iPadButtonList.OnClickButton(0, isCharge);     //按下充电桩按钮
                CmdClickChargeIn();
                //  lookAtMe.ClickChargePile();
                yield return new WaitForSeconds(3f);
            }
            else
            {
                iPadButtonList.OnClickButton(0, isCharge);
                CmdClickChargeOut();
                //  dataPanel.ChageDataOut();
                yield return new WaitForSeconds(4f);
                syncCamera = true;
            }

        }
        if (num == 2)
        {

            if (isCharge == true)
            {
                //  iPadButtonList.OnClickButton(0, isCharge);
                CmdClickChargeOut();
                // dataPanel.ChageDataOut();
                yield return new WaitForSeconds(4f);
            }
            if (isCarPort == true)
            {
                CmdClickCarPortOut();
                // dataPanel.CarPortDataOut();
                yield return new WaitForSeconds(4f);
            }
            if (isPhotovoltaticWall == true)
            {
                // dataPanel.WallDataOut();
                CmdClickPhotovoltaticWallOut();
                yield return new WaitForSeconds(4.7f);
            }
            if (isPhotovoltaticRoof == true)
            {
                // dataPanel.PhotovoltaicDataOut();
                CmdClickPhotovoltaticRoofOut();
                yield return new WaitForSeconds(4f);
            }
            if (isAircondition == true)
            {
                CmdClickAirconditionOut();
                // dataPanel.AirconditionerDataOut();
                yield return new WaitForSeconds(7.3f);
            }

            if (isStorage == true)
            {
                // dataPanel.StorageDataOut();
                CmdClickStorageOut();
                yield return new WaitForSeconds(6f);
            }

            isCarPort = false;
            isPhotovoltaticWall = false;
            isPhotovoltaticRoof = false;
            isAircondition = false;
            isStorage = false;
            isCharge = false;

            isPower = !isPower;

            if (isPower)
            {
                syncCamera = false;
                Camera.main.transform.position = new Vector3(-8f, 101f, 192f);
                Camera.main.transform.rotation = Quaternion.Euler(new Vector3(30f, 180f, 0f));
                Camera.main.transform.localScale = Vector3.one;
                iPadButtonList.OnClickButton(1, isPower);     //按下配电系统按钮
                CmdClickPowerIn();
                yield return new WaitForSeconds(4.7f);
            }
            else
            {
                iPadButtonList.OnClickButton(1, isPower);
                // dataPanel.PowerDataOut();
                CmdClickPowerOut();
                yield return new WaitForSeconds(4.7f);
                syncCamera = true;
            }

        }
        if (num == 3)
        {

            if (isCharge == true)
            {
                // iPadButtonList.OnClickButton(0, isCharge);
                CmdClickChargeOut();
                //  dataPanel.ChageDataOut();
                yield return new WaitForSeconds(4f);
            }
            if (isPower == true)
            {
                //  dataPanel.PowerDataOut();
                CmdClickPowerOut();
                yield return new WaitForSeconds(4.7f);
            }

            if (isPhotovoltaticWall == true)
            {
                //  dataPanel.WallDataOut();
                CmdClickPhotovoltaticWallOut();
                yield return new WaitForSeconds(4.7f);
            }
            if (isPhotovoltaticRoof == true)
            {
                // dataPanel.PhotovoltaicDataOut();
                CmdClickPhotovoltaticRoofOut();
                yield return new WaitForSeconds(4f);
            }
            if (isAircondition == true)
            {
                CmdClickAirconditionOut();
                //  dataPanel.AirconditionerDataOut();
                yield return new WaitForSeconds(7.3f);
            }

            if (isStorage == true)
            {
                //  dataPanel.StorageDataOut();
                CmdClickStorageOut();
                yield return new WaitForSeconds(6f);
            }

            isPower = false;
            isPhotovoltaticWall = false;
            isPhotovoltaticRoof = false;
            isAircondition = false;
            isStorage = false;
            isCharge = false;

            isCarPort = !isCarPort;

            if (isCarPort)
            {
                syncCamera = false;
                Camera.main.transform.position = new Vector3(-8f, 101f, 192f);
                Camera.main.transform.rotation = Quaternion.Euler(new Vector3(30f, 180f, 0f));
                Camera.main.transform.localScale = Vector3.one;
                iPadButtonList.OnClickButton(2, isCarPort);     //按下停车棚按钮
                CmdClickCarPortIn();
                yield return new WaitForSeconds(3f);
            }
            else
            {
                iPadButtonList.OnClickButton(2, isCarPort);
                CmdClickCarPortOut();
                //  dataPanel.CarPortDataOut();
                yield return new WaitForSeconds(4f);
                syncCamera = true;
            }

        }
        if (num == 4)
        {

            if (isCharge == true)
            {
                // iPadButtonList.OnClickButton(0, isCharge);
                CmdClickChargeOut();
                // dataPanel.ChageDataOut();
                yield return new WaitForSeconds(4f);
            }
            if (isPower == true)
            {
                //  dataPanel.PowerDataOut();
                CmdClickPowerOut();
                yield return new WaitForSeconds(4.7f);
            }
            if (isCarPort == true)
            {
                CmdClickCarPortOut();
                // dataPanel.CarPortDataOut();
                yield return new WaitForSeconds(4f);
            }

            if (isPhotovoltaticRoof == true)
            {
                //dataPanel.PhotovoltaicDataOut();
                CmdClickPhotovoltaticRoofOut();
                yield return new WaitForSeconds(4f);
            }
            if (isAircondition == true)
            {
                CmdClickAirconditionOut();
                //  dataPanel.AirconditionerDataOut();
                yield return new WaitForSeconds(7.3f);
            }

            if (isStorage == true)
            {
                // dataPanel.StorageDataOut();
                CmdClickStorageOut();
                yield return new WaitForSeconds(6f);
            }

            isPower = false;
            isCarPort = false;
            isPhotovoltaticRoof = false;
            isAircondition = false;
            isStorage = false;
            isCharge = false;

            isPhotovoltaticWall = !isPhotovoltaticWall;

            if (isPhotovoltaticWall)
            {
                syncCamera = false;
                Camera.main.transform.position = new Vector3(-8f, 101f, 192f);
                Camera.main.transform.rotation = Quaternion.Euler(new Vector3(30f, 180f, 0f));
                Camera.main.transform.localScale = Vector3.one;
                iPadButtonList.OnClickButton(3, isPhotovoltaticWall);//按下光伏墙按钮
                CmdClickPhotovoltaticWallIn();
                yield return new WaitForSeconds(4.7f);
            }
            else
            {
                iPadButtonList.OnClickButton(3, isPhotovoltaticWall);
                //  dataPanel.WallDataOut();
                CmdClickPhotovoltaticWallOut();
                yield return new WaitForSeconds(4.7f);
                syncCamera = true;
            }

        }
        if (num == 5)
        {

            if (isCharge == true)
            {
                // iPadButtonList.OnClickButton(0, isCharge);
                CmdClickChargeOut();
                //  dataPanel.ChageDataOut();
                yield return new WaitForSeconds(4f);
            }
            if (isPower == true)
            {
                //  dataPanel.PowerDataOut();
                CmdClickPowerOut();
                yield return new WaitForSeconds(4.7f);
            }
            if (isCarPort == true)
            {
                CmdClickCarPortOut();
                //  dataPanel.CarPortDataOut();
                yield return new WaitForSeconds(4f);
            }
            if (isPhotovoltaticWall == true)
            {
                // dataPanel.WallDataOut();
                CmdClickPhotovoltaticWallOut();
                yield return new WaitForSeconds(4.7f);
            }

            if (isAircondition == true)
            {
                CmdClickAirconditionOut();
                //  dataPanel.AirconditionerDataOut();
                yield return new WaitForSeconds(7.3f);
            }

            if (isStorage == true)
            {
                // dataPanel.StorageDataOut();
                CmdClickStorageOut();
                yield return new WaitForSeconds(6f);
            }
            isPower = false;
            isCarPort = false;
            isPhotovoltaticWall = false;
            isAircondition = false;
            isStorage = false;
            isCharge = false;
            isPhotovoltaticRoof = !isPhotovoltaticRoof;

            if (isPhotovoltaticRoof)
            {
                syncCamera = false;
                Camera.main.transform.position = new Vector3(-8f, 101f, 192f);
                Camera.main.transform.rotation = Quaternion.Euler(new Vector3(30f, 180f, 0f));
                Camera.main.transform.localScale = Vector3.one;
                iPadButtonList.OnClickButton(4, isPhotovoltaticRoof);     //按下光伏屋顶按钮
                CmdClickPhotovoltaticRoofIn();
                yield return new WaitForSeconds(3f);
            }
            else
            {
                iPadButtonList.OnClickButton(4, isPhotovoltaticRoof);
                // dataPanel.PhotovoltaicDataOut();
                CmdClickPhotovoltaticRoofOut();
                yield return new WaitForSeconds(4f);
                syncCamera = true;
            }

        }
        if (num == 6)
        {

            if (isCharge == true)
            {
                //iPadButtonList.OnClickButton(0, isCharge);
                CmdClickChargeOut();
                // dataPanel.ChageDataOut();
                yield return new WaitForSeconds(4f);
            }
            if (isPower == true)
            {
                //  dataPanel.PowerDataOut();
                CmdClickPowerOut();
                yield return new WaitForSeconds(4.7f);
            }
            if (isCarPort == true)
            {
                CmdClickCarPortOut();
                // dataPanel.CarPortDataOut();
                yield return new WaitForSeconds(4f);
            }
            if (isPhotovoltaticWall == true)
            {
                // dataPanel.WallDataOut();
                CmdClickPhotovoltaticWallOut();
                yield return new WaitForSeconds(4.7f);
            }
            if (isPhotovoltaticRoof == true)
            {
                // dataPanel.PhotovoltaicDataOut();
                CmdClickPhotovoltaticRoofOut();
                yield return new WaitForSeconds(4f);
            }

            if (isStorage == true)
            {
                // dataPanel.StorageDataOut();
                CmdClickStorageOut();
                yield return new WaitForSeconds(6f);
            }
            isPower = false;
            isCarPort = false;
            isPhotovoltaticWall = false;
            isPhotovoltaticRoof = false;
            isStorage = false;
            isCharge = false;
            isAircondition = !isAircondition;

            if (isAircondition)
            {
                syncCamera = false;
                Camera.main.transform.position = new Vector3(-8f, 101f, 192f);
                Camera.main.transform.rotation = Quaternion.Euler(new Vector3(30f, 180f, 0f));
                Camera.main.transform.localScale = Vector3.one;
                iPadButtonList.OnClickButton(5, isAircondition);     //按下空调房按钮
                CmdClickAirconditionIn();
                yield return new WaitForSeconds(7.3f);
            }
            else
            {
                iPadButtonList.OnClickButton(5, isAircondition);
                CmdClickAirconditionOut();
                // dataPanel.AirconditionerDataOut();
                yield return new WaitForSeconds(7.3f);
                syncCamera = true;
            }

        }
        if (num == 7)
        {

            if (isCharge == true)
            {
                // iPadButtonList.OnClickButton(0, isCharge);
                CmdClickChargeOut();
                //  dataPanel.ChageDataOut();
                yield return new WaitForSeconds(4f);
            }
            if (isPower == true)
            {
                //  dataPanel.PowerDataOut();
                CmdClickPowerOut();
                yield return new WaitForSeconds(4.75f);
            }
            if (isCarPort == true)
            {
                CmdClickCarPortOut();
                // dataPanel.CarPortDataOut();
                yield return new WaitForSeconds(4f);
            }
            if (isPhotovoltaticWall == true)
            {
                //  dataPanel.WallDataOut();
                CmdClickPhotovoltaticWallOut();
                yield return new WaitForSeconds(4.75f);
            }
            if (isPhotovoltaticRoof == true)
            {
                // dataPanel.PhotovoltaicDataOut();
                CmdClickPhotovoltaticRoofOut();
                yield return new WaitForSeconds(4f);
            }
            if (isAircondition == true)
            {
                CmdClickAirconditionOut();
                // dataPanel.AirconditionerDataOut();
                yield return new WaitForSeconds(7.35f);
            }

            isPower = false;
            isCarPort = false;
            isPhotovoltaticWall = false;
            isPhotovoltaticRoof = false;
            isAircondition = false;
            isCharge = false;
            isStorage = !isStorage;

            if (isStorage)
            {
                syncCamera = false;
                Camera.main.transform.position = new Vector3(-8f, 101f, 192f);
                Camera.main.transform.rotation = Quaternion.Euler(new Vector3(30f, 180f, 0f));
                Camera.main.transform.localScale = Vector3.one;
                iPadButtonList.OnClickButton(6, isStorage);     //按下储能系统按钮
                CmdClickStorageIn();
                yield return new WaitForSeconds(6f);
            }
            else
            {
                iPadButtonList.OnClickButton(6, isStorage);
                // dataPanel.StorageDataOut();
                CmdClickStorageOut();
                yield return new WaitForSeconds(6f);
                syncCamera = true;
            }

        }
        BoolEnable(true);

    }

    //动画播放未完成按钮不能按
    void BoolEnable(bool enable)
    {
        //7个按钮
        charge.enabled = enable;
        power.enabled = enable;
        carPort.enabled = enable;
        photovoltaicWall.enabled = enable;
        photovoltaicRoof.enabled = enable;
        aircondition.enabled = enable;
        storage.enabled = enable;

        //功能按钮
        zoomINButton.enabled = enable;
        zoomOutButton.enabled = enable;
        autoPlay.enabled = enable;
        introduce.enabled = enable;
        position.enabled = enable;
        home.enabled = enable;
        if (enable == false)
        {
            StartCoroutine("LoadingTip");
        }
        if (enable)
        {
            StopCoroutine("LoadingTip");
            loading1.enabled = false;
            loading2.enabled = false;
            loading3.enabled = false;
        }
    }
    #endregion


    #region 功能按钮逻辑

    public IEnumerator JudgeBack(int functionNum)
    {

        if (isCharge)
        {
            StartCoroutine("SevenLogic", 1); yield return new WaitForSeconds(4f);
        }
        if (isPower)
        {
            StartCoroutine("SevenLogic", 2); yield return new WaitForSeconds(5f);
        }
        if (isCarPort)
        {
            StartCoroutine("SevenLogic", 3); yield return new WaitForSeconds(4f);
        }
        if (isPhotovoltaticWall)
        {
            StartCoroutine("SevenLogic", 4); yield return new WaitForSeconds(5f);
        }
        if (isPhotovoltaticRoof)
        {
            StartCoroutine("SevenLogic", 5); yield return new WaitForSeconds(4f);
        }
        if (isAircondition)
        {
            StartCoroutine("SevenLogic", 6); yield return new WaitForSeconds(8f);
        }
        if (isStorage)
        {
            StartCoroutine("SevenLogic", 7); yield return new WaitForSeconds(6.5f);
        }


        if (functionNum == 1)  //按下主页按钮
        {
            isPosition = false;
            isIntroduce = false;
            isHome = !isHome;
            if (isHome)
            {

                iPadButtonList.OnClickHideButton();//向下隐藏iPad端7个按钮
                                                   //7个按钮
                charge.enabled = false;
                power.enabled = false;
                carPort.enabled = false;
                photovoltaicWall.enabled = false;
                photovoltaicRoof.enabled = false;
                aircondition.enabled = false;
                storage.enabled = false;
                CmdClickHomeIn();
                iPadButtonList.OnClickFunctionButton(7, isHome);
                if (isAutoPlay)
                {
                    isAutoPlay = false;
                    yield return new WaitForSeconds(3f);
                    syncCamera = true;
                }
            }
            else
            {

                CmdClickHomeOut();
                iPadButtonList.OnClickFunctionButton(7, isHome);
            }
        }
        if (functionNum == 2)
        {
            isPosition = false;
            isHome = false;
            isIntroduce = !isIntroduce;
            if (isIntroduce)
            {

                iPadButtonList.OnClickHideButton();//向下隐藏iPad端7个按钮
                //7个按钮
                charge.enabled = false;
                power.enabled = false;
                carPort.enabled = false;
                photovoltaicWall.enabled = false;
                photovoltaicRoof.enabled = false;
                aircondition.enabled = false;
                storage.enabled = false;
                CmdIntroduceIn();
                iPadButtonList.OnClickFunctionButton(9, isIntroduce);     //按下介绍按钮
                if (isAutoPlay)
                {
                    isAutoPlay = false;
                    yield return new WaitForSeconds(3f);
                    syncCamera = true;
                }
            }
            else
            {

                CmdIntroduceOut();
                iPadButtonList.OnClickFunctionButton(9, isIntroduce);
            }
        }

        if (functionNum == 3)
        {
            isIntroduce = false;
            isHome = false;
            isPosition = !isPosition;
            if (isPosition)
            {
                iPadButtonList.OnClickShowButton();  //显示iPad7个按钮
                CmdClickPositionIn();
                iPadButtonList.OnClickFunctionButton(8, isPosition);     //按下位置按钮
                if (isAutoPlay)
                {
                    isAutoPlay = false;
                    yield return new WaitForSeconds(3f);
                    syncCamera = true;
                    //7个按钮
                    charge.enabled = true;
                    power.enabled = true;
                    carPort.enabled = true;
                    photovoltaicWall.enabled = true;
                    photovoltaicRoof.enabled = true;
                    aircondition.enabled = true;
                    storage.enabled = true;
                }
                else
                {
                    //7个按钮
                    charge.enabled = true;
                    power.enabled = true;
                    carPort.enabled = true;
                    photovoltaicWall.enabled = true;
                    photovoltaicRoof.enabled = true;
                    aircondition.enabled = true;
                    storage.enabled = true;
                }
            }
            else
            {

                iPadButtonList.OnClickHideButton();
                //7个按钮
                charge.enabled = false;
                power.enabled = false;
                carPort.enabled = false;
                photovoltaicWall.enabled = false;
                photovoltaicRoof.enabled = false;
                aircondition.enabled = false;
                storage.enabled = false;
                CmdClickPositionOut();
                iPadButtonList.OnClickFunctionButton(8, isPosition);
            }
        }
        if (functionNum == 4)
        {
            isPosition = false;
            isIntroduce = false;
            isHome = false;
            isAutoPlay = !isAutoPlay;

            if (isAutoPlay)
            {
                syncCamera = false;
                Camera.main.transform.position = new Vector3(-8f, 101f, 192f);
                Camera.main.transform.rotation = Quaternion.Euler(new Vector3(30f, 180f, 0f));
                Camera.main.transform.localScale = Vector3.one;
                iPadButtonList.OnClickHideButton();//向下隐藏iPad端7个按钮
                //7个按钮
                charge.enabled = false;
                power.enabled = false;
                carPort.enabled = false;
                photovoltaicWall.enabled = false;
                photovoltaicRoof.enabled = false;
                aircondition.enabled = false;
                storage.enabled = false;
                CmdAutoPlayIn();
                iPadButtonList.OnClickFunctionButton(10, isAutoPlay);     //按下巡游按钮
            }
            else
            {
                CmdAutoPlayOut();
                iPadButtonList.OnClickFunctionButton(10, isAutoPlay);
                yield return new WaitForSeconds(4f);
                syncCamera = true;    //相机同步
            }
        }
    }

    #endregion

    #region loading跳转
    IEnumerator LoadingTip()
    {
        while (true)
        {
            loading1.enabled = true;
            loading2.enabled = false;
            loading3.enabled = false;
            yield return new WaitForSeconds(0.5f);
            loading1.enabled = false;
            loading2.enabled = true;
            loading3.enabled = false;
            yield return new WaitForSeconds(0.5f);
            loading1.enabled = false;
            loading2.enabled = false;
            loading3.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }

    }
    #endregion

    #region 背景音乐
    //static bool isbgm;
    //[Command]
    //void CmdBGMPlay()
    //{
     
    //}
    //[Command]
    //void CmdBGMPause()
    //{
    
    //}
    //void OnClickBGM()
    //{
    //    if (isbgm)
    //    {
    //        CmdBGMPlay();
    //        iPadButtonList.OnClickBGM(14, isbgm);
    //        isbgm = false;
    //    }
    //    else
    //    {
    //        CmdBGMPause();
    //        iPadButtonList.OnClickBGM(14, isbgm);
    //        isbgm = true;
    //    }

    //}
    #endregion
    private void OnApplicationQuit()
    {
        StopAllCoroutines();

    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            return;
        }
        StopAllCoroutines();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            StopAllCoroutines();
        }

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
