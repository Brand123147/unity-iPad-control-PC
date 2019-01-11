using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetialPosition : MonoBehaviour
{

    static bool showPosition = false;
    public GameObject DataPosition;
    DataShow dataShow;

    public void ShowPositionIn()
    {
        //显示
        showPosition = true;
        for (int i = 0; i < DataPosition.transform.childCount; i++)
        {
            DataPosition.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
            DataPosition.transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
        }
        //隐藏介绍页，主页，漫游
        dataShow.IntroduceOut();
        dataShow.DataLogoOut();
        dataShow.CruiseLandspaceStop();
    }
    public void ShowPositionOut()
    {
        if (showPosition == false) return;

        //隐藏
        showPosition = false;
        for (int i = 0; i < DataPosition.transform.childCount; i++)
        {
            DataPosition.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
            DataPosition.transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
        }


    }
    public void BoolShowPosition()
    {
        if (showPosition == false)
        {
            ShowPositionIn();
        }
        else
        {
            ShowPositionOut();
        }

    }

    public void BoolPressAlpha3()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BoolShowPosition();
        }
    }

    // Use this for initialization
    void Start()
    {
        dataShow = GameObject.Find("Canvas").GetComponent<DataShow>();
    }

    // Update is called once per frame
    void Update()
    {
        BoolPressAlpha3();
    }
}
