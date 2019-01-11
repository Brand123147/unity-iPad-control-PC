using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieChart : MonoBehaviour
{

    WMG_Pie_Graph pieGraph;

    ReceiveData receiveData = new ReceiveData();

    WMG_List<string> sliceLabels = new WMG_List<string>();

    WMG_List<float> sliceValues = new WMG_List<float>();

    WMG_List<Color> sliceColors = new WMG_List<Color>();

    static float timer = 0;

    RectTransform labelZM;

    RectTransform labelCN;

    RectTransform labelDL;

    RectTransform labelKT;

    void Hello()
    {

        pieGraph = transform.Find("PieGraphData").GetComponent<WMG_Pie_Graph>();

        pieGraph.Init();

        pieGraph.resizeEnabled = true;

        pieGraph.explodeLength = 1.1f; //饼块分离程度为0，意思为不分离

        pieGraph.useDoughnut = true;  //中间掏空

        pieGraph.doughnutPercentage = 0.4f;   //掏空大小

        pieGraph.autoCenter = true;  //自动居中

        pieGraph.autoCenterMinPadding = 41.4f;   //自动居中后插孔填充背景

        pieGraph.bgCircleOffset = 4.7f;

        pieGraph.sliceLabelType = WMG_Enums.labelTypes.Labels_Percents;  //显示标题和百分比

        pieGraph.sortBy = WMG_Pie_Graph.sortMethod.None;   //摆放顺序

        //调整标签
        pieGraph.sliceLabelExplodeLength = 35;

        pieGraph.sliceLabelFontSize = 15;

        pieGraph.numberDecimalsInPercents = 1;

        pieGraph.sliceLabelColor = new Color32(73, 181, 255, 255);
    }



    void GetData()
    {

        sliceLabels.Clear();

        sliceValues.Clear();

        sliceColors.Clear();

        //设置序列名、数值（会自动求比例的）、颜色
        sliceLabels.Add("动力");
        sliceLabels.Add("充电");
        sliceLabels.Add("空调");
        sliceLabels.Add("其他");
        sliceLabels.Add("照明");
        sliceLabels.Add("储能");


        sliceValues.Add(receiveData.DownloadData("usePowerRate", "power"));
        sliceValues.Add(receiveData.DownloadData("usePowerRate", "charge"));
        sliceValues.Add(receiveData.DownloadData("usePowerRate", "airConditioner"));
        sliceValues.Add(receiveData.DownloadData("usePowerRate", "others"));
        sliceValues.Add(receiveData.DownloadData("usePowerRate", "lighting"));
        sliceValues.Add(receiveData.DownloadData("usePowerRate", "storedEnergy"));


        sliceColors.Add(new Color32(86, 186, 255, 255));
        sliceColors.Add(new Color32(123, 223, 252, 255));
        sliceColors.Add(new Color32(86, 186, 255, 255));
        sliceColors.Add(new Color32(123, 223, 252, 255));
        sliceColors.Add(new Color32(86, 186, 255, 255));
        sliceColors.Add(new Color32(123, 223, 252, 255));


        pieGraph.sliceValues.SetList(sliceValues);
        pieGraph.sliceLabels.SetList(sliceLabels);
        pieGraph.sliceColors.SetList(sliceColors);


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

    // Use this for initialization
    void Awake()
    {
        Hello();
        GetData();

        Invoke("LabelsPrecent", 1f);
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

    #region 调整数字百分比位置

    void LabelsPrecent()
    {
        labelKT = transform.Find("PieGraphData/Slices/空调/Text").GetComponent<RectTransform>();
        labelZM = transform.Find("PieGraphData/Slices/照明/Text").GetComponent<RectTransform>();
        labelCN = transform.Find("PieGraphData/Slices/储能/Text").GetComponent<RectTransform>();
        labelDL = transform.Find("PieGraphData/Slices/动力/Text").GetComponent<RectTransform>();

        labelKT.anchoredPosition = new Vector2(labelKT.anchoredPosition.x, -62f);
        labelZM.anchoredPosition = new Vector2(labelZM.anchoredPosition.x, 56.9f);
        labelCN.anchoredPosition = new Vector2(labelCN.anchoredPosition.x, 77.28f);
        labelDL.anchoredPosition = new Vector2(labelDL.anchoredPosition.x, 60.32f);
    }
    #endregion
}
