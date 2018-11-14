using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameTime : MonoBehaviour {
    #region Fields
    [HideInInspector]
    public static GameTime Instance; //A reference to our game control script so we can access it statically.
    #region Settings
    public int m_dayDuration;
    [Range(0, 1)]
    public float m_transitionTime = 0.1f;
    private double m_timeElapsed = 250;
    private TimeOfDay m_cashedTimeOfDay;
    private int m_cashedDay;
    private bool m_freezed = false;
    #endregion //Settings

    #endregion //Fields


    #region Events
    public event EventHandler<DayChangedEventArgs> TimeOfDayChanged;
    public event EventHandler<DayChangedEventArgs> DayChanged;
    public event EventHandler<TimeFreezedEventArgs> TimeFreezed;
    #endregion //Events

    #region Properties
    public double TimeElapsed
    {
        get { return m_timeElapsed; }
    }

    public TimeOfDay TimeOfDay
    {
        get
        {
            if (m_timeElapsed == 0)
            {
                return 0;
            }
            // if number is even it is day, if odd it is night
            return (int)(m_timeElapsed / (m_dayDuration / 2.0f)) % 2 == 0 ?
                TimeOfDay.Day :
                TimeOfDay.Night;
        }
    }

    public int CurrentDay
    {
        get
        {
            return (int)(m_timeElapsed % m_dayDuration) + 1;
        }
    }

    public double CalculateTime(double days)
    {
        return (int)(m_dayDuration * days);
    }

    //the current time of day in procents
    public float DayTimeProgress
    {
        get
        {
            float halfday = m_dayDuration / 2.0f;
            return (float)(m_timeElapsed % halfday) / (halfday);
        }
    }

    public float DayProgress
    {
        get
        {
            return (float)(m_timeElapsed % m_dayDuration) / m_dayDuration;
        }
    }

    public bool IsFreezed
    {
        get { return m_freezed; }
    }
    #endregion //Properties

    #region Messages
    // Use this for initialization
    void Start () {
        m_cashedTimeOfDay = TimeOfDay;
        m_cashedDay = CurrentDay;
	}

    private void Awake()
    {
        //If we don't currently have a game control...
        if (Instance == null)
        {
            //...set this one to be it...
            Instance = this;
        }
        //...otherwise...
        else if (Instance != this)
        {
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (!m_freezed)
        {
            Step();
        }
    }

    private void OnValidate()
    {
        if (m_dayDuration < 1)
        {
            Debug.Log("Day length is less than zero");
            m_dayDuration = 1;
        }
    }
    #endregion //Messages

    #region Methods
    public double TimeLeft(double time)
    {
        return time - TimeElapsed;
    }

    public void Freeze(bool freeze)
    {
        var freezedMemento = m_freezed;
        m_freezed = freeze;
        if (m_freezed != freezedMemento)
        {
            if (TimeFreezed != null)
            {
                TimeFreezed(this, new TimeFreezedEventArgs(m_freezed));
            }
        }
    }

    public int GetDay(double time)
    {
        return (int)time % m_dayDuration;
    }


    public int GetDays(double time)
    {
        return (int)time / m_dayDuration;
    }

    private void Step()
    {
        m_timeElapsed += UnityEngine.Time.deltaTime;

        int currentDay = CurrentDay;
        TimeOfDay currentTimeOfDay = TimeOfDay;

        if (m_cashedDay != currentDay)
        {
            if (DayChanged != null)
            {
                DayChanged(this, new DayChangedEventArgs(currentDay, currentTimeOfDay));
            }
        }
        if (m_cashedTimeOfDay != currentTimeOfDay)
        {
            if (TimeOfDayChanged != null)
            {
                TimeOfDayChanged(this, new DayChangedEventArgs(currentDay, currentTimeOfDay));
            }
        }

        m_cashedDay = currentDay;
        m_cashedTimeOfDay = currentTimeOfDay;
    }
    #endregion //Methods
}
