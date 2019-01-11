using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


[NetworkSettings(channel = 0, sendInterval = 0.1f)]   //设置网络频到，发送间隔
public class Speaker : NetworkBehaviour
{

    DataShow dataShow;

    LookAtMe lookAtMe;

    DataPanel dataPanel;

    private float fieldofView = 60f;
    public override void OnDeserialize(NetworkReader reader, bool initialState)
    {
        base.OnDeserialize(reader, initialState);
    }
    // Use this for initialization
    void Start()
    {
        lookAtMe = GameObject.Find("DataTag/AirConditioner").GetComponent<LookAtMe>();

        dataPanel = GameObject.Find("Canvas/DataChart").GetComponent<DataPanel>();

        dataShow = GameObject.Find("Canvas").GetComponent<DataShow>();
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
    }

    #region 巡游按钮
    [Command]
    void CmdAutoPlayIn()
    {
        dataShow.CruiseLandspaceStart();
    }
    [Command]
    void CmdAutoPlayOut()
    {
        dataShow.CruiseLandspaceStop();
    }
    #endregion

    #region 位置按钮
    [Command]
    void CmdClickPositionIn()
    {
        dataShow.ShowPositionIn();
    }
    [Command]
    void CmdClickPositionOut()
    {
        dataShow.ShowPositionOut();
    }
    #endregion

    #region 主页按钮
    [Command]
    void CmdClickHomeIn()
    {
        dataShow.DataLogoIn();
    }
    [Command]
    void CmdClickHomeOut()
    {
        dataShow.DataLogoOut();
    }
    #endregion


    #region 介绍面板
    [Command]
    void CmdIntroduceIn()
    {
        dataShow.IntroduceIn();
    }
    [Command]
    void CmdIntroduceOut()
    {
        dataShow.IntroduceOut();
    }
    #endregion

    #region 7个数据图点位
    [Command]
    void CmdClickChargeIn()
    {
        lookAtMe.ClickChargePile();

    }
    [Command]
    void CmdClickChargeOut()
    {
        dataPanel.ChageDataOut();

    }

    [Command]
    void CmdClickPowerIn()
    {
        lookAtMe.ClickPower();

    }
    [Command]
    void CmdClickPowerOut()
    {
        dataPanel.PowerDataOut();

    }

    [Command]
    void CmdClickCarPortIn()
    {
        lookAtMe.ClickCarPort();
    }
    [Command]
    void CmdClickCarPortOut()
    {
        dataPanel.CarPortDataOut();
    }
    [Command]
    void CmdClickPhotovoltaticWallIn()
    {
        lookAtMe.ClickPhotovoltaticWall();
    }
    [Command]
    void CmdClickPhotovoltaticWallOut()
    {
        dataPanel.WallDataOut();
    }
    [Command]
    void CmdClickPhotovoltaticRoofIn()
    {
        lookAtMe.ClickPhotovoltaticRoof();
    }
    [Command]
    void CmdClickPhotovoltaticRoofOut()
    {
        dataPanel.PhotovoltaicDataOut();
    }
    [Command]
    void CmdClickAirconditionIn()
    {
        lookAtMe.ClickAircondition();
    }
    [Command]
    void CmdClickAirconditionOut()
    {
        dataPanel.AirconditionerDataOut();
    }
    [Command]
    void CmdClickStorageIn()
    {
        lookAtMe.ClickStorage();
    }
    [Command]
    void CmdClickStorageOut()
    {
        dataPanel.StorageDataOut();
    }
    #endregion

    #region 位置同步
    [Command]  //客户端执行，服务端调用
    void CmdSendServerPos(Vector3 pos, Quaternion rotate)
    {
        Camera.main.GetComponent<Transform>().position = pos;
        Camera.main.GetComponent<Transform>().rotation = rotate;
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

    //void SendButtonCallback()
    //{
    //    if (!isLocalPlayer)
    //    {
    //        return;
    //    }
    //    if (inputBox.text.Length > 0)   //如果发送内容长度大于0
    //    {
    //        //发送内容格式为，当前玩家的IP地址：新一行，发送内容
    //        string str = string.Format("{0}:{1}{2}", Network.player.ipAddress, System.Environment.NewLine, inputBox.text);

    //        CmdSend(str);   //调用发送内容函数    

    //        inputBox.text = string.Empty;//发完清空发送输入框
    //    }
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
    //} // Update is called once per frame
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

    #region 背景音乐
    static bool isbgm;
    [Command]
    void CmdBGMPlay()
    {
        dataShow.BGMON();
    }
    [Command]
    void CmdBGMPause()
    {
        dataShow.BGMOFF();
    }
    #endregion
}
