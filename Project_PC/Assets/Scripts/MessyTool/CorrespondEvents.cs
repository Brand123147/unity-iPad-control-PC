using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrespondEvents : MonoBehaviour {
    //数据面板
    DataPanel dataPanel;

    // Use this for initialization
    void Start () {
        dataPanel = GameObject.Find("DataChart").GetComponent<DataPanel>();
	}
	
    private void AirConditionEvent()
    {
        dataPanel.AirconditionerDataInfo();
    }

    private void ChargeEvent()
    {
        dataPanel.ChageDataInfo();
    }

    private void CarPortEvent()
    {
        dataPanel.CarPortDataInfo();
    }
    private void WallEvent()
    {
        dataPanel.WallDataInfo();
    }
    private void PhontovoltaicEvent()
    {
        dataPanel.PhotovoltaicDataInfo();
    }
    private void StorageEvent()
    {
        dataPanel.StorageDataInfo();
    }
    private void PowerEvent()
    {
        dataPanel.PowerDataInfo();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
