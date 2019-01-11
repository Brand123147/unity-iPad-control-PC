using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveEnergyData : MonoBehaviour {

    ReceiveData receiveData = new ReceiveData();
    float timer = 60f;

    Text photovoltaic1, photovoltaic2, photovoltaic3, photovoltaic4, photovoltaic5, photovoltaic6;
    Text chargePile7, chargePile8, chargePile9;
    Text powerSys10, powerSys11, powerSys12, powerSys13;
    Text storageSys14, storageSys15;

    // Use this for initialization
    private void Start()
    {
        photovoltaic1 = GameObject.Find("Canvas/Imageleft/ImageData/Text1").GetComponent<Text>();
        photovoltaic2 = GameObject.Find("Canvas/Imageleft/ImageData/Text2").GetComponent<Text>();
        photovoltaic3 = GameObject.Find("Canvas/Imageleft/ImageData/Text3").GetComponent<Text>();
        photovoltaic4 = GameObject.Find("Canvas/Imageleft/ImageData/Text4").GetComponent<Text>();
        photovoltaic5 = GameObject.Find("Canvas/Imageleft/ImageData/Text5").GetComponent<Text>();
        photovoltaic6 = GameObject.Find("Canvas/Imageleft/ImageData/Text6").GetComponent<Text>();
        chargePile7 = GameObject.Find("Canvas/Imageleft/ImageData/Text7").GetComponent<Text>();
        chargePile8 = GameObject.Find("Canvas/Imageleft/ImageData/Text8").GetComponent<Text>();
        chargePile9 = GameObject.Find("Canvas/Imageleft/ImageData/Text9").GetComponent<Text>();
        powerSys10 = GameObject.Find("Canvas/Imageleft/ImageData/Text10").GetComponent<Text>();
        powerSys11 = GameObject.Find("Canvas/Imageleft/ImageData/Text11").GetComponent<Text>();
        powerSys12 = GameObject.Find("Canvas/Imageleft/ImageData/Text12").GetComponent<Text>();
        powerSys13 = GameObject.Find("Canvas/Imageleft/ImageData/Text13").GetComponent<Text>();
        storageSys14 = GameObject.Find("Canvas/Imageleft/ImageData/Text14").GetComponent<Text>();
        storageSys15 = GameObject.Find("Canvas/Imageleft/ImageData/Text15").GetComponent<Text>();
        DrawData();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer<0)
        {
            timer = 60f;
            DrawData();
        }
    }
    void DrawData () {
        try
        {
            transform.Find("Text1").GetComponent<Text>().text = receiveData.ReturnData("saveEnergy", "totalCo") + "吨";
            transform.Find("Text2").GetComponent<Text>().text = receiveData.ReturnData("saveEnergy", "totalCoal") + "吨";
            transform.Find("Text3").GetComponent<Text>().text = receiveData.ReturnData("saveEnergy", "saveTrees") + "棵";

            photovoltaic1.text = receiveData.ReturnData("photovoltaicStationfile", "powerCapacity");
            photovoltaic2.text = receiveData.ReturnData("photovoltaicStationfile", "wayOfAccess");
            photovoltaic3.text = receiveData.ReturnData("photovoltaicStationfile", "numberOfInverter");
            photovoltaic4.text = receiveData.ReturnData("photovoltaicStationfile", "roof");
            photovoltaic5.text = receiveData.ReturnData("photovoltaicStationfile", "curtainWall");
            photovoltaic6.text = receiveData.ReturnData("photovoltaicStationfile", "carport");
            chargePile7.text = receiveData.ReturnData("chargePileFile", "amountOfChargePile") + "台";
            chargePile8.text = receiveData.ReturnData("chargePileFile", "powerOfChargePile") + "kW";
            chargePile9.text = receiveData.ReturnData("chargePileFile", "typeOfChargePile");
            powerSys10.text = receiveData.ReturnData("powerDistriFile", "capacityOfStation") + "kVA";
            powerSys11.text = receiveData.ReturnData("powerDistriFile", "amountOfTransformer") + "台";
            powerSys12.text = receiveData.ReturnData("powerDistriFile", "highPowerDistributionCabinet") + "面";
            powerSys13.text = receiveData.ReturnData("powerDistriFile", "lowPowerDistributionCabinet") + "面";
            storageSys14.text = receiveData.ReturnData("energyStoreFile", "capacity") + "kW / " + receiveData.ReturnData("energyStoreFile", "electricQuantity") + "kWh";
            storageSys15.text = receiveData.ReturnData("energyStoreFile", "amountOfStorageCabinet") + "面";
        }
        catch (System.Exception)
        {
            transform.Find("Text1").GetComponent<Text>().text = "获取失败";
            transform.Find("Text2").GetComponent<Text>().text = "获取失败";
            transform.Find("Text3").GetComponent<Text>().text = "获取失败";
            photovoltaic1.text = "获取失败";
            photovoltaic2.text = "获取失败";
            photovoltaic3.text = "获取失败";
            photovoltaic4.text = "获取失败";
            photovoltaic5.text = "获取失败";
            photovoltaic6.text = "获取失败";
            chargePile7.text = "获取失败";
            chargePile8.text = "获取失败";
            chargePile9.text = "获取失败";
            powerSys10.text = "获取失败";
            powerSys11.text = "获取失败";
            powerSys12.text = "获取失败";
            powerSys13.text = "获取失败";
            storageSys14.text = "获取失败";
            storageSys15.text = "获取失败";
        }
   
    }
	
}
