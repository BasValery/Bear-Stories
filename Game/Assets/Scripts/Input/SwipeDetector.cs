using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public enum SwipeDirection
{
    UP, DOWN, LEFT, RIGHT
}

public class SwipeEvent : UnityEvent<SwipeDirection>
{
}

public class SwipeDetector : MonoBehaviour
{
    #region Fields
    private Vector2 fingerDown; // The terminal thinger position
    private Vector2 fingerUp; // The start thinger position
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;
    #endregion //Fields

    #region Events
    public UnityEvent<SwipeDirection> OnSwipe = new SwipeEvent();
    #endregion //Eventsk

    #region Messages
    // Update is called once per frame
    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    CheckSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                CheckSwipe();
            }
        }
    }
    #endregion //Messages


    #region Methods
    void CheckSwipe()
    {
        //Check if Vertical swipe
        if (VerticalDisplacement() > SWIPE_THRESHOLD && VerticalDisplacement() > HorizontalDisplacement())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipe.Invoke(SwipeDirection.UP);
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipe.Invoke(SwipeDirection.DOWN);
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (HorizontalDisplacement() > SWIPE_THRESHOLD && HorizontalDisplacement() > VerticalDisplacement())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipe.Invoke(SwipeDirection.RIGHT);
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipe.Invoke(SwipeDirection.LEFT);
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float VerticalDisplacement()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float HorizontalDisplacement()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }
    #endregion //Methods
}
