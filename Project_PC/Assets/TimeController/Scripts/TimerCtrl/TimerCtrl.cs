using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking.Match;
using System.Text.RegularExpressions;


public class TimerCtrl : MonoBehaviour
{
    [Serializable]
    public struct Timer
    {
        public int year;
        public int month;
        public int day;

        public Timer(int year, int month, int day)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }
    }
    [HideInInspector]
    public Timer mTimer = new Timer(2016, 12, 31);
    private DateTime targetTime;
    private DateTime currentTime;

    private string path;
    private string filename = "DATE.txt";
    private string current;
    void Awake()
    {
#if !UNITY_EDITOR
        //每当过期后重新设置时间的时候将下面的初始密匙复制到Keys文件夹下的DATE.txt
        //初始的密匙:074123084123074123084123075123075123075123074123091123074123073123065123075123075123065123075123075123091123058123054123
        targetTime = new DateTime(mTimer.year, mTimer.month, mTimer.day);
        Debug.Log("targetTime = " + targetTime);

        path = Application.streamingAssetsPath + "/Keys";
        var file = Path.Combine(path, filename);
        FileInfo fileInfo = new FileInfo(file);
        if (fileInfo.Exists)
        {
            StreamReader sr = new StreamReader(file);
            var _time = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            if (!string.IsNullOrEmpty(_time))
            {
                current = GetKey(_time);
                Debug.Log("current = " + current);
            }
            else
            {
                Application.Quit();
                Debug.LogWarning("Key is null ! ! !");
            }
        }
        else
        {
            //如果文件不存在直接退出
            Application.Quit();
            Debug.LogWarning("Path is null ! ! !");
            return;
        }
        currentTime = current == DateTime.MinValue.ToString() ? DateTime.Now : DateTime.Parse(current);
        TimeSpan timeSpan = targetTime - currentTime;
        if (timeSpan.TotalSeconds < 0)
        {
            if (current == DateTime.MinValue.ToString())
            {
                StreamWriter sw = new StreamWriter(file);
                var _time = AddKey(DateTime.Now.ToString());
                sw.WriteLine(_time);
                sw.Close();
                sw.Dispose();
            }
            Application.Quit();
        }
#endif
    }
    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string AddKey(string str)
    {
        StringBuilder sb = new StringBuilder();
        byte[] bytes1 = Encoding.Unicode.GetBytes(str);
        for (int i = 0; i < bytes1.Length; i++)
        {
            bytes1[i] = (byte)(bytes1[i] ^ 123);
            sb.AppendFormat("{0:D3}", bytes1[i]);
        }
        return sb.ToString();
    }
    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string GetKey(string str)
    {
        MatchCollection matches = Regex.Matches(str, @"\d{3}");
        byte[] bytes2 = new byte[matches.Count];
        for (int i = 0; i < matches.Count; i++)
            bytes2[i] = (byte)(byte.Parse(matches[i].Value) ^ 123);
        return Encoding.Unicode.GetString(bytes2);
    }
}