using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum ArrowDirection
{
    UP, UPRIGHT, RIGHT, RIGHTDOWN, DOWN, DOWNLEFT, LEFT, LEFTUP
}

[RequireComponent(typeof(Animator))]
public class ArrowController : MonoBehaviour
{
    #region Fields
    public RectTransform m_object;
    private RectTransform m_rectTransform;
    private float m_initialZ;
    private Animator m_animator;
    #endregion //Fields

    #region Messages
    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_initialZ = m_rectTransform.localEulerAngles.z;
        Hide();
    }

    private void OnEnable()
    {
        Global.Time.TimeFreezed += TimeFreezed;
    }

    private void OnDisable()
    {
        Global.Time.TimeFreezed -= TimeFreezed;
    }
    #endregion //Messages

    #region Methods
    public void PointOut(RectTransform objTranform, ArrowDirection direction)
    {
        //m_animator.SetBool("show", true);
        gameObject.SetActive(true);

        Vector3 newPos = objTranform.position;
        newPos.z = transform.position.z;
        Vector3[] objCorners = new Vector3[4];
        Vector3[] corners = new Vector3[4];
        objTranform.GetWorldCorners(objCorners);
        m_rectTransform.GetWorldCorners(corners);
        float width = Mathf.Abs(objCorners[2].x - objCorners[0].x);
        float height = Mathf.Abs(objCorners[1].y - objCorners[3].y);
        float arrowWidth = Mathf.Abs(corners[2].x - corners[0].x);
        float arrowHeight = Mathf.Abs(corners[1].y - corners[3].y);

        var angleWidth = (width / 2f) + (arrowWidth / 4f);
        var angleHeight = (height / 2f) + (arrowHeight / 4f);

        width = (width / 2f) + (arrowWidth / 2f);
        height = (height / 2f)  + (arrowHeight / 2f);
        
        Vector3 rotation = m_rectTransform.localEulerAngles;

        switch (direction)
        {
            case ArrowDirection.UP:
                newPos.y += height;
                rotation.z = m_initialZ;
                break;
            case ArrowDirection.UPRIGHT:
                newPos.x += angleWidth;
                newPos.y += angleHeight;
                rotation.z = m_initialZ - 45;
                break;
            case ArrowDirection.RIGHT:
                newPos.x += width;
                rotation.z = m_initialZ - 90;
                break;
            case ArrowDirection.RIGHTDOWN:
                newPos.x += angleWidth;
                newPos.y -= angleHeight;
                rotation.z = m_initialZ - 135;
                break;
            case ArrowDirection.DOWN:
                newPos.y -= height;
                rotation.z = m_initialZ - 180;
                break;
            case ArrowDirection.DOWNLEFT:
                newPos.x -= angleWidth;
                newPos.y -= angleHeight;
                rotation.z = m_initialZ - 225;
                break;
            case ArrowDirection.LEFT:
                newPos.x -= width;
                rotation.z = m_initialZ - 270;
                break;
            case ArrowDirection.LEFTUP:
                newPos.x -= angleWidth;
                newPos.y += angleHeight;
                rotation.z = m_initialZ - 315;
                break;
        }
        m_rectTransform.position = newPos;
        m_rectTransform.localEulerAngles = rotation;
    }

    private void TimeFreezed(object sender, TimeFreezedEventArgs e)
    {
        if (e.Freezed)
        {
            m_animator.speed = 0;
        }
        else
        {
            m_animator.speed = 1;
        }
    }

    public void Hide()
    {
        //m_animator.SetBool("show", false);
        gameObject.SetActive(false);
    }
    #endregion //Methods
}
