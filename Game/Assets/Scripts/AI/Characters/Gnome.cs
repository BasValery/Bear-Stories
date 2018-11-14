using Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MovementGuider))]
[RequireComponent(typeof(Animator))]
class Gnome : MonoBehaviour
{
    #region Fields
    public Vector2 m_stonePos = new Vector2();
    private bool m_chiseling;
    private AudioSource m_audio;
    private Animator m_animator;
    private MovementGuider m_movementGuider;
    private Vector3 m_initialPos;

    private DialogManager m_dialogManager;
    private DialogControl m_dialogControl;
    private GameObject m_player;
    private MovementGuider m_playerMovementGuider;
    private bool m_dialogStarted = false;
    private bool m_playerReached = false;
    private bool m_shouldLeave = false;
    #endregion //Fields

    #region Proeprties
    public bool IsChiseling
    {
        get { return m_chiseling; }
        private set
        {
            m_chiseling = value;
            if(m_audio!= null && m_audio.enabled)
            if (value)
                m_audio.Play();
            else
                m_audio.Stop();
        }
    }
    #endregion //Properties

    #region Messages
    private void Awake()
    {
        m_movementGuider = GetComponent<MovementGuider>();
        m_animator = GetComponent<Animator>();
        m_movementGuider.DestinationReached += DestinationReached;
        m_initialPos = transform.position;
        m_dialogManager = new GnomeDialogManager();
        SceneManager.sceneLoaded += SceneLoaded;
        m_dialogManager.DialogueIsOver += DialogueIsOver;
        m_audio = GetComponent<AudioSource>();
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
       
            IsChiseling = IsChiseling;

    }

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_playerMovementGuider = m_player.GetComponent<MovementGuider>();
        m_movementGuider.Destination = new Destination(m_stonePos, StoneReached, null);
    }

    private void Update()
    {
        if (m_playerReached)
        {
            if (Vector2.Distance(m_player.transform.position, transform.position) > 3f)
            {
                m_playerReached = false;
                m_dialogStarted = false;
                m_dialogControl.EndDialog();
                DialogueIsOver(this, new EventArgs());

                //                     //

                if (m_shouldLeave)
                {
                    m_movementGuider.Destination = new Destination(m_initialPos);
                    m_animator.SetBool("chisel", false);
                    m_shouldLeave = false;
                }

            }
        }
    }

    private void OnMouseDown()
    {
        if (IsChiseling)
        {
            var movementGuider = m_player.GetComponent<MovementGuider>();
            var pos = m_stonePos;
            pos.x += 2f;
            m_playerMovementGuider.Destination = new Destination(pos, PlayerReached, null);
            m_dialogStarted = true;
        }
    }
    #endregion //Messages

    #region Methods
    private void DestinationReached(object sender, DestinationReachedEventArgs e)
    {
        e.Destination.Invoke();
    }

    private void StoneReached()
    {
        m_animator.SetBool("chisel", true);
        IsChiseling = true;
    }

    private void Time_TimeOfDayChanged(object sender, DayChangedEventArgs e)
    {
        switch (e.CurrentTimeOfDay)
        {
            case TimeOfDay.Day:
                m_movementGuider.Destination = new Destination(m_stonePos, StoneReached, null);
                break;

                //                      //
                case TimeOfDay.Night:
                  if (m_dialogStarted == false)
                   {
                       m_animator.SetBool("chisel", false);
                       IsChiseling = false;
                       m_movementGuider.Destination = new Destination(m_initialPos);
                   }
                   else
                   {
                       m_shouldLeave = true;
                   }
                   break;
        }
    }

    private void PlayerReached()
    {
        IsChiseling = false;
        m_dialogManager.FirstParticipantName = "Толик";
        m_dialogManager.SecondParticipantName = "Лысый";
        m_dialogManager.FirstHeroImageName = "Player";
        m_dialogManager.SecondHeroImageName = "Gnome";
        m_dialogControl.RegisterDialogManager(m_dialogManager);
        m_playerReached = true;

        var scale = m_player.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * -1;
        m_player.transform.localScale = scale;
        m_animator.SetBool("chisel", false);
        transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
            );
    }

    private void DialogueIsOver(object sender, EventArgs e)
    {
        
        if (m_shouldLeave)
        {
            m_movementGuider.Destination = new Destination(m_initialPos);
            m_shouldLeave = false;
            m_playerReached = false;
            m_dialogStarted = false;
            m_animator.SetBool("chisel", false);
            IsChiseling = false;
          
        }
        else
        //
        {
            IsChiseling = true;
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x) * -1,
                transform.localScale.y,
                transform.localScale.z
            );
            m_animator.SetBool("chisel", true);
        }
    }
    #endregion //Methods
}
