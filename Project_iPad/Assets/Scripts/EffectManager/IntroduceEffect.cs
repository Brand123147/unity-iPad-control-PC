using System.Collections;
using System.Collections.Generic;
//using HighlightingSystem;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class IntroduceEffect : MonoBehaviour
{
    public GameObject circleEnv;  //节能环保3圈特效

    public List<GameObject> characterTwinkles;

    public List<GameObject> circleTwinkles;

    public GameObject circleIntroduce;  //介绍面板圈特效

    // Use this for initialization
    void Start()
    {

        CharacterOFF();
        CircleOFF();
    }
    #region 两列标题特效


    public void CharacterON()
    {
        //节能环保3圈特效
        circleEnv.SetActive(true);

        StartCoroutine("CharacterOn");
    }
    public void CharacterOFF()
    {
        //节能环保3圈特效
        circleEnv.SetActive(false);

        for (int i = 0; i < characterTwinkles.Count; i++)
        {
            characterTwinkles[i].GetComponent<Image>().enabled = false;
        }

        StopCoroutine("CharacterOn");
    }
    IEnumerator CharacterOn()
    {
        for (int i = 0; i < characterTwinkles.Count; i++)
        {
            characterTwinkles[i].GetComponent<Image>().enabled = true;

            characterTwinkles[i].GetComponent<Image>().DOFade(1f, 1f);

            yield return new WaitForSecondsRealtime(1f);

            characterTwinkles[i].GetComponent<Image>().DOFade(0f, 1f);

            yield return new WaitForSecondsRealtime(1f);

            characterTwinkles[i].GetComponent<Image>().enabled = false;
        }

    }
    #endregion


    #region 介绍圆圈特效
    public void CircleON()
    {
        //介绍面板圈特效
        circleIntroduce.SetActive(true);

        StartCoroutine("CircleOn");
    }
    public void CircleOFF()
    {
        //介绍面板圈特效
        circleIntroduce.SetActive(false);

        for (int i = 0; i < circleTwinkles.Count; i++)
        {

            circleTwinkles[i].SetActive(false);
        }

        StopCoroutine("CircleOn");
    }
    IEnumerator CircleOn()
    {
        for (int i = 0; i < 11; i++)
        {
            circleTwinkles[i].SetActive(true);

            circleTwinkles[i].GetComponent<Image>().DOFade(1f, 1f);

            if (i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 7 || i == 8 || i == 9 || i == 10)
            {
                circleTwinkles[i].transform.GetChild(0).GetComponent<Image>().DOFade(1f, 1f);
            }

            yield return new WaitForSecondsRealtime(1f);

            circleTwinkles[i].GetComponent<Image>().DOFade(0f, 1f);

            if (i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 7 || i == 8 || i == 9 || i == 10)
            {
                circleTwinkles[i].transform.GetChild(0).GetComponent<Image>().DOFade(0f, 1f);
            }

            yield return new WaitForSecondsRealtime(1f);

            circleTwinkles[i].SetActive(false);
        }

    }
    #endregion


    //public void TwinkLeON()
    //{
    //    StartCoroutine("TwinkleOn");
    //}
    //public void TwinkleOFF()
    //{
    //    StopCoroutine("TwinkleOn");
    //    foreach (var item in twinkles)
    //    {
    //        item.SetActive(false);
    //    }
    //}



    //IEnumerator TwinkleOn()
    //{
    //    for (int i = 0; i < twinkles.Count; i++)
    //    {
    //        twinkles[i].SetActive(true);
    //        twinkles[i].GetComponent<Highlighter>().enabled = true;
    //        twinkles[i].GetComponent<HighlighterFlashing>().enabled = true;
    //        twinkles[i].GetComponent<HighlighterFlashing>().seeThrough = true;
    //        yield return new WaitForSecondsRealtime(5f);
    //        twinkles[i].GetComponent<Highlighter>().enabled = false;
    //        twinkles[i].GetComponent<HighlighterFlashing>().enabled = false;
    //        twinkles[i].GetComponent<HighlighterFlashing>().seeThrough = false;
    //        twinkles[i].SetActive(false);
    //    }
    //}
}
