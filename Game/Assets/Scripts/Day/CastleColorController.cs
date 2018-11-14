using System;
using UnityEngine;

public class CastleColorController : MonoBehaviour
{
    #region Fields
    public Color m_dayColor;
    public Color m_nightColor;
    private SpriteRenderer m_renderer;
    #endregion //Fields

    #region Messages
    private void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        switch (Global.Time.TimeOfDay)
        {
            case TimeOfDay.Day:
                m_renderer.color = Color.Lerp(m_nightColor, m_dayColor,Global.Time.DayTimeProgress);
                m_renderer.color = m_dayColor;
                break;
            case TimeOfDay.Night:
                m_renderer.color = Color.Lerp(m_dayColor, m_nightColor,  0.25f / Global.Time.DayTimeProgress);
                //m_renderer.color = m_nightColor;
                break;
        }
    }
    #endregion //Messages
}