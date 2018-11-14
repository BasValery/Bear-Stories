using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum NPCSTate : int
{
    Walk = 1, Stop = 3, Fly = 4, Peck = 5, Jump = 6, Balancing = 7, Falling = 8, Hit = 9, Lying = 10
}

public class Destination
{
    #region Fields
    private string m_name;
    private Vector3 m_destination;
    private Action m_action;
    private Action m_aborted;
    private bool m_autoInvoke = true;
    #endregion //Fields

    #region Constructors
    public Destination(Vector3 destinationPos, Action action, Action aborted, string name = null)
    {
        Position = destinationPos;
        m_action = action;
        m_aborted = aborted;
        m_name = name;
    }

    public Destination(Vector3 destinationPos, Action action, string name = null)
    {
        Position = destinationPos;
        m_action = action;
        m_name = name;
    }

    public Destination(Vector3 destinationPos, string name = null)
    {
        Position = destinationPos;
        m_name = name;
    }
    #endregion //Constructors

    #region Properties
    public bool AutoInvoke
    {
        get { return m_autoInvoke; }
        set { m_autoInvoke = true;}
    }

    public Vector3 Position
    {
        get { return m_destination; }
        set { m_destination = value; }
    }

    public string Name
    {
        get { return m_name; }
        private set { m_name = value; }
    }
    #endregion //Properties

    #region Methods
    public void Invoke()
    {
        if (m_action != null)
        {
            m_action.Invoke();
        }
    }

    public void Abort()
    {
        if (m_aborted != null)
        {
            m_aborted.Invoke();
        }
    }
    #endregion //Methods
}
