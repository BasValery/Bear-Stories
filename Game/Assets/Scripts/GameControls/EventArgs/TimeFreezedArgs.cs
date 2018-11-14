using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TimeFreezedEventArgs : EventArgs
{
    #region Fields
    private bool m_freezed;
    #endregion //Fields

    #region Constructors
    public TimeFreezedEventArgs(bool freezed)
    {
        Freezed = freezed;
    }
    #endregion Constructors

    #region Properties
    public bool Freezed
    {
        get { return m_freezed; }
        private set { m_freezed = value; }
    }
    #endregion //Properties
}
