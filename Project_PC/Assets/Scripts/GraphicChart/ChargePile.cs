using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargePile : MonoBehaviour
{
    ReceiveData receiveData = new ReceiveData();
    WMG_List<string> xAxisLabels = new WMG_List<string>();//设置横轴名    
    WMG_List<Vector2> pointvalues1 = new WMG_List<Vector2>();
    WMG_Axis_Graph lineGraph;
    WMG_Series series1;

    Text amountOfchargeText;
    Text costOfChargeText;

    Transform dataLabelsParent;
    List<int> totalTime = new List<int>();
    static float timer = 0;
    // Use this for initialization

    void Awake()
    {
        receiveData.ChargePile("chargePileAndFile");
        for (int i = 9; i < 102; i += 4)
        {
            int a = int.Parse(receiveData.chargeData[i]);
            totalTime.Add(a);

        }
        Hello();
        GetData();
        dataLabelsParent = transform.Find("LineGraphTest/Series/Series1/dataLabelsParent"); //一定要放在最后

        Invoke("CompareValues", 1f);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 60)
        {
            GetData();
            CompareValues();
            timer = 0;
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

        amountOfchargeText = transform.Find("amountOfchargeText").GetComponent<Text>();
        costOfChargeText = transform.Find("costOfChargeText").GetComponent<Text>();
        lineGraph = GameObject.Find("LineGraphTest").GetComponent<WMG_Axis_Graph>();

        lineGraph.graphType = WMG_Axis_Graph.graphTypes.bar_side;   //设置为柱形图

        //删除自带的两个series
        lineGraph.deleteSeries();
        lineGraph.deleteSeries();

        //y轴设置
        // lineGraph.yAxis.AxisMaxValue = 15;
        lineGraph.yAxis.AxisNumTicks = 11;
        lineGraph.yAxis.AxisMinValue = 0;
        lineGraph.yAxis.hideTicks = false;

        //x轴设置      
        lineGraph.xAxis.AxisNumTicks = 24;//0~23
        lineGraph.xAxis.LabelType = WMG_Axis.labelTypes.ticks;
        lineGraph.xAxis.AxisLabelColor = new Color32(250, 238, 36, 255);
        lineGraph.xAxis.hideTicks = false;


        series1 = lineGraph.addSeries();  //自己添加series
        series1.dataLabelsEnabled = true;   //柱形图头顶数字显示
        series1.dataLabelsNumDecimals = 0;
        series1.dataLabelsFontSize = 12;
        series1.dataLabelsColor = new Color32(250, 238, 36, 255); //头顶数字颜色
        series1.seriesName = "充电桩";
        series1.UseXDistBetweenToSpace = true;
        series1.ManuallySetXDistBetween = true;
        series1.xDistBetweenPoints = 21.7f;
        series1.ManuallySetExtraXSpace = true;
        series1.extraXSpace = 0;
        lineGraph.Refresh();
    }

    private void GetData()
    {
        pointvalues1.Clear();
        xAxisLabels.Clear();

        receiveData.ChargePile("chargePileAndFile");

        amountOfchargeText.text = float.Parse(receiveData.chargeData[4]).ToString("0.0") + "kWh";
        costOfChargeText.text = float.Parse(receiveData.chargeData[6]).ToString("0.0") + "元";



        float max = 0;
        for (int i = 9; i < receiveData.chargeData.Count; i += 4)
        {
            xAxisLabels.Add(receiveData.chargeData[i]);    //添加x坐标

            pointvalues1.Add(new Vector2(float.Parse(receiveData.chargeData[i]), float.Parse(float.Parse(receiveData.chargeData[i + 2]).ToString("0"))));

            float num = float.Parse(float.Parse(receiveData.chargeData[i + 2]).ToString("0"));

            if (max <= num)  //y轴动态设置
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
                lineGraph.yAxis.AxisMaxValue =  max;
            }
        }


        lineGraph.xAxis.axisLabels.SetList(xAxisLabels);   //把横坐标显示出来
        series1.pointValues.SetList(pointvalues1);   //把图显示出来
        series1.pointColor = new Color32(86, 186, 255, 255);    //标记点的颜色
        lineGraph.Refresh();
        Invoke("FengXi", 0.5f);
    }


    /// <summary>
    /// 与当前系统时间作比较，如果还没到当前时间点则不显示柱形图头上的标签
    /// </summary>
    void CompareValues()
    {
        int currentTime = System.DateTime.Now.Hour;
        for (int i = 0; i < dataLabelsParent.childCount; i++)
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
        line = transform.Find("LineGraphTest/Background/XAxis/Line").GetComponent<RectTransform>();
        line.sizeDelta = new Vector2(line.sizeDelta.x, 4.5f);
    }

   
}
