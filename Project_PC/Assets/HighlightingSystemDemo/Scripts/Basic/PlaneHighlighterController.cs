using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;

public class PlaneHighlighterController : MonoBehaviour {
    protected Highlighter hh;
    private void Awake()
    {
        hh = GetComponent<Highlighter>();
        if (hh == null)
        {
            hh =gameObject.AddComponent<Highlighter>();
        }
    }
    // Use this for initialization
    void Start () {

        // hh.On(Color.green);
        //  hh.ConstantParams(Color.red);
      //  hh.FlashingOn(Color.green, Color.green);
        hh.ConstantOn(Color.yellow);

        // Turn off constant highlighting
        //hh.ConstantOffImmediate();

        // Start flashing from blue to cyan color and frequency = 2f
       // hh.FlashingOn(Color.blue, Color.cyan, 2f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
