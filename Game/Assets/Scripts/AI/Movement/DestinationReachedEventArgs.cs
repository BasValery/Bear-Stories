using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DestinationReachedEventArgs : EventArgs
{
    #region Fields
    Destination m_destination;
    #endregion //Fields

    #region Constructors
    public DestinationReachedEventArgs(Destination destination)
    {
        m_destination = destination;
    }
    #endregion //Constructors

    #region Properties 
    public Destination Destination
    {
        get { return m_destination; }
        private set { m_destination = value; }
    }
    #endregion //Properties
}
