using UnityEngine;
using System.Collections.Generic;
using SpringGUI;
using UnityEngine.UI;

public class PieGraphExample : MonoBehaviour
{
    public PieGraph PieGraph = null;

    public PieGraph PieGraph1 = null;
    public PieGraph PieGraph2 = null;
    public PieGraph PieGraph3 = null;

    ReceiveData data = new ReceiveData();

    public Color co1 = new Color32(86,186,255,255);
    public Color co2 = new Color32(123,223,252,255);

    float timer = 60f;

    //private void Awake()
    //{
    //    DrawData();
    //}
    private void Update()
    {
        //十分钟更新一次数据
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 60f;
            DrawData();
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            return;
        }
        else
        {
            DrawData();
        }
    }
    private void DrawData()
    { 
        #region 今日用电
        float others = 0;
        float lighting = 0;
        float storedEnergy = 0;
        float power = 0;
        float charge = 0;
        float airConditioner = 0;
        others = data.DownloadData("usePowerRate", "others");
         lighting = data.DownloadData("usePowerRate", "lighting");
         storedEnergy = data.DownloadData("usePowerRate", "storedEnergy");
         power = data.DownloadData("usePowerRate", "power");
         charge = data.DownloadData("usePowerRate", "charge");
         airConditioner = data.DownloadData("usePowerRate", "airConditioner");
        #endregion

        #region 今日发电占比
        transform.Find("genePowerRate1").Find("Text").GetComponent<Text>().text = "";
        transform.Find("genePowerRate2").Find("Text").GetComponent<Text>().text = "";
        transform.Find("genePowerRate3").Find("Text").GetComponent<Text>().text = "";
        transform.Find("genePowerRate1").Find("Textkwh").GetComponent<Text>().text = "";
        transform.Find("genePowerRate2").Find("Textkwh").GetComponent<Text>().text = "";
        transform.Find("genePowerRate3").Find("Textkwh").GetComponent<Text>().text = "";
        float strpvGenePercent = data.DownloadData("genePowerRate", "strpvGenePercent");
        float strseGenePercent = data.DownloadData("genePowerRate", "strseGenePercent");
        float strpnGenePercent = data.DownloadData("genePowerRate", "strpnGenePercent");
        float photovoltaicGenerate = data.DownloadData("genePowerRate", "photovoltaicGenerate");
        float storedEnergyGenerate = data.DownloadData("genePowerRate", "storedEnergyGenerate");
        float powerNetworkGenerate = data.DownloadData("genePowerRate", "powerNetworkGenerate");

        try
        {
            transform.Find("genePowerRate1").Find("Text").GetComponent<Text>().text = strpvGenePercent.ToString() + "%";
            transform.Find("genePowerRate2").Find("Text").GetComponent<Text>().text = strseGenePercent.ToString() + "%";
            transform.Find("genePowerRate3").Find("Text").GetComponent<Text>().text = strpnGenePercent.ToString() + "%";
            transform.Find("genePowerRate1").Find("Textkwh").GetComponent<Text>().text = photovoltaicGenerate.ToString() + "kWh";
            transform.Find("genePowerRate2").Find("Textkwh").GetComponent<Text>().text = storedEnergyGenerate.ToString() + "kWh";
            transform.Find("genePowerRate3").Find("Textkwh").GetComponent<Text>().text = powerNetworkGenerate.ToString() + "kWh";
        }
        catch
        {
            transform.Find("genePowerRate1").Find("Textkwh").GetComponent<Text>().text = "未获取到数据";
            transform.Find("genePowerRate2").Find("Textkwh").GetComponent<Text>().text = "未获取到数据";
            transform.Find("genePowerRate3").Find("Textkwh").GetComponent<Text>().text = "未获取到数据";

        }
    
        #endregion
  

        //画图
        PieGraph.Inject(new List<PieData>()
        {
            new PieData("动力", power ,co1),
            new PieData("储能", storedEnergy, co2),
            new PieData("照明", lighting, co1),
            new PieData("其他", others, co2),
            new PieData("空调", airConditioner, co1),
            new PieData("充电", charge, co2),
        });

        PieGraph1.Inject(new List<PieData>()
        {
            new PieData("光伏", strpvGenePercent ,co1),
          
        });
        PieGraph2.Inject(new List<PieData>()
        {
            new PieData("储能", strseGenePercent, co2),

        });
        PieGraph3.Inject(new List<PieData>()
        {
            new PieData("电网", strpnGenePercent, co1),

        });
   
    }

   
}