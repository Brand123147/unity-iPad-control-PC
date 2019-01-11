using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;

namespace HighlightingSystem
{


    public class AircondictionEffect : MonoBehaviour
    {

        private Highlighter h;
        public GameObject whiteSmoke;
        // Use this for initialization
        void OnEnable()
        {
            h = gameObject.GetComponent<Highlighter>();
            if (h == null)
            {
                h = gameObject.AddComponent<Highlighter>();
            }

            //打开闪烁
            h.FlashingOn(new Color32(178, 255, 255, 0), new Color32(178, 255, 255, 255), 0.3f);
            h.seeThrough = false;

            //空调白雾
            whiteSmoke.SetActive(true);
        }

        private void OnDisable()
        {
            //关闭闪烁
            h.FlashingOff();
            //空调白雾
            whiteSmoke.SetActive(false);
        }

    }
}