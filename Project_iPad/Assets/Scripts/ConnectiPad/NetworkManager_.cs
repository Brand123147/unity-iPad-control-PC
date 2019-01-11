using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkManager_ : NetworkManager
{

    Text errorText;
    Text tipText;
    GameObject logIN;
    float timer = 0;

    bool connect2;
    bool isCharge_;
    bool isPower_;
    bool isCarport_;
    bool isPhotovoltaticWall_;
    bool isPhotovoltaticRoof_;
    bool isAircondition_;
    bool isStorage_;
    bool syncCamera_;

    private void Start()
    {

        errorText = GameObject.Find("Canvas/ButtonManager/LogIn/Text").GetComponent<Text>();
        tipText = GameObject.Find("Canvas/ButtonManager/LogIn/Text1").GetComponent<Text>();
        logIN = GameObject.Find("Canvas/ButtonManager/LogIn");
        errorText.text = "";
        tipText.text = "";
    }
    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        errorText.text = "";
        tipText.text = "";
        logIN.SetActive(true);
        errorText.text = "哎呀！网络故障了了了！";
        tipText.text = "请重新连接/重启程序";
        base.OnClientError(conn, errorCode);
    }

    // 当断开服务器时调用  
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        connect2 = true;
        MemoryState(Speaker.isCharge,
            Speaker.isPower,
            Speaker.isCarPort,
            Speaker.isPhotovoltaticWall,
            Speaker.isPhotovoltaticRoof,
            Speaker.isAircondition,
            Speaker.isStorage,
            Speaker.syncCamera);
        //断线自动重连

        StopClient();
        StartClient();

    }

    //void ShowTips()
    //{
    //    errorText.text = "";
    //    tipText.text = "";
    //    logIN.SetActive(true);
    //    errorText.text = "哎呀！主机断开了";
    //    tipText.text = "请重新连接";
    //}

    // 当连接到服务器时调用  

    public override void OnClientConnect(NetworkConnection conn)
    {
        ClientScene.Ready(conn);
        ClientScene.AddPlayer(0);
        errorText.text = "";
        tipText.text = "";
        if (connect2)
        {
            ReturnState(isCharge_, 
                isPower_, 
                isCarport_,
                isPhotovoltaticWall_,
                isPhotovoltaticRoof_,
                isAircondition_,
                isStorage_,
                syncCamera_);
            connect2 = false;   
        }
    }

    #region 存储状态与赋值状态
    void MemoryState(
        bool isCharge,
        bool isPower,
        bool isCarport,
        bool isPhotovoltaticWall,
        bool isPhotovoltaticRoof,
        bool isAircondition,
        bool isStorage,
        bool syncCamera)
    {
        isCharge_ = isCharge;
        isPower_ = isPower;
        isCarport_ = isCarport;
        isPhotovoltaticWall_ = isPhotovoltaticWall;
        isPhotovoltaticRoof_ = isPhotovoltaticRoof;
        isAircondition_ = isAircondition;
        isStorage_ = isStorage;
        syncCamera_ = syncCamera;
        //Debug.Log("isCharge_" + isCharge_);
        //Debug.Log("isPower_" + isPower_);
        //Debug.Log("isCarport_" + isCarport_);
        //Debug.Log("isPhotovoltaticWall_" + isPhotovoltaticWall_);
        //Debug.Log("isPhotovoltaticRoof_" + isPhotovoltaticRoof_);
        //Debug.Log("isAircondition_" + isAircondition_);
        //Debug.Log("isStorage_" + isStorage_);
        //Debug.Log("syncCamera_" + syncCamera);
        //Debug.Log("存储成功！！！");
    }

    void ReturnState(
        bool isCharge_,
        bool isPower_,
        bool isCarport_,
        bool isPhotovoltaticWall_,
        bool isPhotovoltaticRoof_,
        bool isAircondition_,
        bool isStorage_,
        bool syncCamera_)
    {
        Speaker.isCharge = isCharge_;
        Speaker.isPower = isPower_;
        Speaker.isCarPort = isCarport_;
        Speaker.isPhotovoltaticWall = isPhotovoltaticWall_;
        Speaker.isPhotovoltaticRoof = isPhotovoltaticRoof_;
        Speaker.isAircondition = isAircondition_;
        Speaker.isStorage = isStorage_;
        Speaker.syncCamera = syncCamera_;
        //Debug.Log("赋值成功！！！");
        //Debug.Log("isCharge_" + Speaker.isCharge);
        //Debug.Log("isPower_" + Speaker.isPower);
        //Debug.Log("isCarport_" + Speaker.isCarPort);
        //Debug.Log("isPhotovoltaticWall_" + Speaker.isPhotovoltaticWall);
        //Debug.Log("isPhotovoltaticRoof_" + Speaker.isPhotovoltaticRoof);
        //Debug.Log("isAircondition_" + Speaker.isAircondition);
        //Debug.Log("isStorage_" + Speaker.isStorage);
        //Debug.Log("syncCamera_" + Speaker.syncCamera);
    }
    #endregion
    private void OnApplicationQuit()
    {
        StopClient();
    }
}
