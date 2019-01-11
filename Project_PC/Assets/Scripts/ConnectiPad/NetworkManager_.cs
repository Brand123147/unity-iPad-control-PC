using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager_ : NetworkManager {

    //建立连接时调用
    //public override void OnServerConnect(NetworkConnection conn)
    //{
    //    Debug.Log("哎呀，连上了");
    //    base.OnServerConnect(conn);
    //}


    //客户端掉线是调用
    public override void OnServerDisconnect(NetworkConnection conn)
    {
       // Debug.Log("哎呀掉线了");
        NetworkServer.DestroyPlayersForConnection(conn);
    }

    //从服务器移除player时调用
    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
    {
       // Debug.Log("扔掉");
        base.OnServerRemovePlayer(conn, player);
    }

    //当客户端添加player时，双方都添加
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {     
        base.OnServerAddPlayer(conn, playerControllerId);

    }
}
