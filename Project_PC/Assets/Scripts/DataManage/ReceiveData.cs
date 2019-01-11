using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using UnityEngine;

public class ReceiveData
{
    WebClient _client = new WebClient();

    public List<string> geneData = new List<string>();
    public List<string> chargeData = new List<string>();
    public List<string> photovoltaicWall = new List<string>();
    public List<string> photovoltaicRoof = new List<string>();
    public List<string> photovoltaicCar = new List<string>();
    public List<string> airData = new List<string>();
    public List<string> storageData = new List<string>();
    public List<string> powerData = new List<string>();

    string address = "http://IPaddress:9091/zizhu/";

    ////今日用电占比
    //string usePowerRate = "usePowerRate";
    ////今日发电占比
    //string genePowerRate = "genePowerRate";
    ////充放电柱状图 
    //string chargeDisAmount = "chargeDisAmount";
    ////充电量曲线图 
    //string chargeAmount = "chargeAmount";
    ////1#—6#充电桩 
    //string chargePile = "chargePile";
    ////充电桩档案 
    //string chargePileFile = "chargePileFile";
    ////配电系统档案 
    //string powerDistriFile = "powerDistriFile";
    ////储能系统档案 
    //string energyStoreFile = "energyStoreFile";
    ////光伏电站档案  
    //string photovoltaicStationfile = "photovoltaicStationfile";
    ////今日发电量/发电功率  
    //string geneCapacityPower = "geneCapacityPower";
    ////节能环保效益  
    //string saveEnergy = "saveEnergy";

    private void Start()
    {
        //Debug.Log(ReturnData("saveEnergy", "totalCo"));
        //Debug.Log(ReturnData("saveEnergy", "totalCoal"));
        //Debug.Log(ReturnData("saveEnergy", "saveTrees"));
        //Debug.Log(DownloadData("saveEnergy", "power"));
        //Debug.Log(DownloadData(saveEnergy, "charge"));
        //Debug.Log(DownloadData(saveEnergy, "airConditioner"));

    }
    #region Web方式返回字典今日用电占比

    public float DownloadData(string url, string name)
    {


        List<string> result = new List<string>();

        Dictionary<string, float> data = new Dictionary<string, float>();

        string Path = address + url;

        string str = _client.DownloadString(Path);

        string[] getStr = str.Split(new char[8] { ':', '[', ']', '{', '}', ',', '"', '%' });

        for (int i = 1; i < getStr.Length; i++)
        {
            //去除空格
            if (string.IsNullOrEmpty(getStr[i]) == false)
            {
                result.Add(getStr[i]);
            }
        }
        for (int i = 1; i < result.Count; i += 2)
        {
            data.Add(result[i], float.Parse(result[i + 1]));
        }

        return data[name];
    }

    #endregion

    #region 返回列表
    public string ReturnData(string url, string name)
    {

        List<string> result = new List<string>();

        Dictionary<string, float> data = new Dictionary<string, float>();

        data.Clear();

        string Path = address + url;

        string str = _client.DownloadString(Path);

        string[] getStr = str.Split(new char[8] { ':', '[', ']', '{', '}', ',', '"', '%' });

        for (int i = 1; i < getStr.Length; i++)
        {
            //去除空格
            if (string.IsNullOrEmpty(getStr[i]) == false)
            {
                result.Add(getStr[i]);
            }
        }
        int index = result.IndexOf(name);  //返回该值所在的索引
        return result[index + 1];
    }
    #endregion


    #region 发电功率折线图
    public void GenerationData(string url)
    {
        List<string> result = new List<string>();


        string Path = address + url;

        if (!_client.IsBusy)
        {
            string str = _client.DownloadString(Path);

            string[] getStr = str.Split(new char[8] { ':', '[', ']', '{', '}', ',', '"', '%' });

            for (int i = 0; i < getStr.Length; i++)
            {
                //去除空格
                if (string.IsNullOrEmpty(getStr[i]) == false)
                {
                    result.Add(getStr[i]);
                }
            }
        }
		//打印是第几个数据
        //for (int i = 0; i < result.Count; i++)
        //{
        //    Debug.Log(i + "个" + result[i]);
        //}

        geneData = result;

    }
    #endregion



    #region 充电桩
    public void ChargePile(string url)
    {
        List<string> result = new List<string>();



        string Path = address + url;

        if (!_client.IsBusy)
        {
            string str = _client.DownloadString(Path);

            string[] getStr = str.Split(new char[8] { ':', '[', ']', '{', '}', ',', '"', '%' });

            for (int i = 0; i < getStr.Length; i++)
            {
                //去除空格
                if (string.IsNullOrEmpty(getStr[i]) == false)
                {
                    result.Add(getStr[i]);
                }
            }
        }
        //for (int i = 0; i < result.Count; i++)
        //{
        //    Debug.Log(i + "个" + result[i]);
        //}

        chargeData = result;

    }
    #endregion


    #region 光伏车棚/光伏幕墙/光伏屋顶
    public void PhotovoltaicCar(string url)
    {
        List<string> result = new List<string>();

        string Path = address + url;

        if (!_client.IsBusy)
        {
            string str = _client.DownloadString(Path);

            string[] getStr = str.Split(new char[8] { ':', '[', ']', '{', '}', ',', '"', '%' });

            for (int i = 0; i < getStr.Length; i++)
            {
                //去除空格
                if (string.IsNullOrEmpty(getStr[i]) == false)
                {
                    result.Add(getStr[i]);
                }
            }
        }
        //for (int i = 0; i < result.Count; i++)
        //{
        //    Debug.Log(i + "个" + result[i]);
        //}

        photovoltaicCar = result;


    }
    public void PhotovoltaicRoof(string url)
    {
        List<string> result = new List<string>();


        string Path = address + url;

        if (!_client.IsBusy)
        {
            string str = _client.DownloadString(Path);

            string[] getStr = str.Split(new char[8] { ':', '[', ']', '{', '}', ',', '"', '%' });

            for (int i = 0; i < getStr.Length; i++)
            {
                //去除空格
                if (string.IsNullOrEmpty(getStr[i]) == false)
                {
                    result.Add(getStr[i]);
                }
            }
        }
        //for (int i = 0; i < result.Count; i++)
        //{
        //    Debug.Log(i + "个" + result[i]);
        //}

        photovoltaicRoof = result;

    }
    public void PhotovoltaicWall(string url)
    {
        List<string> result = new List<string>();


        string Path = address + url;

        if (!_client.IsBusy)
        {
            string str = _client.DownloadString(Path);

            string[] getStr = str.Split(new char[8] { ':', '[', ']', '{', '}', ',', '"', '%' });

            for (int i = 0; i < getStr.Length; i++)
            {
                //去除空格
                if (string.IsNullOrEmpty(getStr[i]) == false)
                {
                    result.Add(getStr[i]);
                }
            }
        }
        //for (int i = 0; i < result.Count; i++)
        //{
        //    Debug.Log(i + "个" + result[i]);
        //}

        photovoltaicWall = result;

    }
    #endregion


    #region 空调
    public void AirConditoner(string url)
    {
        List<string> result = new List<string>();


        string Path = address + url;

        if (!_client.IsBusy)
        {
            string str = _client.DownloadString(Path);

            string[] getStr = str.Split(new char[8] { ':', '[', ']', '{', '}', ',', '"', '%' });

            for (int i = 0; i < getStr.Length; i++)
            {
                //去除空格
                if (string.IsNullOrEmpty(getStr[i]) == false)
                {
                    result.Add(getStr[i]);
                }
            }
        }
        //for (int i = 0; i < result.Count; i++)
        //{
        //    Debug.Log(i + "个" + result[i].ToString());
        //}

        airData = result;

    }
    #endregion

    #region 储能电池
    public void Storage(string url)
    {
        List<string> result = new List<string>();


        string Path = address + url;

        if (!_client.IsBusy)
        {
            string str = _client.DownloadString(Path);

            string[] getStr = str.Split(new char[8] { ':', '[', ']', '{', '}', ',', '"', '%' });

            for (int i = 0; i < getStr.Length; i++)
            {
                //去除空格
                if (string.IsNullOrEmpty(getStr[i]) == false)
                {
                    result.Add(getStr[i]);
                }
            }
        }
        //for (int i = 0; i < result.Count; i++)
        //{
        //    Debug.Log(i + "个" + result[i].ToString());
        //}

        storageData = result;

    }
    #endregion

    #region 配电系统
    public void PowerSys(string url)
    {
        List<string> result = new List<string>();


        string Path = address + url;

        if (!_client.IsBusy)
        {
            string str = _client.DownloadString(Path);

            string[] getStr = str.Split(new char[8] { ':', '[', ']', '{', '}', ',', '"', '%' });

            for (int i = 0; i < getStr.Length; i++)
            {
                //去除空格
                if (string.IsNullOrEmpty(getStr[i]) == false)
                {
                    result.Add(getStr[i]);
                }
            }
        }
        //for (int i = 0; i < result.Count; i++)
        //{
        //    Debug.Log(i + "个" + result[i].ToString());
        //}

        powerData = result;

    }
    #endregion
    #region WWW方式

    //public IEnumerator HelloData(string Hello)   //迭代器的返回值必须是IEnumerator或IEnumerable中的一种
    //{

    //    using (WWW www = new WWW(address + Hello))
    //    {
    //        yield return www;      //迭代器遍历类中的数据结构，自动生成movenext方法，依次返回每个元素
    //        if (www.isDone)
    //        {
    //            //Debug.Log(www.text);  //string类型
    //            string str = www.text;

    //            string[] getStr = str.Split(new char[7] { ':', '[', ']', '{', '}', ',', '"' });

    //            for (int i = 1; i < getStr.Length; i++)
    //            {
    //                //去除空格
    //                if (string.IsNullOrEmpty(getStr[i]) == false)
    //                {
    //                    result.Add(getStr[i]);
    //                }
    //            }
    //        }
    //    }
    //    for (int i = 1; i < result.Count; i += 2)
    //    {
    //        data.Add(result[i],float.Parse( result[i + 1]));          
    //    }       
    //}

    #endregion



}
