﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerChart : MonoBehaviour
{

    ReceiveData receiveData = new ReceiveData();
    WMG_List<string> xAxisLabels = new WMG_List<string>();//设置横轴名    
    WMG_List<Vector2> pointvalues1 = new WMG_List<Vector2>();    //柱形图的设置  
    WMG_Axis_Graph barGraph;
    WMG_Series series1;    //柱形图

    Text todayUsePowerAmount;  //今日用电量
    Text nowLoad;  //当前负荷
    Text todayPowerMoney;      //今日电费

    static float timer = 0;
    List<int> totalTime = new List<int>();
    Transform dataLabelsParent;
    // Use this for initialization
    void Awake()
    {
        todayUsePowerAmount = transform.Find("todayUsePowerAmount").GetComponent<Text>();
        nowLoad = transform.Find("nowLoad").GetComponent<Text>();
        todayPowerMoney = transform.Find("todayPowerMoney").GetComponent<Text>();
        barGraph = transform.Find("LineGraphPower").GetComponent<WMG_Axis_Graph>();
        receiveData.PowerSys("powerDistriSystem");
        for (int i = 2; i <= 95; i += 4)
        {
            int a = int.Parse(receiveData.powerData[i]);
            totalTime.Add(a);
        }
        Hello();
        GetData();
        dataLabelsParent = transform.Find("LineGraphPower/Series/Series1/dataLabelsParent");
        Invoke("CompareValues", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 60)
        {
            timer = 0;
            GetData();
            CompareValues();
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
            Invoke("CompareValues", 1f);
        }

    }
    void Hello()
    {
      
        //删除自带的两个series
        barGraph.deleteSeries();
        barGraph.deleteSeries();

        //设置为柱形图
        barGraph.graphType = WMG_Axis_Graph.graphTypes.bar_side;

        //y轴设置
        barGraph.yAxis.hideGrid = true;
      //  barGraph.yAxis.AxisMaxValue = 100;   //y轴最大值，设为自动
        barGraph.yAxis.AxisMinValue = 0;
        barGraph.yAxis.AxisNumTicks = 11;
        barGraph.yAxis.AxisLabelColor = new Color32(250, 238, 36, 255);

        //x轴设置
        barGraph.xAxis.AxisNumTicks = 24;  //0点到23点      
        barGraph.xAxis.hideGrid = true;
        barGraph.xAxis.LabelType = WMG_Axis.labelTypes.ticks;
        barGraph.xAxis.AxisLabelColor = new Color32(250, 238, 36, 255);



        series1 = barGraph.addSeries();  //自己添加series   
        series1.comboType = WMG_Series.comboTypes.bar;
        series1.dataLabelsEnabled = true;
        series1.dataLabelsNumDecimals = 0;    //主子图形上显示几位小数
        series1.dataLabelsFontSize = 12;
        series1.dataLabelsColor = new Color32(250, 238, 36, 255);
        series1.dataLabelsOffset = new Vector2(0, 5);
        series1.UseXDistBetweenToSpace = true;
        series1.ManuallySetXDistBetween = true;
        series1.xDistBetweenPoints = 21.9f;  //调整间距  
        series1.ManuallySetExtraXSpace = true;
        series1.extraXSpace = 0;
        series1.seriesName = "配电系统";
        barGraph.Refresh();
    }

    void GetData()
    {
        pointvalues1.Clear();
        xAxisLabels.Clear();
        receiveData.PowerSys("powerDistriSystem");

        todayUsePowerAmount.text = float.Parse(receiveData.powerData[receiveData.powerData.Count - 13]).ToString("f1") + "kWh";
        nowLoad.text = float.Parse(receiveData.powerData[receiveData.powerData.Count - 11]).ToString("f1") + "kW";
        todayPowerMoney.text = float.Parse(receiveData.powerData[receiveData.powerData.Count - 9]).ToString("f1") + "元";
        float max = 0;
        for (int i = 2; i <= receiveData.powerData.Count - 17; i += 4)
        {

            xAxisLabels.Add(receiveData.powerData[i]);    //添加x坐标

            pointvalues1.Add(new Vector2(float.Parse(receiveData.powerData[i]), float.Parse(float.Parse(receiveData.powerData[i + 2]).ToString("0"))));

            float num = float.Parse(float.Parse(receiveData.powerData[i + 2]).ToString("0"));

            if (max <= num)
            {
                max = num;
                if (max <= 10)
                {
                    max = 10;
                }
                else if (max > 10 && max <= 15)
                {
                    max = 15f;
                }
                else if (max > 15 && max <= 30)
                {
                    max = 30f;
                }
                else if (max > 30 && max <= 50)
                {
                    max = 50f;
                }
                else if (max > 50 && max <= 100)
                {
                    max = 100f;
                }
                else if (max > 100 && max <= 150)
                {
                    max = 150f;
                }
                else if (max > 150 && max <= 200)
                {
                    max = 200;
                }
                else if (max > 200 && max <= 250)
                {
                    max = 250f;
                }
                else if (max > 250 && max <= 300)
                {
                    max = 300f;
                }
                else
                {
                    max = num;
                }
                barGraph.yAxis.AxisMaxValue = max;
            }
        }
        barGraph.xAxis.axisLabels.SetList(xAxisLabels);   //把横坐标显示出来
        series1.pointValues.SetList(pointvalues1);   //把图显示出来
        series1.pointColor = new Color32(86, 186, 255, 255);    //标记点的颜色
        barGraph.Refresh();
        Invoke("FengXi", 0.5f);
    }

    void CompareValues()
    {
        int currentTime = System.DateTime.Now.Hour;
        for (int i = 0; i < totalTime.Count; i++)
        {
            if (totalTime[i] <= currentTime)
            {
                dataLabelsParent.GetChild(i).GetComponent<Text>().enabled = true;
            }
            else
            {
                dataLabelsParent.GetChild(i).GetComponent<Text>().enabled = false;
            }
        }
    }

    void FengXi()
    {   //柱形图与x轴有缝隙
        //barGraph.autoUpdateBarAxisValue = false;
        //barGraph.barAxisValue = -0.3f;
        RectTransform line;
        line = transform.Find("LineGraphPower/Background/XAxis/Line").GetComponent<RectTransform>();
        line.sizeDelta = new Vector2(line.sizeDelta.x, 4.5f);
    }
}
