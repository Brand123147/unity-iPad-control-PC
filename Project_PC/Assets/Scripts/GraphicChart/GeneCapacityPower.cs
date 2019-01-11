using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneCapacityPower : MonoBehaviour
{

    WMG_Series series1;     //折线图，发电功率

    WMG_Axis_Graph barGraph;
    WMG_List<string> xAxisLabels = new WMG_List<string>();//设置横轴刻度    
    WMG_List<Vector2> pointvalues1 = new WMG_List<Vector2>();    //柱形图的设置 

    ReceiveData receiveData = new ReceiveData();

    static float timer = 0;
    // Use this for initialization
    private void Awake()
    {
        Hello();
        GetData();
    }

    private void Update()
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
        barGraph = transform.Find("BarGraphTest").GetComponent<WMG_Axis_Graph>();
        barGraph.deleteSeries();
        barGraph.deleteSeries();

        //设置图形样式
        barGraph.graphType = WMG_Axis_Graph.graphTypes.line;
        //x轴设置
        barGraph.xAxis.hideGrid = true;
        barGraph.xAxis.LabelType = WMG_Axis.labelTypes.ticks;
        barGraph.xAxis.AxisLabelSize = 10;  //x轴下标字体大小
        barGraph.xAxis.AxisMaxValue = 24;   //x轴总长度
        barGraph.xAxis.AxisLabelColor = new Color32(250, 238, 36, 255);  //标记颜色


        //y轴设置
        barGraph.yAxis.AxisLabelSize = 9;
       // barGraph.yAxis.AxisMaxValue = 300;    //y轴最大值
        barGraph.yAxis.AxisNumTicks = 11;       //y轴标记个数
        barGraph.yAxis.AxisLabelSpaceOffset = 2;  //标字与轴的距离
        barGraph.yAxis.AxisLabelColor = new Color32(250, 238, 36, 255);  //标记颜色

        //图表的设置
        series1 = barGraph.addSeries();
        series1.comboType = WMG_Series.comboTypes.line;
        series1.UseXDistBetweenToSpace = true;
        series1.seriesName = "发电功率";
        barGraph.Refresh();

    }


    //数据接入
    private void GetData()
    {
        pointvalues1.Clear();
        xAxisLabels.Clear();
        receiveData.GenerationData("geneCapacityPower");

        // int j = 122;   // x轴从4~22点开始

        float max = 0;
        for (int i = 2; i < receiveData.geneData.Count; i += 4)
        {
            barGraph.xAxis.AxisNumTicks = int.Parse(receiveData.geneData[i]) - 3;

            xAxisLabels.Add(float.Parse(receiveData.geneData[i]).ToString("0"));

            pointvalues1.Add(new Vector2(float.Parse(receiveData.geneData[i]), float.Parse(receiveData.geneData[i + 2])));

            float num = float.Parse(receiveData.geneData[i + 2]);

            if (max < num)
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
    }


}
