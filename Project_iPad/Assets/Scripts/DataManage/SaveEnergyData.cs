using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveEnergyData : MonoBehaviour {

    ReceiveData receiveData = new ReceiveData();
    float timer = 600f;
    // Use this for initialization
    private void Start()
    {
        DrawData();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer<0)
        {
            timer = 600f;
            DrawData();
        }
    }
    void DrawData () {
        try
        {
            transform.Find("Text1").GetComponent<Text>().text = receiveData.ReturnData("saveEnergy", "totalCo") + "吨";
            transform.Find("Text2").GetComponent<Text>().text = receiveData.ReturnData("saveEnergy", "totalCoal") + "吨";
            transform.Find("Text3").GetComponent<Text>().text = receiveData.ReturnData("saveEnergy", "saveTrees") + "棵";
        }
        catch (System.Exception)
        {
            transform.Find("Text1").GetComponent<Text>().text = "获取失败";
            transform.Find("Text2").GetComponent<Text>().text = "获取失败";
            transform.Find("Text3").GetComponent<Text>().text = "获取失败";
        }
   
    }
	
}
