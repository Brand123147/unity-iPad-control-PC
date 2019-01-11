using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
namespace UnityEngine.Networking
{
    [EditorBrowsable(EditorBrowsableState.Never), AddComponentMenu("Network/NetworkManagerHUD"), RequireComponent(typeof(NetworkManager))]
    public class NetworkHud_ : MonoBehaviour
    {
        [SerializeField] bool clientReady = false;
        [SerializeField] bool showServer = false;
        [SerializeField] bool showServerOnly = false;
        [SerializeField] bool showEnableMatchMaker = false;
        [SerializeField] Texture2D buttonBG;
        [SerializeField] GUISkin manualGuiSkin;


        public NetworkManager manager;
        [SerializeField]
        public bool showGUI = true;
        [SerializeField]
        public int offsetX = 1000;
        [SerializeField]
        public int offsetY = 700;
        private bool m_ShowServer;

        //修改用UI自适应分辨率
        Button loginBt;
        Text nameBt;
        Text tips;

        private void Awake()
        {
            loginBt = GameObject.Find("Canvas/ButtonManager/LogIn/Button").GetComponent<Button>();
            nameBt = GameObject.Find("Canvas/ButtonManager/LogIn/Button/Text").GetComponent<Text>();
            tips = GameObject.Find("Canvas/ButtonManager/LogIn/InputField/Text").GetComponent<Text>();
            this.manager = base.GetComponent<NetworkManager>();
            this.manager.networkAddress = "请输入PC端IP地址...";
        }
        private void Update()
        {
            if (!this.showGUI)
            {
                return;
            }

            #region 键盘控制

            //if (!this.manager.IsClientConnected() && !NetworkServer.active && this.manager.matchMaker == null)
            //{
            //    if (Application.platform != RuntimePlatform.WebGLPlayer)
            //    {
            //        if (Input.GetKeyDown(KeyCode.S))
            //        {
            //            this.manager.StartServer();
            //        }
            //        if (Input.GetKeyDown(KeyCode.H))
            //        {
            //            this.manager.StartHost();
            //        }
            //    }
            //    if (Input.GetKeyDown(KeyCode.C))
            //    {
            //        this.manager.StartClient();
            //    }
            //}

            #endregion

            if (NetworkServer.active && this.manager.IsClientConnected() && Input.GetKeyDown(KeyCode.X))
            {
                this.manager.StopHost();
            }

            //如果未连接则显示connect

        }

        public void StandOnGUI_ConnectBt()
        {

            if (!this.showGUI)
            {
                return;
            }
            bool flag = this.manager.client == null || this.manager.client.connection == null || this.manager.client.connection.connectionId == -1;
            if (!this.manager.IsClientConnected() && !NetworkServer.active && this.manager.matchMaker == null)
            {
                if (flag)
                {

                }
            }
        }
    }
}