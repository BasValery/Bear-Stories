using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DayChangedEventArgs : EventArgs
{
    #region Fields
    private int m_currentDay;
    private TimeOfDay m_currentTimeOfDay;
    #endregion //Fields

    #region Constructors
    public DayChangedEventArgs(int day, TimeOfDay timeOfDay)
        : base()
    {
        if (day < 1)
        {
            throw new ArgumentOutOfRangeException("day", day, "must be bigger than zero");
        }
        m_currentDay = day;
        m_currentTimeOfDay = timeOfDay;
    }
    #endregion //Constructors

    #region Properties
    public int CurrentDay
    {
        get { return m_currentDay; }
    }
    
    public TimeOfDay CurrentTimeOfDay
    {
        get { return m_currentTimeOfDay; }
    }
    #endregion //Properties
}
