using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Networking.Match;
namespace UnityEngine.Networking
{
    [EditorBrowsable(EditorBrowsableState.Never), AddComponentMenu("Network/NetworkManagerHUD"), RequireComponent(typeof(NetworkManager))]
    public class NetworkHud : MonoBehaviour
    {
        [SerializeField] bool showClient = false;
        [SerializeField] bool clientReady = false;
        [SerializeField] bool showServerOnly = false;
        [SerializeField] bool showEnableMatchMaker = false;
        [SerializeField] bool showClientConnect;
        [SerializeField] Texture2D buttonBG;
        [SerializeField] GUISkin manualGUISkin;


        public NetworkManager manager;
        [SerializeField]
        public bool showGUI = true;
        [SerializeField]
        public int offsetX = 1000;
        [SerializeField]
        public int offsetY = 700;
        private bool m_ShowServer;

        private void Awake()
        {
            this.manager = base.GetComponent<NetworkManager>();
        }

        private void Start()
        {
            manager.StartServer();
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
        }
        private void OnGUI()
        {
            #region 字体风格
            GUI.skin = manualGUISkin;
            GUIStyle fontStyle1 = new GUIStyle();
            fontStyle1.alignment = TextAnchor.MiddleCenter;
            fontStyle1.normal.textColor = new Color(0, 0, 0);
            fontStyle1.fontSize = 25;


            GUIStyle fontStyle2 = new GUIStyle();
            fontStyle2.alignment = TextAnchor.MiddleCenter;
            fontStyle2.normal.textColor = new Color(0, 0, 0);
            fontStyle2.fontSize = 30;
            fontStyle2.normal.background = buttonBG;
            #endregion



            if (!this.showGUI)
            {
                return;
            }
            int num = 10 + this.offsetX;
            int num2 = 40 + this.offsetY;
            bool flag = this.manager.client == null || this.manager.client.connection == null || this.manager.client.connection.connectionId == -1;
            if (!this.manager.IsClientConnected() && !NetworkServer.active && this.manager.matchMaker == null)
            {
                if (flag)
                {
                    if (showServerOnly)
                    {
                        if (Application.platform != RuntimePlatform.WebGLPlayer)
                        {
                            if (GUI.Button(new Rect((float)num + 890, (float)num2 + 674, 150f, 50f), "Connect", fontStyle2))
                            {
                                this.manager.StartHost();
                            }
                            num2 += 24;
                        }
                    }
                    if (showClient)
                    {
                        if (GUI.Button(new Rect((float)num, (float)num2, 200f, 70f), "连接", fontStyle2))
                        {
                            this.manager.StartClient();
                        }
                    }

                    //接收IP地址输入
                    this.manager.networkAddress = GUI.TextField(new Rect((float)(num) + 670, (float)num2 + 394, 560, 158), this.manager.networkAddress, fontStyle1);
                    num2 += 24;
                    if (Application.platform == RuntimePlatform.WebGLPlayer)
                    {
                        GUI.Box(new Rect((float)num, (float)num2, 200f, 25f), "(  WebGL cannot be server  )");
                        num2 += 24;
                    }
                    else
                    {

                        if (GUI.Button(new Rect((float)num + 890, (float)num2 + 650, 150f, 50f), "create", fontStyle2))
                        {
                            this.manager.StartServer();
                        }
                        num2 += 24;

                    }
                }
                else
                {
                    GUI.Label(new Rect((float)num + 670, (float)num2 + 370, 560, 158), string.Concat(new object[]
                    {
                        "Connecting to",
                        this.manager.networkAddress,
                        ":",
                        this.manager.networkPort,
                        ".."
                    }));
                    num2 += 24;
                    if (GUI.Button(new Rect((float)num + 670, (float)num2 + 370, 560, 158), "Cancel Connection Attempt"))
                    {
                        this.manager.StopClient();
                    }
                }
            }
            else
            {
                if (NetworkServer.active)
                {
                    string text = "ServerIP:" + this.manager.networkAddress + "    port:" + this.manager.networkPort;
                    if (this.manager.useWebSockets)
                    {
                        text += " (Using WebSockets)";
                    }
                    GUI.Label(new Rect((float)num + 670, (float)num2 + 395, 560, 158f), text, fontStyle1);
                    num2 += 24;
                }
                if (showClientConnect)
                {
                    //修改隐藏
                    if (this.manager.IsClientConnected())
                    {
                        GUI.Label(new Rect((float)num - 150, (float)num2 - 200, 600f, 200f), string.Concat(new object[]
                        {
                        "连接成功\n",
                        "Client: address=",
                        this.manager.networkAddress,
                        "\nport=",
                        this.manager.networkPort
                        }), fontStyle1);    //设置字体
                        num2 += 24;
                    }
                }

            }

            if (clientReady)
            {
                if (this.manager.IsClientConnected() && !ClientScene.ready)
                {
                    if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Client Ready"))
                    {
                        ClientScene.Ready(this.manager.client.connection);
                        if (ClientScene.localPlayers.Count == 0)
                        {
                            ClientScene.AddPlayer(0);
                        }
                    }
                    num2 += 24;
                }
            }

            if (NetworkServer.active || this.manager.IsClientConnected())
            {
                if (GUI.Button(new Rect((float)num + 860, (float)num2 + 650, 200f, 50f), "Disconnect", fontStyle2))
                {
                    this.manager.StopHost();
                }
                num2 += 24;
            }
            if (!NetworkServer.active && !this.manager.IsClientConnected() && flag)
            {
                num2 += 10;
                if (Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    GUI.Box(new Rect((float)(num - 5), (float)num2, 220f, 25f), "(WebGL cannot use Match Maker)");
                    return;
                }
                if (this.manager.matchMaker == null)
                {
                    if (showEnableMatchMaker)
                    {
                        if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Enable Match Maker (M)"))
                        {
                            this.manager.StartMatchMaker();
                        }
                        num2 += 24;
                    }

                }
                else
                {
                    if (this.manager.matchInfo == null)
                    {
                        if (this.manager.matches == null)
                        {
                            if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Create Internet Match"))
                            {
                                this.manager.matchMaker.CreateMatch(this.manager.matchName, this.manager.matchSize, true, string.Empty, string.Empty, string.Empty, 0, 0, new NetworkMatch.DataResponseDelegate<MatchInfo>(this.manager.OnMatchCreate));
                            }
                            num2 += 24;
                            GUI.Label(new Rect((float)num, (float)num2, 100f, 20f), "Room Name:");
                            this.manager.matchName = GUI.TextField(new Rect((float)(num + 100), (float)num2, 100f, 20f), this.manager.matchName);
                            num2 += 24;
                            num2 += 10;
                            if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Find Internet Match"))
                            {
                                this.manager.matchMaker.ListMatches(0, 20, string.Empty, false, 0, 0, new NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>>(this.manager.OnMatchList));
                            }
                            num2 += 24;
                        }
                        else
                        {
                            for (int i = 0; i < this.manager.matches.Count; i++)
                            {
                                MatchInfoSnapshot matchInfoSnapshot = this.manager.matches[i];
                                if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Join Match:" + matchInfoSnapshot.name))
                                {
                                    this.manager.matchName = matchInfoSnapshot.name;
                                    this.manager.matchMaker.JoinMatch(matchInfoSnapshot.networkId, string.Empty, string.Empty, string.Empty, 0, 0, new NetworkMatch.DataResponseDelegate<MatchInfo>(this.manager.OnMatchJoined));
                                }
                                num2 += 24;
                            }
                            if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Back to Match Menu"))
                            {
                                this.manager.matches = null;
                            }
                            num2 += 24;
                        }
                    }
                    if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Change MM server"))
                    {
                        this.m_ShowServer = !this.m_ShowServer;
                    }
                    if (this.m_ShowServer)
                    {
                        num2 += 24;
                        if (GUI.Button(new Rect((float)num, (float)num2, 100f, 20f), "Local"))
                        {
                            this.manager.SetMatchHost("localhost", 1337, false);
                            this.m_ShowServer = false;
                        }
                        num2 += 24;
                        if (GUI.Button(new Rect((float)num, (float)num2, 100f, 20f), "Internet"))
                        {
                            this.manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
                            this.m_ShowServer = false;
                        }
                        num2 += 24;
                        if (GUI.Button(new Rect((float)num, (float)num2, 100f, 20f), "Staging"))
                        {
                            this.manager.SetMatchHost("staging-mm.unet.unity3d.com", 443, true);
                            this.m_ShowServer = false;
                        }
                    }
                    num2 += 24;
                    GUI.Label(new Rect((float)num, (float)num2, 300f, 20f), "MM Uri: " + this.manager.matchMaker.baseUri);
                    num2 += 24;
                    if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Disable Match Maker"))
                    {
                        this.manager.StopMatchMaker();
                    }
                    num2 += 24;
                }
            }
        }
    }
}
