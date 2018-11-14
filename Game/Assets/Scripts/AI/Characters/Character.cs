using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dialogs;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
class Character : MonoBehaviour
{
    #region Fields
    private DialogManager m_dialogManager;
    private DialogControl m_dialogControl;
    private GameObject m_player;
    private MovementGuider m_playerMovementGuider;
    private bool m_dialogStarted = false;
    private bool m_playerReached = false;
    private bool m_shouldLeave = false;
    #endregion //Fields

    #region Properties
    public DialogManager DialogManager
    {
        get { return m_dialogManager; }
        set { m_dialogManager = value; }
    }

    public DialogControl DialogControl
    {
        get { return m_dialogControl; }
    }

    public bool DialogStarted
    {
        get { return m_dialogStarted; }
    }
    
    public bool PlayerReached
    {
        get { return m_playerReached; }
    }

    public bool ShouldLeave
    {
        get { return m_shouldLeave; }
        set { m_shouldLeave = value; }
    }
    #endregion //Properties

    #region Messages
    private void Awake()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_playerMovementGuider = m_player.GetComponent<MovementGuider>();
    }

    private void OnEnable()
    {
        Global.Time.TimeOfDayChanged += Time_TimeOfDayChanged;
    }

    private void OnDisable()
    {
        Global.Time.TimeOfDayChanged -= Time_TimeOfDayChanged;
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        m_dialogControl = FindObjectOfType<DialogControl>();
    }
    #endregion //Messages

    #region Methods
    private void Time_TimeOfDayChanged(object sender, DayChangedEventArgs e)
    {
        switch (e.CurrentTimeOfDay)
        {
            case TimeOfDay.Night:
                if (m_dialogStarted)
                {
                    m_shouldLeave = true;
                }
                break;
        }
    }

    private void DialogueIsOver(object sender, EventArgs e)
    {
        if (m_shouldLeave)
        {
            m_shouldLeave = false;
        }
    }
    #endregion //Methods
}
