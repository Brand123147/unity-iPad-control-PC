using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class CameraRotate : MonoBehaviour
{
    //private int isforward;//标记摄像机的移动方向
    public GameObject Centre;//中心目标
    public float SpeedY = 10f;
    public float SpeedX = 5f;
    private bool rotate = false;
    public float minEulerX = 20;
    public float maxEulerX = 80;
    //public float LDMinEulerX = 20;
    //public float LDMaxEulerX = 60;
    public float maxView = 90f;
    public float minView = 3f;
    //public GameObject huxing;
    private bool BlHXon;
    //private bool ChangeToLD;
    //float EularY;
    private GameObject HXMianMenuObj;
    //public  GameObject HXCentPoint;

    public static float fieldView = 60f;  //用来同步iPad放大缩小
    public static Transform tr;   //用来同步iPad位置

    static float timer = 10f;
    void Start()
    {
        //fieldView = CameraMove.Camview;
       // StartCoroutine(AnimatorFalse());
    }

    //IEnumerator AnimatorFalse()
    //{
    //    yield return new WaitForSecondsRealtime(6f);
    //    gameObject.GetComponent<Animator>().enabled = false;
    //}

    void Update()
    {
        tr = GetComponent<Transform>();   //实时获取位置
#if UNITY_IOS
        MoveFinger(Centre);
        FingerfieldOfView();
#endif
#if UNITY_EDITOR
        MoveMouse(Centre);
        ScrollView();
        MoveFinger(Centre);
        FingerfieldOfView();
#endif

#if UNITY_STANDALONE_WIN
        MoveMouse(Centre);
        ScrollView();
        MoveFinger(Centre);
        FingerfieldOfView();
#endif
        gameObject.GetComponent<Camera>().fieldOfView = fieldView;

    }
    void OnEnable()
    {
        //fieldView = CameraMove.Camview;
        rotate = false;
        this.transform.position = new Vector3(-8f, 101f, 192f);
        this.transform.rotation = Quaternion.Euler(new Vector3(30f, 180f, 0f));
        this.transform.localScale = Vector3.one;
    }

    //void OnGUI()
    //{
    //    GUIStyle font = new GUIStyle();
    //    font.fontSize = 60;
    //    GUI.Label(new Rect(400, 160, 300, 500), "x::" + transform.eulerAngles.x, font);
    //    GUI.Label(new Rect(400, 200, 300, 500), "y::" + transform.eulerAngles.y, font);
    //    //GUI.Label(new Rect(400, 140, 330, 500), "x:" + this.transform.eulerAngles.x, font);
    //    //GUI.Label(new Rect(400, 180, 330, 500), "y:" + this.transform.eulerAngles.y, font);
    //}


    //手指
    float touch_x;
    float touch_y;
    void MoveFinger(GameObject Centre)
    {
        if (Input.touchCount == 1 && !ClickIsOverUI.Instance.IsPointerOverUIObject(Input.GetTouch(0).fingerId))
        {
            touch_x = Input.touches[0].deltaPosition.x;
            touch_x = MathDate(touch_x, 3) / 3;
            touch_y = Input.touches[0].deltaPosition.y;
            touch_y = -MathDate(touch_y, 3) / 3;
            if (Input.touches[0].phase == TouchPhase.Moved) //手指在屏幕上移动，移动摄像机
            {
                if (Input.touches[0].deltaPosition.x < -1 || Input.touches[0].deltaPosition.x > 1)
                {
                    StartCoroutine(InertiaRotate(Centre.transform.position, new Vector3(0, 1, 0), touch_x, 20));
                }
                StartCoroutine(InertiaRotateY(Centre.transform.position, touch_y, 30));
                if (Input.touches[0].deltaPosition.y < -1 && (transform.eulerAngles.x <= maxEulerX || this.transform.eulerAngles.x >= 300))
                {
                    BlRotateY = true;
                }
                else if (Input.touches[0].deltaPosition.y > 1 && transform.eulerAngles.x >= minEulerX)
                {
                    BlRotateY = true;
                }
                else
                {
                    BlRotateY = false;
                }
            }
            else
            {
                BlRotateY = false;
            }
        }
    }


    float MathDate(float touch_x, float y)
    {
        if (touch_x <= y && touch_x >= -y)
        {
            touch_x = 0;
        }
        touch_x = touch_x / 5;
        return touch_x = Mathf.Clamp(touch_x, -5, 5);
    }
    IEnumerator InertiaRotate(Vector3 point, Vector3 VecAsix, float x, int y)
    {
        for (int i = y - 1; i > 0; i--)
        {
            float a = Mathf.Pow((i), 2) * x / Mathf.Pow((y - 1), 2);
            transform.RotateAround(point, VecAsix, a);
            yield return new WaitForFixedUpdate();
        }
    }
    bool BlRotateY = true;
    IEnumerator InertiaRotateY(Vector3 point, float x, int y)
    {
        for (int i = y - 1; i > 0; i--)
        {
            if (BlRotateY)
            {
                float a = Mathf.Pow((i), 2) * x / Mathf.Pow((y - 1), 2);
                transform.RotateAround(point, transform.right, a);
                yield return new WaitForFixedUpdate();
            }
        }
    }

    //鼠标
    void MoveMouse(GameObject Centre)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {

            float mouse_x = Input.GetAxis("Mouse X");//获取鼠标X轴移动
            float mouse_y = -Input.GetAxis("Mouse Y");//获取鼠标Y轴移动
            mouse_y = Mathf.Clamp(mouse_y / 2, -1, 1);
            if (rotate)
            {
                if (mouse_y > 0 && this.transform.eulerAngles.x > maxEulerX)
                {
                    mouse_y = 0;
                }
                if (mouse_y < 0 && this.transform.eulerAngles.x < minEulerX)
                {
                    mouse_y = 0;
                }
                transform.RotateAround(Centre.transform.position, transform.right, SpeedY * mouse_y);
                transform.RotateAround(Centre.transform.position, Vector3.up, SpeedX * mouse_x);
            }
            if (Input.GetMouseButtonDown(0))
            {
                rotate = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                rotate = false;
            }
        }
    }


    void OnDisable()
    {
        //fieldView = CameraMove.Camview;
    }
    void ScrollView()
    {
        if (fieldView > 3 || fieldView < 90)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                fieldView -= 2;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                fieldView += 2;
            }
            fieldView = Mathf.Clamp(fieldView, 2, 90);
        }



    }

    /// <summary>  
    /// 两指缩放  
    /// </summary>  
    float fieldNum;
    float distance;
    float oldDistance;
    private Vector2 oldPosition1;
    private Vector2 oldPosition2;
    private Vector2 lastSingleTouchPosition;
    //这个变量用来记录单指双指的变换  
    private bool m_IsSingleFinger;
    private int oldTouchCount = 0;
    private int TouchCount = 0;
    private bool Istwo = true;
    void FingerfieldOfView()
    {
        TouchCount = 0;
        //判断触摸数量为单点触摸  
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began || !m_IsSingleFinger)
            {
                //在开始触摸或者从两字手指放开回来的时候记录一下触摸的位置  
                lastSingleTouchPosition = Input.GetTouch(0).position;
            }
            m_IsSingleFinger = true;
            Istwo = true;
            a = 0;
        }
        else if (Input.touchCount > 1)
        {
            Istwo = false;
            //当从单指触摸进入多指触摸的时候,记录一下触摸的位置  
            //保证计算缩放都是从两指手指触碰开始的  
            if (m_IsSingleFinger)
            {
                oldPosition1 = Input.GetTouch(0).position;
                oldPosition2 = Input.GetTouch(1).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                ScaleCamera();
            }
            if (a == 1)
            {
                fieldView = fieldView + 0.5f;
            }
            else if (a == 2)
            {
                fieldView = fieldView - 0.5f;
            }
            m_IsSingleFinger = false;
            TouchCount = 2;
        }
        else
        {
            a = 0;
        }
        fieldView = Mathf.Clamp(fieldView, minView, maxView);
        oldTouchCount = TouchCount;
    }
    int a = 0;
    float currentTouchDistance;
    float lastTouchDistance;
    private void ScaleCamera()
    {
        //计算出当前两点触摸点的位置  
        var tempPosition1 = Input.GetTouch(0).position;
        var tempPosition2 = Input.GetTouch(1).position;
        currentTouchDistance = Vector3.Distance(tempPosition1, tempPosition2);
        lastTouchDistance = Vector3.Distance(oldPosition1, oldPosition2);
        distance -= (lastTouchDistance - currentTouchDistance);
        //distance = -distance / 50;
        //distance = Mathf.Clamp(distance, -5,5);
        if ((distance - oldDistance) > 3 && fieldView <= maxView)
        {
            a = 2;

        }
        else if ((distance - oldDistance) < -3 && fieldView >= minView)
        {
            a = 1;
        }

        oldDistance = distance;
    }

}



