using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[RequireComponent(typeof(SpriteOutline))]
[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{

    #region Fields
    public int m_outlineSize = 4;
    private SpriteOutline m_outline;
    private Collider2D m_collider;
    private RaycastHit2D m_rayCastHit;
    #endregion //Fields

    #region Events
    public UnityEvent Click = new UnityEvent();
    #endregion

    #region Messages
    private void Start()
    {
        m_outline = GetComponent<SpriteOutline>();
        m_collider = GetComponent<Collider2D>();
    }


#if UNITY_STANDALONE
    private void OnMouseDown()
    {
        Click.Invoke();
    }

    private void OnMouseEnter()
    {
        if (m_outline == null)
        {
            return;
        }
        m_outline.outlineSize = m_outlineSize;
    }

    private void OnMouseExit()
    {
        if (m_outline == null)
        {
            return;
        }
        m_outline.outlineSize = 0;
    }


#elif (UNITY_IOS || UNITY_ANDROID)
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    m_rayCastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
                    if (m_rayCastHit.collider == m_collider)
                    {
                        Outline(true);
                        Click.Invoke();
                    }
                    else
                    {
                        Outline(false);
                    }
                    break;
                case TouchPhase.Moved:
                    Outline(false);
                    break;
            }
            
        }
        else
        {
            Outline(false);
        }
    }
#endif
    #endregion //Messages

    #region Methods
    public void Outline(bool outline)
    {
        if (outline)
        {
            m_outline.outlineSize = m_outlineSize;
        }
        else
        {
            m_outline.outlineSize = 0;
        }
    }
    #endregion //Methods
}
