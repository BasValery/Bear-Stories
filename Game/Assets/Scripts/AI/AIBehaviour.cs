using Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace AI
{

    [RequireComponent(typeof(MovementGuider))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ItemTaker))]
    [RequireComponent(typeof(AudioSource))]
    public abstract class AIBehavior : MonoBehaviour
    {
        #region Fields
        public float m_conversationDistance = 2f;
        protected static MovementGuider m_playerMovementGuider;
        protected string m_displayName;
        protected string m_name;
        protected bool m_playerReached;

        protected Animator m_animator;
        protected Collider2D m_collider;
        protected MovementGuider m_movementGuider;
        protected DialogControl m_dialogControl;
        protected DialogManager m_dialogManager;
        protected AudioSource m_audioSource;
        private ItemTaker m_itemTaker;

        #endregion //Fields

        #region Properties
        public Vector3 Bottom
        {
            get { return m_movementGuider.Bottom; }
        }
        #endregion //Properties

        #region Messages
        protected virtual void OnMouseDown()
        {
            OnInteraction();
        }

        protected virtual void Awake()
        {
            m_collider = GetComponent<Collider2D>();
            m_animator = GetComponent<Animator>();
            m_itemTaker = GetComponent<ItemTaker>();
            m_itemTaker.Check = ItemDragged;
            m_movementGuider = GetComponent<MovementGuider>();
            m_audioSource = GetComponent<AudioSource>();
            if (m_playerMovementGuider == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                m_playerMovementGuider = player.GetComponent<MovementGuider>();
            }
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        protected virtual void OnEnable()
        {
            Global.Time.TimeOfDayChanged += TimeOfDayChanged;
        }

        protected virtual void OnDisable()
        {
            Global.Time.TimeOfDayChanged -= TimeOfDayChanged;
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
        }


        protected virtual void Update()
        {
            if (m_playerReached &&
                Vector2.Distance(m_playerMovementGuider.transform.position, transform.position) > m_conversationDistance + 1f)
            {
                m_dialogControl.EndDialog();
                m_playerReached = false;
            }
        }
        #endregion //Messages

        #region Methods
        protected abstract void TimeOfDayChanged(object sender, DayChangedEventArgs e);

        protected abstract void OnInteraction();


        protected virtual bool ItemDragged(Item item)
        {
            return false;
        }

        protected float GetDirection(float value)
        {
            return value > 0 ? 1 : -1;
        }


        protected virtual void OnLaunch(string name, string displayName)
        {
            m_name = name;
            m_displayName = displayName;
        }

        protected void DialogIsOver(object sender, EventArgs args)
        {
            m_playerReached = false;
        }

        protected void StartDialog(DialogManager dialogManager)
        {
            if (m_dialogManager != null)
            {
                m_dialogManager.DialogueIsOver -= DialogIsOver;
            }
            m_playerReached = true;
            m_dialogManager = dialogManager;
            dialogManager.FirstHeroImageName = m_name;
            dialogManager.FirstParticipantName = m_displayName;
            dialogManager.SecondHeroImageName = "Player";
            dialogManager.SecondParticipantName = "Толик";
            dialogManager.DialogueIsOver += DialogIsOver;
            m_dialogControl.RegisterDialogManager(dialogManager);
        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            m_dialogControl = FindObjectOfType<DialogControl>();
        }

        protected void SendPlayer(MainParitipant mainParticipant, Action reached = null)
        {
            if (m_playerMovementGuider)
            {
                Vector3 destination = new Vector3(
                    transform.position.x,
                    m_movementGuider.Bottom.y,
                    transform.position.z
                    );
                switch (mainParticipant)
                {
                    case MainParitipant.First:
                        destination.x += 2f;
                        break;
                    case MainParitipant.Second:
                        destination.x -= 2f;
                        break;
                }
                m_playerMovementGuider.Destination = new Destination(destination, reached, null);
            }
            else
            {
                Debug.Log("Plaer is not net");
            }
        }
        


        protected void CallPlayer(DialogManager dialogManager)
        {
            SendPlayer(
                dialogManager.GetMainParticipant(),
                () =>
                {
                    var playerScale = m_playerMovementGuider.transform.localScale;
                    var scale = transform.localScale;
                    var delta = m_playerMovementGuider.transform.position.x - transform.position.x;
                    playerScale.x = Mathf.Abs(playerScale.x) * GetDirection(scale.x) * -1;
                    m_playerMovementGuider.transform.localScale = playerScale;
                    StartDialog(dialogManager);
                }
                );
        }

        protected Vector3 ToVector3(Vector2 vector)
        {
            return new Vector3(
                vector.x,
                vector.y,
                transform.position.z
                );
        }

        #endregion //Methods
    }

}