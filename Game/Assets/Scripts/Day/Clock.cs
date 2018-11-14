using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    #region Fields
    public GameObject m_arrow;
    public GameObject m_sun;
    public GameObject m_moon;
    public int m_dayDuration;

    private TimeOfDay m_timeOfDay;
    private float m_dayTimeProgress;
    private float m_intitialZRotation;
    #endregion //Fields

    #region Properties
    public TimeOfDay TimeOfDay
    {
        get { return m_timeOfDay; }
        set
        {
            m_timeOfDay = value;
        }
    }

    public float DayTimeProgress
    {
        get { return m_dayTimeProgress; }
        set { m_dayTimeProgress = value; }
    }
    #endregion //Properties

    #region Messages
    // Use this for initialization
    void Start()
    {
        if (m_moon == null)
        {
            Debug.LogWarning("Clock moon is not set");
            return;
        }
        else if (m_sun == null)
        {
            Debug.LogWarning("Clock sun is not set");
            return;
        }

        //ЭТО ЕБАННЫЙ КОСТЫЛЬ
        //TODO: Убрать его. Пожааалуйста... 
        m_intitialZRotation = m_arrow.transform.localEulerAngles.z - 180 /*- Global.Time.DayTimeProgress /*(Global.Time.m_dayDuration/Global.Time.TimeElapsed) * 360*/;
    }

    // Update is called once per frame
    void Update()
    {
        m_sun.SetActive(m_timeOfDay == TimeOfDay.Day);
        m_moon.SetActive(m_timeOfDay == TimeOfDay.Night);

        //m_arrow.transform.rotation = Quaternion.Euler(0, 0, 0);

        m_arrow.transform.localEulerAngles = new Vector3(
            0,
            0,
            -((360.0f * DayTimeProgress)) + m_intitialZRotation
        );
    }

    private void OnValidate()
    {
        if (m_arrow == null)
        {
            m_arrow = transform.Find("Arrow").gameObject;
        }
        if (m_moon == null)
        {
            m_moon = transform.Find("Moon").gameObject;
        }
        if (m_sun == null)
        {
            m_sun = transform.Find("Sun").gameObject;
        }
    }
    #endregion //Messages

}
