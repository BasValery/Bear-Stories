using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dialogs;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Target : MonoBehaviour
{
    #region Fields
    public DialogManager m_dialogManager;
    private MovementGuider m_movementGuider;
    private Collider2D m_collider;
    private RaycastHit2D m_rayCastHit;
    private bool m_started;
    private Vector2 m_startClick;
    #endregion //Fields

    #region Messages
    private void Awake()
    {
        m_movementGuider = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<MovementGuider>();
        m_collider = GetComponent<Collider2D>();
    }

#if UNITY_STANDALONE
    private void OnMouseDown()
    {
        Vector3 destinationPos = this.transform.position;
        var bounds = GetComponent<BoxCollider2D>().bounds;
        destinationPos.y = bounds.min.y - 0.2f;
        destinationPos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        m_movementGuider.Destination = new Destination(destinationPos, OnReached, OnAborted);
        if (m_dialogManager != null)
        {
        }
    }
#elif (UNITY_IOS || UNITY_ANDROID)
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            var touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            m_rayCastHit = Physics2D.Raycast(touchPos, Vector2.zero);
            if (m_rayCastHit.collider == m_collider)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (m_rayCastHit.collider == m_collider)
                        {
                            m_started = true;
                            m_startClick = touch.position;
                        }
                        break;
                    case TouchPhase.Ended:
                        if (m_started == true && m_startClick == touch.position)
                        {
                            Vector3 destinationPos = this.transform.position;
                            var bounds = GetComponent<BoxCollider2D>().bounds;
                            destinationPos.y = bounds.min.y - 0.2f;
                            destinationPos.x = touchPos.x;
                            m_movementGuider.Destination = new Destination(destinationPos, OnReached, OnAborted);
                        }
                        m_started = false;
                        break;
                    case TouchPhase.Canceled:
                        m_started = false;
                        break;
                }
            }
        }
    }
#endif
    #endregion //Messages

    #region Methods
    protected virtual void OnReached()
    {
    }

    protected virtual void OnAborted()
    {
    }
#endregion //Methods
}
