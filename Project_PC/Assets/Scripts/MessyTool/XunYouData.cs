using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class XunYouData : MonoBehaviour {
    private Transform CarPortData;
    private Transform PowerData;
    private Transform WallData;
    private Transform StorageData;
    private Transform PhotovoltaicData;
    private Transform ChageData;
    private Transform AirconditionerData;

	// Use this for initialization
	void Start () {

        CarPortData = GameObject.Find("Canvas/DataChart/CarPortData").transform;
        PowerData = GameObject.Find("Canvas/DataChart/PowerData").transform;
        WallData = GameObject.Find("Canvas/DataChart/WallData").transform;
        StorageData = GameObject.Find("Canvas/DataChart/StorageData").transform;
        PhotovoltaicData = GameObject.Find("Canvas/DataChart/PhotovoltaicData").transform;
        ChageData = GameObject.Find("Canvas/DataChart/ChageData").transform;
        AirconditionerData = GameObject.Find("Canvas/DataChart/AirconditionerData").transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region 车棚
    public void CarportIn()
    {
        CarPortData.DOLocalMoveX(-561f,0.5f);
    }
    public void CarportOut()
    {
        CarPortData.DOLocalMoveX(-1370f, 0.5f);
    }
    #endregion
    #region 配电系统
    public void PowerDataIn()
    {
        PowerData.DOLocalMoveX(-561f, 0.5f);
    }
    public void PowerDataOut()
    {
        PowerData.DOLocalMoveX(-1370f, 0.5f);
    }
    #endregion
    #region 光伏墙
    public void WallDataIn()
    {
        WallData.DOLocalMoveX(561f, 0.5f);
    }
    public void WallDataOut()
    {
        WallData.DOLocalMoveX(1370f, 0.5f);
    }
    #endregion
    #region 储能系统
    public void StorageDataIn()
    {
        StorageData.DOLocalMoveX(561f, 0.5f);
    }
    public void StorageDataOut()
    {
        StorageData.DOLocalMoveX(1370f, 0.5f);
    }
    #endregion
    #region 光伏屋顶
    public void PhotovoltaicDataIn()
    {
        PhotovoltaicData.DOLocalMoveX(-561f, 0.5f);
    }
    public void PhotovoltaicDataOut()
    {
        PhotovoltaicData.DOLocalMoveX(-1370f, 0.5f);
    }
    #endregion
    #region 充电桩
    public void ChageDataIn()
    {
        ChageData.DOLocalMoveX(561f, 0.5f);
    }
    public void ChageDataOut()
    {
        ChageData.DOLocalMoveX(1370f, 0.5f);
    }
    #endregion
    #region 空调
    public void AirconditionerDataIn()
    {
        AirconditionerData.DOLocalMoveX(561f, 0.5f);
    }
    public void AirconditionerDataOut()
    {
        AirconditionerData.DOLocalMoveX(1370f, 0.5f);
    }
    #endregion
}
