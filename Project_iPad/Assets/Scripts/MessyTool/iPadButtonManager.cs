using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class iPadButtonManager : MonoBehaviour
{

    public List<GameObject> iPadButtons;

    #region 7个按钮显隐

    public void OnClickButton(int index, bool isPressed)
    {
        for (int i = 0; i < 7; i++)
        {
            iPadButtons[i].SetActive(false);
        }
        iPadButtons[index].SetActive(isPressed);

    }

    IEnumerator ShowButton()
    {
        for (int i = 0; i < 7; i++)
        {
            iPadButtons[i].transform.parent.DOLocalMoveY(-700f, 0.5f);
            yield return new WaitForSecondsRealtime(0.2f);
            
        }
    }
    IEnumerator HideButton()
    {
        for (int i = 0; i < 7; i++)
        {
            iPadButtons[i].transform.parent.DOLocalMoveY(-940f, 0.5f);
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
    public void OnClickShowButton()
    {
        StartCoroutine("ShowButton");
    }
    public void OnClickHideButton()
    {
        StartCoroutine("HideButton");
    }
    #endregion


    public void OnClickFunctionButton(int index, bool isPressed)
    {
        for (int i = 7; i < 11; i++)
        {
            iPadButtons[i].SetActive(false);
        }
        iPadButtons[index].SetActive(isPressed);
    }

    public void OnClickZoon(int index, bool isPressed)
    {
        for (int i = 11; i < 13; i++)
        {
            iPadButtons[i].SetActive(false);
        }
        iPadButtons[index].SetActive(isPressed);
    }
    public void OnClickSet(int index, bool isPressed)
    {
        for (int i = 13; i < 14; i++)
        {
            iPadButtons[i].SetActive(false);
        }
        iPadButtons[index].SetActive(isPressed);
    }
    //public void OnClickBGM(int index, bool isPressed)
    //{
    //    for (int i = 14; i < 15; i++)
    //    {
    //        iPadButtons[i].SetActive(false);
    //    }
    //    iPadButtons[index].SetActive(isPressed);
    //}
}
