using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliceScript : MonoBehaviour {

    public UnityEvent onCuted = new UnityEvent();
    public UnityEvent onUnCuted = new UnityEvent();

    public GameObject knife;
    public RectTransform targetPanel;
    public RectTransform backGroud;
    public Slider slider;
    public int difficult;
    public int speed = 50;
    public bool inputAccept { get; set; }

    private float rectWidth;

    private bool started;
    private int move = 1;
    // Use this for initialization
    void Start () {
        rectWidth = backGroud.rect.width;
        setWidth();
        onCuted.AddListener(CutDebug);
        onUnCuted.AddListener(UnCutDebug);
        inputAccept = true;
    }
	
	// Update is called once per frame
	void Update () {

        started = knife.GetComponent<KnifeAnim>().Done;
        if (started)
        {

            if (Input.GetKeyDown(KeyCode.Space) && inputAccept)
            {
                started = false;
             
                CheckCut();

            }

            if (Input.touchCount == 1 && inputAccept)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    started = false;
                    CheckCut();
                }
            }

            slider.value += move * speed * UnityEngine.Time.deltaTime;
            if (slider.value >= 100 || slider.value <= 0)
                move = move == 1 ? -1 : 1;
            Vector3 oldPos = knife.transform.position;

            knife.transform.Translate((move * speed * Time.deltaTime)/17, 0, 0);


         

        }
		

	}
    void CheckCut()
    {
        if( Mathf.Abs((slider.value - 50) * 2) <= difficult)
        {
            knife.GetComponent<KnifeAnim>().startCut(true);
            if (onCuted != null)
                onCuted.Invoke();
        }else if(onUnCuted!= null)
        {
            knife.GetComponent<KnifeAnim>().startCut(false);
            onUnCuted.Invoke();
        }
    }

    void setWidth()
    {
        float targetWidt = rectWidth - rectWidth / 100 * difficult;
        Vector2 hightUpDown = new Vector2(targetPanel.offsetMax.y, targetPanel.offsetMin.y);
        targetPanel.offsetMax = new Vector2(-(targetWidt / 2), hightUpDown.x);
        targetPanel.offsetMin = new Vector2(targetWidt / 2, hightUpDown.y);
    }

    void StartSlider()
    {
        started = true;
    }
     void StopSlider()
    {
        started = false;
    }

    void CutDebug()
    {
        Debug.Log("Cuted");
    }
    void UnCutDebug()
    {
        Debug.Log("UnCuted");
    }

}
