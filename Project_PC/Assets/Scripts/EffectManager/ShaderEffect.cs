using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ShaderEffect : MonoBehaviour {

    public Material saoguang;

    private float addNum = 0;

    static float minNum = -3.14f;

    static float maxNum = 3.14f;

     float currentNum = maxNum;

    // Use this for initialization
    void Start() {
        
    }
	// Update is called once per frame
	void Update () {
        //if (addNum <= 0.5f)  //延迟加加
        //{
        //    addNum += Time.deltaTime;
        //    Debug.Log(addNum);
        //}
        //else
        //{
            if (currentNum < maxNum)
            {
                currentNum -= Time.deltaTime;
                //把值赋给shader
                saoguang.SetFloat("_saoguang", currentNum);
                //addNum = 0;
            }
            else
            {
                currentNum = minNum;
            }
        //}
	}
}
