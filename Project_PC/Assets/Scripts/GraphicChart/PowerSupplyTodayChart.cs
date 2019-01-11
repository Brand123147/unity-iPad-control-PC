using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSupplyTodayChart : MonoBehaviour {

    WMG_Pie_Graph pieGraphGF;
    WMG_Pie_Graph pieGraphCN;
    WMG_Pie_Graph pieGraphDW;
 
    WMG_List<string> sliceLabelsGF = new WMG_List<string>();
    WMG_List<string> sliceLabelsCN = new WMG_List<string>();
    WMG_List<string> sliceLabelsDW = new WMG_List<string>();

    WMG_List<float> sliceValuesGF = new WMG_List<float>();
    WMG_List<float> sliceValuesCN = new WMG_List<float>();
    WMG_List<float> sliceValuesDW = new WMG_List<float>();

    WMG_List<Color> sliceColorsGF = new WMG_List<Color>();
    WMG_List<Color> sliceColorsCN = new WMG_List<Color>();
    WMG_List<Color> sliceColorsDW = new WMG_List<Color>();

    ReceiveData receiveData = new ReceiveData();

    Text GF_Percent;
    Text CN_Percent;
    Text DW_Percent;
    Text GF;
    Text CN;
    Text DW;

    //Transform circleGF;
    //Transform circleCN;
    //Transform circleDW;
    //float r0 = 97f;
    //float mf = 3.6f;

    float timer = 0f;

    // Use this for initialization
    void Awake()
    {
        pieGraphGF = transform.Find("PieGraphGF").GetComponent<WMG_Pie_Graph>();
        pieGraphCN = transform.Find("PieGraphCN").GetComponent<WMG_Pie_Graph>();
        pieGraphDW = transform.Find("PieGraphDW").GetComponent<WMG_Pie_Graph>();

        GF_Percent = transform.Find("PieGraphGF/Text").GetComponent<Text>();
        CN_Percent = transform.Find("PieGraphCN/Text").GetComponent<Text>();
        DW_Percent = transform.Find("PieGraphDW/Text").GetComponent<Text>();

        GF = transform.Find("PieGraphGF/Textkwh").GetComponent<Text>();
        CN = transform.Find("PieGraphCN/Textkwh").GetComponent<Text>();
        DW = transform.Find("PieGraphDW/Textkwh").GetComponent<Text>();

        //circleGF = transform.Find("CircleGF");
        //circleCN = transform.Find("CircleCN");
        //circleDW = transform.Find("CircleDW");

        Hello();
        GetData();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 60)
        {
            timer = 0;

            GetData();

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
            GetData();
        }
    }


    void Hello()
    {
        //初始化
        pieGraphGF.Init();
        pieGraphCN.Init();
        pieGraphDW.Init();

        //使能
        pieGraphGF.resizeEnabled = true;
        pieGraphCN.resizeEnabled = true;
        pieGraphDW.resizeEnabled = true;

        //不裂缝
        pieGraphGF.explodeLength = 0;
        pieGraphCN.explodeLength = 0;
        pieGraphDW.explodeLength = 0;

        //不掏空
        pieGraphGF.useDoughnut = false;
        pieGraphCN.useDoughnut = false;
        pieGraphDW.useDoughnut = false;

        //自动居中
        pieGraphGF.autoCenter = true;
        pieGraphCN.autoCenter = true;
        pieGraphDW.autoCenter = true;

        //摆放顺序
        pieGraphGF.sortBy = WMG_Pie_Graph.sortMethod.None;
        pieGraphCN.sortBy = WMG_Pie_Graph.sortMethod.None;
        pieGraphDW.sortBy = WMG_Pie_Graph.sortMethod.None;

        //饼状图中百分比显示样式
        pieGraphGF.sliceLabelType = WMG_Enums.labelTypes.None;
        pieGraphCN.sliceLabelType = WMG_Enums.labelTypes.None;
        pieGraphDW.sliceLabelType = WMG_Enums.labelTypes.None;

        //饼状图大小
        pieGraphGF.autoCenterMinPadding = 9f;
        pieGraphCN.autoCenterMinPadding = 9f;
        pieGraphDW.autoCenterMinPadding = 9f;
        pieGraphGF.bgCircleOffset = 1f;
        pieGraphCN.bgCircleOffset = 1f;
        pieGraphDW.bgCircleOffset = 1f;

        //设置饼状图名称
        sliceLabelsGF.Add("光伏");
        sliceLabelsGF.Add("光伏_");
        sliceLabelsCN.Add("储能");
        sliceLabelsCN.Add("储能_");
        sliceLabelsDW.Add("电网");
        sliceLabelsDW.Add("电网_");

        //设置饼状图颜色
        sliceColorsGF.Add(new Color32(84, 176, 241, 255));
        sliceColorsGF.Add(new Color32(43, 54, 84, 0));
        sliceColorsCN.Add(new Color32(121, 220, 248, 255));
        sliceColorsCN.Add(new Color32(43, 54, 84, 0));
        sliceColorsDW.Add(new Color32(84, 176, 241, 255));
        sliceColorsDW.Add(new Color32(43, 54, 84, 0));

        //赋值给最终显示
        pieGraphGF.sliceLabels.Clear();
        pieGraphCN.sliceLabels.Clear();
        pieGraphDW.sliceLabels.Clear();
        pieGraphGF.sliceColors.Clear();
        pieGraphCN.sliceColors.Clear();
        pieGraphDW.sliceColors.Clear();
        pieGraphGF.sliceLabels.SetList(sliceLabelsGF);
        pieGraphCN.sliceLabels.SetList(sliceLabelsCN);
        pieGraphDW.sliceLabels.SetList(sliceLabelsDW);
        pieGraphGF.sliceColors.SetList(sliceColorsGF);
        pieGraphCN.sliceColors.SetList(sliceColorsCN);
        pieGraphDW.sliceColors.SetList(sliceColorsDW);

        pieGraphGF.Refresh();
        pieGraphCN.Refresh();
        pieGraphDW.Refresh();
    }
    void GetData()
    {

        //圈角度的确定 Rotation（97 ~ -263） ===>  (0 ~ 360)  ===>  (0 ~ 100)
       // circleGF.rotation = Quaternion.Euler(0, 0, r0 - (mf * receiveData.DownloadData("genePowerRate", "strpvGenePercent")));
       // circleCN.rotation = Quaternion.Euler(0, 0, r0 - (mf * receiveData.DownloadData("genePowerRate", "strseGenePercent")));
       // circleDW.rotation = Quaternion.Euler(0, 0, r0 - (mf * receiveData.DownloadData("genePowerRate", "strpnGenePercent")));
       

        //清空
        sliceValuesCN.Clear();
        sliceValuesDW.Clear();
        sliceValuesGF.Clear();
        pieGraphGF.sliceValues.Clear();
        pieGraphCN.sliceValues.Clear();
        pieGraphDW.sliceValues.Clear();
        GF_Percent.text = "";
        CN_Percent.text = "";
        DW_Percent.text = "";
        GF.text = "";
        CN.text = "";
        DW.text = "";

        //赋值
        sliceValuesGF.Add(receiveData.DownloadData("genePowerRate", "strpvGenePercent"));
        sliceValuesGF.Add(100 - receiveData.DownloadData("genePowerRate", "strpvGenePercent"));
        sliceValuesCN.Add(receiveData.DownloadData("genePowerRate", "strseGenePercent"));
        sliceValuesCN.Add(100 - receiveData.DownloadData("genePowerRate", "strseGenePercent"));
        sliceValuesDW.Add(receiveData.DownloadData("genePowerRate", "strpnGenePercent"));
        sliceValuesDW.Add(100 - receiveData.DownloadData("genePowerRate", "strpnGenePercent"));

        pieGraphGF.sliceValues.SetList(sliceValuesGF);
        pieGraphCN.sliceValues.SetList(sliceValuesCN);
        pieGraphDW.sliceValues.SetList(sliceValuesDW);

        GF_Percent.text = receiveData.ReturnData("genePowerRate", "strpvGenePercent") + "%";
        CN_Percent.text = receiveData.ReturnData("genePowerRate", "strseGenePercent") + "%";
        DW_Percent.text = receiveData.ReturnData("genePowerRate", "strpnGenePercent") + "%";
        GF.text = receiveData.ReturnData("genePowerRate", "photovoltaicGenerate") + "kWh";
        CN.text = receiveData.ReturnData("genePowerRate", "storedEnergyGenerate") + "kWh";
        DW.text = receiveData.ReturnData("genePowerRate", "powerNetworkGenerate") + "kWh";

        pieGraphGF.Refresh();
        pieGraphCN.Refresh();
        pieGraphDW.Refresh();

       
    }
}
